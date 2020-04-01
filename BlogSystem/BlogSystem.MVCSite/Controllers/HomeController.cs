using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.UserViewModels;
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
    public class HomeController : Controller
    {
        [BlogSystemAuth]
        public ActionResult Index()
        {
            return View();
        }
        [BlogSystemAuth]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [BlogSystemAuth]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #region 注册
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//对数据进行增删改时要防止csrf攻击！
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUserManager userManager = new UserManager();
                await userManager.Register(model.Email, model.PassWord);
                return Content("注册成功");
            }
            return View(model);
        }
        #endregion

        #region 登录
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult  Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid userId;
                IUserManager user = new UserManager();
                if (user.Login(model.LoginName, model.LoginPwd, out userId))
                {
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.LoginName,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("loginId")
                        {
                            Value = userId.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.LoginName;
                        Session["loginId"] = userId;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "账号或密码不正确！");
                }
            }
            return View(model);
        }
        #endregion
    }
}