using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Models;
using IDAL;

namespace BlogSystem.DAL
{
    public class CommentService : BaseService<Models.Comment>,ICommentService
    {
        public CommentService() : base(new BlogContext())
        {
        }
    }
}
