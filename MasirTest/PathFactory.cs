/*----------------------------------------------------------------
 *  Copyright (C) 2017 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：PathFactory
 *  所属项目：
 *  创建用户：马发才
 *  创建时间：2017/9/29 13:10:56
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

using MasirTest.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MasirTest
{
    public class PathFactory : IHttpHandlerFactory
    {
        const string m_ProjectName = "MasirTest";
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            var classPath = m_ProjectName + url.Replace('/', '.');
            Type handlerType = System.Web.Compilation.BuildManager.GetType(classPath, false, true);
            if (handlerType != null)
            {
                var handler = Activator.CreateInstance(handlerType) as PathHandler;
                return handler;
            }
            context.Response.Write(JsonHelper.Json("请求路径错误", 1001));
            return null;
        }

        public void ReleaseHandler(IHttpHandler handler) { }
    }
}
