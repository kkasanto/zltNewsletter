using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace zltNewsletter.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string ArticleTitle { get; set; }
        public string? ArticleHeader { get; set; }
        public string? ArticleText { get; set; }
        public bool ArticlePublished { get; set; } = true;
        public DateTime ArticleDateTime { get; set; } = DateTime.Now;
        public string? ArticleAuthor { get; set; }
        public int? ArticleSortOrder { get; set; }

        [ForeignKey(nameof(ArticleSection))]
        public int ArticleSectionId { get; set; }
        public virtual ArticleSection? ArticleSection { get; set; }

    }

    public class ArticleSection
    {

        [Key]
        public int ArticleSectionId { get; set; }

        [Required(ErrorMessage = "Section name is required")]
        public string ArticleSectionName { get; set; }
        public DateTime ArticleSectionCreateDate { get; set; } = DateTime.Now;
        public bool ArticleSectionActive { get; set; } = true;

        public ICollection<Article>? Articles { get; set; }
    }

}
