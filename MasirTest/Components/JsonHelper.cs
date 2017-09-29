/*----------------------------------------------------------------
 *  Copyright (C) 2017 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：JsonHelper
 *  所属项目：
 *  创建用户：马发才
 *  创建时间：2017/9/29 11:13:15
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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasirTest.Components
{
    public static class JsonHelper
    {

        public static string Json(string msg, int errcode)
        {
            JObject _token = new JObject();
            _token["errcode"] = errcode;
            _token["msg"] = msg;
            return _token.ToString();
        }
        public static string Json(string msg, int errcode, object data)
        {
            JObject _token = new JObject();
            _token["errcode"] = errcode;
            _token["msg"] = msg;
            _token["data"] = JObject.FromObject(data);
            return _token.ToString();
        }
    }
}
