using TmService.Services;
using System.Web.Mvc;

namespace TmService.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        // 登录验证 
        public JsonResult EdiLogin()
        {
            string user = Request.Params["user"];
            string pwd = Request.Params["pwd"];
            object resp = LoginService.LonntecLoginCheck(user, pwd);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}