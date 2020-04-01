using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class User:BaseEntity
    {
        [Required,StringLength(40),Column(TypeName = "varchar")]
        public string Email { get; set; }
        [Required, StringLength(30), Column(TypeName = "varchar")]
        public string Password { get; set; }
        //头像
        [Required,StringLength(300),Column(TypeName ="varchar")]
        public string ImgPath { get; set; }
        //粉丝数量
        public int FansCount { get; set; }
        //关注数量
        public int FoucsCount { get; set; }
        //博客名
        public string SiteName { get; set; }
    }
}
