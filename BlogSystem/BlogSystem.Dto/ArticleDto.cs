using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        public string Content { get; set; }
         
        public string Email { get; set; }
        
        public int GoodCount { get; set; }
         
        public int BadCount { get; set; }
        public string ImgPath { get; set; }
        /// <summary>
        /// 分类标签
        /// </summary>
        public string[] CategoryNames { get; set; }
        public Guid[] CategoryIds { get; set; }

    }
}
