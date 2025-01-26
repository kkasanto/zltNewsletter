using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zltNewsletter.Models;
using System.Web;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.IO;
using zltNewsletter.Data;

namespace zltNewsletter.Pages.manage
{
    public class editarticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public editarticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        [BindProperty]
        public int? TransferID { get; set; }

        // Indicate on entry to page, where to redirect after post 
        [BindProperty]
        public string ReturnPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string? rtrPage)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FirstOrDefaultAsync(m => m.ArticleId == id);

            if (article == null)
            {
                return NotFound();
            }

            Article = article;

            ViewData["ArticleSectionId"] = new SelectList(_context.ArticleSection, "ArticleSectionId", "ArticleSectionName");


            // If no returnpage is indicated, set to index, otherwise set field ReturnPage on page (hidden input)
            if (rtrPage != null)
                ReturnPage = rtrPage;
            else
                ReturnPage = "index";


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.ArticleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Redirect page indicated on load
            return RedirectToPage("./" + ReturnPage);
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }
    }
}
