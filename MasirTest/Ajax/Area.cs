/*----------------------------------------------------------------
 *  Copyright (C) 2017 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：Area
 *  所属项目：
 *  创建用户：马发才
 *  创建时间：2017/9/29 13:13:41
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
using MasirTest.Area;
using MasirTest.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MasirTest.Ajax
{
    public class Area : PathHandler
    {
        public string SetAreaToData(HttpContext context)
        {
            var _form = context.Request.QueryString;
            AreaType _type;
            Enum.TryParse(_form["area"].ToUpper(), out _type);
            Task.Run(() =>
            {
                try
                {
                    new SetArea(_type).SetAreaToData();
                }
                catch (Exception ex)
                {
                    this.MaLogInfo(string.Format("{0}", ex));
                }
            });
            return JsonHelper.Json("请求成功", 0);
        }

        public string SetAreaToData2(HttpContext context)
        {
            var _form = context.Request.QueryString;
            AreaType _type;
            Enum.TryParse(_form["area"].ToUpper(), out _type);
            Task.Run(() =>
            {
                try
                {
                    new SetArea(_type).SetAreaToData();
                }
                catch (Exception ex)
                {
                    this.MaLogInfo(string.Format("{0}", ex));
                }
            });
            return JsonHelper.Json("请求成功", 0);
        }

        public string GetAreaTree(HttpContext context)
        {
            var _form = context.Request.QueryString;
            AreaType _type;
            Enum.TryParse(_form["area"].ToUpper(), out _type);
            var _dt = new SetArea(_type).GetAreaTreeList();
            return JsonHelper.Json(_dt);
        }


        public string SetAreaCodeToData(HttpContext context)
        {
            Task.Run(() =>
            {
                try
                {
                    new SetArea(AreaType.NATIONCITY).SetAreaCodeToData();
                }
                catch (Exception ex)
                {
                    this.MaLogInfo(string.Format("{0}", ex));
                }
            });
            return JsonHelper.Json("请求成功", 0);
        }
    }
}
