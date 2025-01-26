using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using zltNewsletter.Models;
using zltNewsletter.Data;

namespace zltNewsletter.Pages.manage
{
    public class newarticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public newarticleModel(ApplicationDbContext context)
        {
            _context = context;
        }


        // precreate article - to set id 0 - when using partial for form with both edit and create
        [BindProperty]
        public Article Article { get; set; } = new Article();

        [BindProperty]
        public string? ReturnPage { get; set; }

        public IActionResult OnGet(string? rtrPage)
        {
            ViewData["ArticleSectionId"] = new SelectList(_context.ArticleSection, "ArticleSectionId", "ArticleSectionName");

            // preset articleid to 0 - when using partial for form with both edit and create
            Article.ArticleId = 0;

            // If no returnpage is indicated, set to index, otherwise set field ReturnPage on page (hidden input)
            if (rtrPage != null)
                ReturnPage = rtrPage;
            else
                ReturnPage = "index";

            return Page();
        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage(ReturnPage);
        }
    }
}
