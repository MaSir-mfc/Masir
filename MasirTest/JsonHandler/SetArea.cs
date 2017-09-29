/*----------------------------------------------------------------
 *  Copyright (C) 2017 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：Area
 *  所属项目：
 *  创建用户：马发才
 *  创建时间：2017/9/29 9:23:46
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
using Masir.Web.Ajax;
using MasirTest.Components;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MasirTest.JsonHandler
{
    public class SetArea 
    {
        #region 数据处理
        public string GetJson()
        {
            return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/area.json");
        }
        public JArray GetArray()
        {
            var _json = GetJson();
            return JArray.Parse(_json);
        }

        public void SetAreaToData()
        {
            using (MaDataHelper helper = MaDataHelper.GetDataHelper("MfcStudent"))
            {
                string _sql = "INSERT INTO [dbo].[area_index] ([area_name] ,[parent_id]) VALUES (@area_name,@parent_id) SELECT @@IDENTITY";
                DoSqlFor(GetArray(), helper, _sql, 1);
            }
        }

        public void DoSqlFor(JToken jtoken, MaDataHelper helper, string sql, int parentId)
        {
            foreach (var child in jtoken)
            {
                int _parentId2 = DoSql(helper, sql, child["name"].ToString(), parentId);
                DoSqlFor(child["child"], helper, sql, _parentId2);
            }
        }

        public int DoSql(MaDataHelper helper, string sql, string name, int parentId)
        {
            helper.SpFileValue["@area_name"] = name;
            helper.SpFileValue["@parent_id"] = parentId;
            return Convert.ToInt32(helper.SqlScalar(sql, helper.SpFileValue));
        }
        #endregion
    }
}
