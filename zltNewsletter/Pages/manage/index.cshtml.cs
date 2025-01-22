using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using zltNewsletter.Models;
using zltNewsletter.Data;

namespace zltNewsletter.Pages.manage
{
    public class indexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public indexModel(ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Article NewArticle { get; set; }

        public List<SelectListItem> GrpList { get; set; }

        public IList<Article> Article { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Article = await _context.Article
                .Include(a => a.ArticleSection).ToListAsync();

            GrpList = _context.ArticleSection.Select(a => new SelectListItem
            {
                Text = a.ArticleSectionName,
                Value = a.ArticleSectionId.ToString()
            }).ToList();
        }

        public async Task OnPostSaveNew(string NewArticleTitle,
                                        bool NewArticleActive,
                                        int NewArticleGroupId,
                                        string NewArticleAuthor)
        {
            // Author is mandatory - prefill if none indicated
            // At a later stage this will change to the login name
            if (NewArticleAuthor == null)
            {
                NewArticleAuthor = "No Author";
            }

            if (NewArticleTitle != null)
            {

                var insArt = _context.Article.Add(new Article
                {
                    ArticlePublished = NewArticleActive,
                    ArticleTitle = NewArticleTitle,
                    ArticleSectionId = NewArticleGroupId,
                    ArticleAuthor = NewArticleAuthor
                });

                await _context.SaveChangesAsync();
            }

            await OnGetAsync();
        }

        public async Task OnPostDelete(int Id)
        {

            var delpost = _context.Article.SingleOrDefault(x => x.ArticleId.Equals(Id));

            if (delpost != null)
            {
                _context.Article.Remove(delpost);
                _context.SaveChanges();
            }

            await OnGetAsync();
        }
    }
}
