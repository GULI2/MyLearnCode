using BlogSystem.DAL;
using BlogSystem.Dto;
using BlogSystem.Models;
using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystemBLL
{
    public class ArticleManager : IArticleManager
    {
        public async Task CreateArticle(string title, string content, Guid[] categoryId, Guid userId)
        {

            using (var articleSev = new ArticleService())
            {
                Article article = new Article()
                {
                    Title = title,
                    Contents = content,
                    UserId = userId
                };
                await articleSev.CreateAsync(article);
                using (var articleToCategorySev = new ArticleToCategoryService())
                {
                    foreach (Guid each in categoryId)
                    {
                        await articleToCategorySev.CreateAsync(new ArticleToCategory()
                        {
                            BlogArticleId = article.Id,
                            BlogCategoryId = each
                        }, false);//每次循环先不保存
                    }
                    await articleToCategorySev.Save();//最后统一保存
                }
            }

        }

        public async Task CreateArticleCategory(string categoryName, Guid userId)
        {
            using (var categorySev = new BlogCategoryService())
            {
                await categorySev.CreateAsync(new BlogCategory()
                {
                    Category = categoryName,
                    UserId = userId
                });
            }
        }

        public async Task EditArticle(Guid ArticleId, string newTitle, string newContent, Guid[] categoryIds)
        {
            using (var articleSev = new ArticleService())
            {
                Article article = await articleSev.GetOneByIdAsync(ArticleId);
                article.Title = newTitle;
                article.Contents = newContent;
                await articleSev.EditAsync(article);
                using (var articleCategorySev = new BlogCategoryService())
                {

                }
            }
        }

        public async Task EditCategory(Guid CategoryId, string newCategoryName)
        {
            using (var categorySev = new BlogCategoryService())
            {
                await categorySev.EditAsync(new BlogCategory()
                {
                    Id = CategoryId,
                    Category = newCategoryName
                });
            }
        }

        public Task<List<ArticleDto>> GetAllArticlesByCategory(Guid CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleDto>> GetAllArticlesByUserEmail(string email)
        {
            throw new NotImplementedException();
        }



        public async Task<List<ArticleDto>> GetAllArticlesByUserId(Guid userId, int pageIndex, int pageSize)
        {
            using (IArticleService articleSev = new ArticleService())
            {
                var articleList = await articleSev.GetByPageOrderAsync(pageSize, pageIndex, false).Include(u => u.User)
                    .Where(u => u.UserId == userId).Select(u => new ArticleDto()
                    {
                        Title = u.Title,
                        Content = u.Contents,
                        BadCount = u.BadCount,
                        GoodCount = u.GoodCount,
                        Id = u.Id,
                        Email = u.User.Email,
                        ImgPath = u.User.ImgPath

                    }).ToListAsync();
                using (IArticleToCategoryService articleToCategorySev = new ArticleToCategoryService())
                {
                    foreach (var eachArticle in articleList)
                    {
                        var categoryList = articleToCategorySev.GetAllAsync()
                             .Include(u => u.BlogCategory)
                             .Where(u => u.BlogArticleId == eachArticle.Id)
                             .ToList();
                        eachArticle.CategoryIds = categoryList.Select(u => u.BlogCategoryId).ToArray();
                        eachArticle.CategoryNames = categoryList.Select(u => u.BlogCategory.Category).ToArray();
                    }
                    return articleList;
                }
            }
        }

        public async Task<List<BlogCategoryDto>> GetAllCategoriesByUser(Guid userId)
        {
            using (IBlogCategoryService categorySev = new BlogCategoryService())
            {
                return await categorySev.GetAllAsync().Where(u => u.UserId == userId).Select(u => new BlogCategoryDto()
                {
                    Id = u.Id,
                    CategoryName = u.Category
                }).ToListAsync();
            }
        }

        public Task RemoveArticle(Guid ArticleId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCategory(Guid CategoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistArticle(Guid ArticleId)
        {
            using (IArticleService articleSev = new ArticleService())
            {
                return await articleSev.GetAllAsync().AnyAsync(u => u.Id == ArticleId);
            }
        }
        public async Task<ArticleDto> GetArticleById(Guid ArticleId)
        {
            using (IArticleService articleSev = new ArticleService())
            {
                var article = await articleSev.GetAllAsync()
                   .Include(u => u.User)
                   .Where(u => u.Id == ArticleId)
                   .Select(u => new ArticleDto()
                   {
                       Title = u.Title,
                       Content = u.Contents,
                       Id = u.Id,
                       BadCount = u.BadCount,
                       GoodCount = u.GoodCount,
                       ImgPath = u.User.ImgPath,
                       Email = u.User.Email
                   }).FirstAsync();
                using (IArticleToCategoryService articleToCategorySev = new ArticleToCategoryService())
                {
                    var categoryList = articleToCategorySev.GetAllAsync()
                        .Include(u => u.BlogCategory).Where(u => u.BlogArticleId == ArticleId).ToList();
                    article.CategoryIds = categoryList.Select(u => u.BlogCategoryId).ToArray();
                    article.CategoryNames = categoryList.Select(u => u.BlogCategory.Category).ToArray();
                }
                return article;
            }
        }

        public async Task<int> GetTotalArticleCount(Guid userId)
        {
            using (IArticleService articleSev = new ArticleService())
            {
                return await articleSev.GetAllAsync().CountAsync(u=>u.UserId== userId);
            }
        }
    }
}
