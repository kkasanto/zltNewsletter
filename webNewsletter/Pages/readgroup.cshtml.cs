using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zltNewsletter.Models;
using zltNewsletter.Data;

namespace webNewsletter.Pages
{
    public class readgroupModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public readgroupModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ArticleSection ArticleGroup { get; set; } = default!;

        public IList<Article> GroupArticles  { get; set; }

        public Article ReadArticle { get; set; }


        public int StartIndex { get; set; }
        public int CurrentIndex { get; set; }
        public int MaxIndex { get; set; }



        public async Task<IActionResult> OnGetAsync(int articleGroupId, int? articleId)
        {
            // always send GroupID, optional articleId
            // when linking to next article, indicate with groupid AND articleId


            // If articleId is not present find first article in group and set id to that
            if (articleId == null) {
                var artid = _context.Article
                    .OrderBy(x => x.ArticleSortOrder)
                    .Where(s => s.ArticlePublished == true)
                    .FirstOrDefault(z => z.ArticleSectionId == articleGroupId);

                    articleId = artid.ArticleId;
            }            

            ArticleGroup = await _context.ArticleSection.FirstOrDefaultAsync(m => m.ArticleSectionId == articleGroupId);
            GroupArticles = await _context.Article.Where(x => x.ArticleSectionId == articleGroupId)
                    .Where(s => s.ArticlePublished == true)
                    .OrderBy(x => x.ArticleSortOrder)
                    .ToListAsync();

            ReadArticle = await _context.Article.FirstOrDefaultAsync(x => x.ArticleId == articleId);


            return Page();
        }
    }
}
