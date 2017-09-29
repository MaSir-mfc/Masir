/*----------------------------------------------------------------
 *  Copyright (C) 2017 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：PathHandler
 *  所属项目：
 *  创建用户：马发才
 *  创建时间：2017/9/29 13:11:06
 *  
 *  功能描述：
 *          1、
 *          2、
 * 
 *  修改标识：
 *  修改描述：
 *  待 完 善：
 *          1、
----------------------------------------------------------------*/

using Masir;
using MasirTest.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace MasirTest
{
    public class PathHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            LitJson.JsonData _json = new LitJson.JsonData();
            _json["success"] = false;
            try
            {
                context.Response.ContentType = "text/plain";
                string methodName = context.Request.PathInfo.Replace("/", "");

                MethodInfo method = this.GetType().GetMethod(methodName, BindingFlags.Instance
                        | BindingFlags.IgnoreCase
                        | BindingFlags.Public);

                if (method == null) { context.Response.Write(JsonHelper.Json("请求的处理函数不存在",1002)); return; }
                var fun = (Func<HttpContext, object>)method.CreateDelegate(typeof(Func<HttpContext, object>), this);
                context.Response.Write(fun(context));
            }
            catch (Exception ex)
            {
                this.MaLogError(string.Format("调用函数内部错误：{0}，描述：{1}", ex.Message, ex));
                context.Response.Write(JsonHelper.Json(ex.Message,1003));
            }
        }
        public bool IsReusable { get { return false; } }
    }
}
