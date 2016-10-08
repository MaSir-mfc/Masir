using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMVC.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            ViewBag.Message = "环境使用mvc";
            return View();
        }

        public string GetString()
        {
            return "世界你好，我现在开始使用你了";
        }

        public ActionResult GetView()
        {
            return View("MyView");
        }
	}
}