using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zltNewsletter.Models;

namespace zltNewsletter.Data

{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Article> Article { get; set; } = default!;
        public DbSet<ArticleSection> ArticleSection { get; set; } = default!;



    }
}
