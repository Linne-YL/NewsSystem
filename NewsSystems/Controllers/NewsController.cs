using NewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Controllers
{
    public class NewsController : Controller
    {


        // GET: News
        public ActionResult Index()
        {
            if (Session["username"] == null) return View("Login");

            List<News> news = new NewsProvider().Select();
            return View(news);
        }
        public ActionResult Login()
        {
            return View();
        }


        /// <summary>
        /// 用户验证
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUser()
        {
            string username = Request["user"].ToString();
            string password = Request["pass"].ToString();
            MemberProvider memberProvider = new MemberProvider();
            var model = memberProvider.Select().FirstOrDefault(t => t.Name == username && t.Password == password);
            if (model != null)
            {
                Session["username"] = model.Name;
                Session["userId"] = model.Id;
                return Content("登录成功");
            }

            return Content("登录失败");

        }
        /// <summary>
        /// 删除一条新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelectNews(int id)
        {
            if (Session["username"] == null) return View("Login");
            var newProvide = new NewsProvider();
            var model = new NewsProvider().Select().FirstOrDefault(item => item.Id == id);
            if (model != null)
            {
                newProvide.Delete(model);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 编辑一条新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditNews(int id)
        {
            if (Session["username"] == null) return View("Login");
            var newProvide = new NewsProvider();
            var model = new NewsProvider().Select().FirstOrDefault(item => item.Id == id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 提交内容
        /// </summary>
        /// <returns></returns>
        public ActionResult EditNewsAction()
        {
            string id = Request["id"].ToString();
            string title = Request["title"].ToString();
            string desc = Request["desc"].ToString();

            if (id == null || int.TryParse(id, out int result) == false)
            {
                return Content("修改失败");
            }
            else if (string.IsNullOrEmpty(title) == true)
            {
                return Content("修改失败");
            }
            else if (string.IsNullOrEmpty(desc) == true)
            {
                return Content("修改失败");
            }
            var provider = new NewsProvider();
            var model = provider.Select().FirstOrDefault(t => t.Id == result);
            if (model != null)
            {
                model.Title = title;
                model.Text = desc;
                var count = provider.Update(model);
                if (count > 0)
                {
                    return Content("修改成功");
                }
                else
                {
                    return Content("修改失败");
                }
            }

            return Content("修改失败");
        }

        public ActionResult AddNews()
        {
            return View();
        }
        public ActionResult AddNewsAction()
        {
            ;
            string title = Request["title"].ToString();
            string desc = Request["desc"].ToString();
            if (string.IsNullOrEmpty(title) == true || string.IsNullOrEmpty(desc) == true)
            {
                return Content("修改失败");
            }
            var model = new News();
            var id = int.Parse(Session["userId"].ToString());
            model.Title = title;
            model.Text = desc;
            model.InsertDate = DateTime.Now;
            model.MemberId = id;
            model.MemberName = Session["username"].ToString();
            var count = new NewsProvider().Insert(model);
            if (count > 0)
            {
                return Content("添加成功");
            }
            else
            {
                return Content("添加失败");
            }
        }
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManagement()
        {
            if (Session["username"] == null)
            {
                return View("Login");
            }
            else
            {
                return View(new MemberProvider().Select());//返回一个泛型数组
            }

        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteMember(int id)
        {
            var provider = new MemberProvider();
            var model = new MemberProvider().Select().FirstOrDefault(item => item.Id == id);
            if (model != null)
            {
                provider.Delete(model);
            }
            return RedirectToAction("UserManagement");
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUserAction()
        {
            string username = Request["user"].ToString();
            string password = Request["pass"].ToString();
            MemberProvider memberProvider = new MemberProvider();
            var model = memberProvider.Select().FirstOrDefault(t => t.Name == username && t.Password == password);
            if (model == null)
            {
                var member = new Member();
                member.Name = username;
                member.Password = password;
                member.InsertDate = DateTime.Now;
                int count = memberProvider.Insert(member);
                if (count > 0)
                {
                    return Content("添加成功");
                }
                else
                {
                    return Content("添加失败");
                }
            }
            else
            {
                return Content("当前用户已存在");
            }

        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult ExitSystem()
        {
            Session["username"] = null;
            Session["password"] = null;
            return RedirectToAction("login");
        }
        public ActionResult NewsDetail(int id)
        {
            if (Session["username"] == null) return View("Login");

            var model = new NewsProvider().Select().FirstOrDefault(item => item.Id == id);
            if (model != null) return View(model);
            return RedirectToAction("Index");

        }
    }
    }
     

