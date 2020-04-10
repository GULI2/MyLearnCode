using BlogSystem.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace IBLL
{
    public interface IArticleManager
    {
        Task CreateArticle(string title,string content,Guid[] categoryId,Guid userId);
        Task CreateArticleCategory(string categoryName,Guid userId);
        //根据用户查找文章类别
        Task<List<BlogCategoryDto>> GetAllCategoriesByUser(Guid userId);
        //根据用户查找文章
        Task<PagedList<ArticleDto>> GetAllArticlesByUserId(Guid userId, int pageIndex, int pageSize);
        Task<List<ArticleDto>> GetAllArticlesByUserEmail(string email);
        //获取文章总数--用于分页中计算总页码数
        Task<int> GetTotalArticleCount(Guid userId);
   
        //根据类别查找文章
        Task<List<ArticleDto>> GetAllArticlesByCategory(Guid CategoryId);

        Task RemoveCategory(Guid CategoryId);
        Task EditCategory(Guid CategoryId, string newCategoryName);
        Task RemoveArticle(Guid ArticleId);
        Task EditArticle(Guid ArticleId,string newTitle,string newContent,Guid[] categoryIds);

        Task<bool> ExistArticle(Guid ArticleId);
        Task<ArticleDto> GetArticleById(Guid ArticleId);

        Task GoodCountAdd(Guid ArticleId);
        Task BadCountAdd(Guid ArticleId);

        Task CreateComment(Guid ArticleId,Guid UserId,string Comment);
       Task<List<CommentDto>> GetCommentByArticleId(Guid ArticleId);
    }
}
