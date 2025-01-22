using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using zltNewsletter.Data;

namespace webNewsletter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<zltNewsletter.Models.ArticleSection> ArtGroup { get; set; }

        public List<zltNewsletter.Models.Article> Last3Articles { get; set; }


        public void OnGet()
        {

            Last3Articles = _context.Article.Where(z => z.ArticlePublished == true)
                .OrderByDescending(z => z.ArticleDateTime)
                .Include(x => x.ArticleSection)
                .Take(3)
                .ToList();

            ArtGroup = _context.ArticleSection
               .Include(x => x.Articles.OrderBy(x => x.ArticleSortOrder))
               .OrderByDescending(z => z.ArticleSectionCreateDate)
               .ToList();

        }
    }
}
