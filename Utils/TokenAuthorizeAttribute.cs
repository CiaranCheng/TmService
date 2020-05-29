using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TmService.Utils
{
    public class TokenAuthorizeAttribute
    {
        public TokenAuthorizeAttribute(bool _isCheck = true)
        {
            this.isCheck = _isCheck;
        }

        private bool isCheck { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var actionDescription = filterContext.ActionDescriptor;

            if (actionDescription.IsDefined(typeof(AllowAnonymousAttribute), false) ||
                actionDescription.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false)) { return; }

            if (!isCheck) return;

            string tokenstr = "";

            string msg = string.Empty;
            string user = string.Empty;
            tokenstr = httpContext.Request.Headers["token"];//httpContext.Request.Params["token"];
            user = httpContext.Request.Params["user"];
            //LogHelper.Info("token-->" + tokenstr);
            //LogHelper.Info("tel-->" + tel);
            if (string.IsNullOrEmpty(tokenstr))
            {
                //filterContext.Result = new JsonResult()
                //{
                //    Data = new {   code = 401, msg = "token参数为空" },
                //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //};
                //return;
                filterContext.RequestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                ContentResult result = new ContentResult();
                result.Content = "未授权的请求:token参数为空";
                result.ContentType = "text/html";
                filterContext.Result = result;//new HttpUnauthorizedResult();
                //filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (!TokenHelper.ValidateToken(tokenstr, user))
            {
                //filterContext.Result = new JsonResult()
                //{
                //    Data = new { code = 401, msg = msg },
                //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //};
                //return;
                filterContext.RequestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                ContentResult result = new ContentResult();
                result.Content = "token无效";
                result.ContentType = "text/html";
                filterContext.Result = result;//new HttpUnauthorizedResult();
                return;
            }
        }
    }
}