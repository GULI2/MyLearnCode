using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.ArticleViewModels;
using BlogSystem.MVCSite.Views.Article;
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
        [BlogSystemAuth]//放在最上面对以下所有方法都起作用
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
        public async Task<ActionResult> CategoryList()
        {
            IArticleManager articleManager = new ArticleManager();
            Guid loginId = Guid.Parse(Session["loginId"].ToString());
            object model = await articleManager.GetAllCategoriesByUser(loginId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> CreateArticle()
        {
            //取出文章分类用于用户选择
            IArticleManager articleManager = new ArticleManager();
            Guid loginId = Guid.Parse(Session["loginId"].ToString());
            ViewBag.CategoryIds = await articleManager.GetAllCategoriesByUser(loginId);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]//提交时不检测潜在危险的request.form值
        public async Task<ActionResult> CreateArticle(CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var articleManager = new ArticleManager();
                await articleManager.CreateArticle(model.Title, model.Content, model.CategoryIds, Guid.Parse(Session["loginId"].ToString()));
                return RedirectToAction("ArticleList");
            }

            ModelState.AddModelError("", "添加失败！");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ArticleList(int pageIndex = 0, int pageSize = 10)
        {
            //分页：页面显示页码、当前第几页、总共页码数
            //需要获取文章总数，从而获取总页码数
            Guid userId = Guid.Parse(Session["loginId"].ToString());
            IArticleManager articleManager = new ArticleManager();
            int count = await articleManager.GetTotalArticleCount(userId);
            //ViewBag.totalPageCount = (count % pageSize == 0) ? count / pageSize : (count / pageSize + 1);//总共页码数
            //ViewBag.PageIndex = pageIndex;//当前第几页
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

            ViewBag.comment =await articleManager.GetCommentByArticleId(id.Value);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ArticleEdit(Guid Id)
        {
            var articleManager = new ArticleManager();
            var data = await articleManager.GetArticleById(Id); 
             Guid loginId = Guid.Parse(Session["loginId"].ToString());
            ViewBag.CategoryIds = await articleManager.GetAllCategoriesByUser(loginId);
            return View(new ArticleEditViewModel()
            {
                Id=data.Id,
                Title=data.Title,
                Content=data.Content,
                CategoryIds=data.CategoryIds
            });
        }

        [HttpPost]
        public async Task<ActionResult> ArticleEdit(ArticleEditViewModel model)
        {
            var articleManager = new ArticleManager();
            if (ModelState.IsValid)
            {
                await articleManager.EditArticle(model.Id, model.Title, model.Content, model.CategoryIds);
                return RedirectToAction("ArticleList");
            }
            else
            {
                Guid loginId = Guid.Parse(Session["loginId"].ToString());
                ViewBag.CategoryIds = await articleManager.GetAllCategoriesByUser(loginId);
                return View(model);
            }
        }

        [HttpPost]
        public  async Task<ActionResult> GoodCountAdd(Guid Id)
        {
            var articleManager = new ArticleManager();
            await articleManager.GoodCountAdd(Id);
            return Json(new { result="ok"});
        }

        [HttpPost]
        public async Task<ActionResult> BadCountAdd(Guid Id)
        {
            var articleManager = new ArticleManager();
            await articleManager.BadCountAdd(Id);
            return Json(new { result = "ok" });
        }

        [HttpPost]
        public async Task<ActionResult> AddComment( CreateCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var articleManager = new ArticleManager();
                await articleManager.CreateComment(model.ArticleId, Guid.Parse(Session["loginId"].ToString()), model.Comment);
                return Json(new { result = "OK" });
            }
            return View();
        }
    }
}