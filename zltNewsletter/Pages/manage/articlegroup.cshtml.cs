using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zltNewsletter.Models;
using zltNewsletter.Data;

namespace zltNewsletter.Pages.manage
{
    public class articlegroupModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // ** Constructor
        public articlegroupModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // ** Properties
        [BindProperty]
        public ArticleSection NewArticle { get; set; }

        public IList<ArticleSection> ArticleSection { get; set; } = default!;


        // ** Methods
        public async Task OnGetAsync()
        {
            ArticleSection = await _context.ArticleSection
                .Include(static z => z.Articles.OrderBy(x => x.ArticleSortOrder))
                .OrderByDescending(x => x.ArticleSectionCreateDate)
                .ToListAsync();
        }

        // Save new record
        public async Task OnPostSaveNew(string NewArticleGroupName, string NewArticleGroupActive)
        {

            // Because the bool checkbox is not coming from a property - we have to do the conversion ourselves
            if (NewArticleGroupName != null)
            {
                var Acti = true;
                if (NewArticleGroupActive == null)
                    Acti = false;


                _context.ArticleSection.Add(new ArticleSection
                {
                    ArticleSectionActive = Acti,
                    ArticleSectionName = NewArticleGroupName

                });
                _context.SaveChanges();
            }

            // Rereads intial data
            await OnGetAsync();
        }


        // Save updated fields
        public async Task OnPostSaveUpdate(string UpdGroupName, bool UpdGrpActive, int Id, DateTime UpdDate)
        {
            var RecToUpdate = _context.ArticleSection.FirstOrDefault(x => x.ArticleSectionId == Id);

            if (UpdGroupName != null)
            {
                {
                    if (RecToUpdate != null)
                    {
                        RecToUpdate.ArticleSectionName = UpdGroupName;
                        RecToUpdate.ArticleSectionActive = UpdGrpActive;
                        RecToUpdate.ArticleSectionCreateDate = UpdDate;
                    }
                    _context.SaveChanges();
                }
            }

            await OnGetAsync();
        }


        // Delete record
        public async Task OnPostDelete(int Id)
        {

            var delpost = _context.ArticleSection.SingleOrDefault(x => x.ArticleSectionId.Equals(Id));

            if (delpost != null)
            {
                _context.ArticleSection.Remove(delpost);
                _context.SaveChanges();
            }

            await OnGetAsync();
        }

    }
}
