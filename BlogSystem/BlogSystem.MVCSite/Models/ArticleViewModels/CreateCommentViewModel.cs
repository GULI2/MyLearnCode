using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Views.Article
{
    public class CreateCommentViewModel
    {
        public Guid ArticleId { get; set; }
        [Required]
        public string Comment { get; set; }

    }
}