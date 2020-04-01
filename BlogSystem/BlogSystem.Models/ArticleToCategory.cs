using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class ArticleToCategory:BaseEntity
    {
        [ForeignKey(nameof(BlogCategory))]
        public Guid BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
        [ForeignKey(nameof(BlogArticle))]
        public Guid BlogArticleId { get; set; }
        public Article BlogArticle { get; set; }
    }
}
