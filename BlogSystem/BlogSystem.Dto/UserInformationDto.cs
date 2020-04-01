using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    public class UserInformationDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string ImgPath { get; set; }
        public string SiteName { get; set; }
        public int FansCount { get; set; }
        public int FoucsCount { get; set; }
    }
}
