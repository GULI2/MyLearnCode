using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.ArticleViewModels;
using BlogSystemBLL;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.MVCSite.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [BlogSystemAuth]
        public async Task<ActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            //先数据校验
            //数据处理
            if (ModelState.IsValid)
            {
                IArticleManager articleManager = new ArticleManager();
                Guid loginId = Guid.Parse(Session["loginId"].ToString());
                await articleManager.CreateArticleCategory(model.CategoryName, loginId);
                return RedirectToAction("CategoryList");
            }
            ModelState.AddModelError("", "录入信息有误！");
            return View(model);
        }

        [HttpGet]
        [BlogSystemAuth]
        public async Task<ActionResult> CategoryList()
        {
            IArticleManager articleManager = new ArticleManager();
            Guid loginId = Guid.Parse(Session["loginId"].ToString());
            object model = await articleManager.GetAllCategoriesByUser(loginId);
            return View(model);
        }

        [HttpGet]
        [BlogSystemAuth]
        public async Task<ActionResult> CreateArticle()
        {
            //取出文章分类用于用户选择
            IArticleManager articleManager = new ArticleManager();
            Guid loginId = Guid.Parse(Session["loginId"].ToString());
            ViewBag.Category = await articleManager.GetAllCategoriesByUser(loginId);
            return View();
        }

        [HttpPost]
        [BlogSystemAuth]
        public async Task<ActionResult> CreateArticle(CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IArticleManager articleManager = new ArticleManager();
                await articleManager.CreateArticle(model.Title, model.Content, model.CategoryIds, Guid.Parse(Session["loginId"].ToString()));
                return RedirectToAction("ArticleList");
            }
            else
            {
                ModelState.AddModelError("", "添加失败！");
            }
            return View(model);
        }

        [HttpGet]
        [BlogSystemAuth]
        public async Task<ActionResult> ArticleList(int pageIndex=0, int pageSize=3)
        {
            //分页：页面显示页码、当前第几页、总共页码数
            //需要获取文章总数，从而获取总页码数
            Guid userId = Guid.Parse(Session["loginId"].ToString());
            IArticleManager articleManager = new ArticleManager();
            int count = await articleManager.GetTotalArticleCount(userId);
            ViewBag.totalPageCount = (count % pageSize==0) ? count / pageSize : (count / pageSize + 1);//总共页码数
            ViewBag.PageIndex = pageIndex;//当前第几页
            var result = await articleManager.GetAllArticlesByUserId(userId, pageIndex, pageSize);
            return View(result);
        }

        public async Task<ActionResult> ArticleDetail(Guid? id)
        {
            var articleManager = new ArticleManager();
            //防止用户修改地址栏的guid   //判断文章是否存在
            if (id == null || !(await articleManager.ExistArticle(id.Value)))
            {
                return RedirectToAction("ArticleList");
            }

            var model = await articleManager.GetArticleById(id.Value);
            return View(model);
        }
    }
}