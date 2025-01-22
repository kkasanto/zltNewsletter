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
    public class viewarticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public viewarticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FirstOrDefaultAsync(m => m.ArticleId == id);

            if (article is not null)
            {
                Article = article;

                return Page();
            }

            return NotFound();
        }
    }
}
