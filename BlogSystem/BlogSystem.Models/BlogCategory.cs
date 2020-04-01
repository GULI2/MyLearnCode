using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class BlogCategory:BaseEntity
    {
        /// <summary>
        /// 文章分类名称
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 用户外键
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; } 
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
    }
}
