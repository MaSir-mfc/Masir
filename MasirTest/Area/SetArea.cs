﻿/*----------------------------------------------------------------
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
using MasirTest.Ajax;
using MasirTest.Components;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MasirTest.Area
{
    public class SetArea
    {
        public AreaType AreaType { get; set; }
        public string InsertSql
        {
            get
            {
                switch (AreaType)
                {
                    case AreaType.CITYAREA:
                        return "INSERT INTO [dbo].[area_city] ([area_name] ,[parent_id]) VALUES (@area_name,@parent_id) SELECT @@IDENTITY";
                    case AreaType.TOWNAREA:
                        return "INSERT INTO [dbo].[area_city_town] ([area_name] ,[parent_id]) VALUES (@area_name,@parent_id) SELECT @@IDENTITY";
                    default:
                        break;
                }
                return null;
            }
        }
        public string SelectSql
        {
            get
            {
                switch (AreaType)
                {
                    case AreaType.CITYAREA:
                        return "SELECT [area_id] ,[area_name] ,[parent_id] FROM [dbo].[area_city]";
                    case AreaType.TOWNAREA:
                        return "SELECT [area_id] ,[area_name] ,[parent_id] FROM [dbo].[area_city_town]";
                    default:
                        break;
                }
                return null;
            }
        }

        public SetArea() { }
        public SetArea(AreaType areaType)
        {
            AreaType = areaType;
        }

        public string GetJson()
        {
            return File.ReadAllText(string.Format("{0}/App_Data/{1}.json", AppDomain.CurrentDomain.BaseDirectory, AreaType));
        }
        public JArray GetArray()
        {
            var _json = GetJson();
            return JArray.Parse(_json);
        }

        #region 数据处理

        public void SetAreaToData()
        {
            using (MaDataHelper helper = MaDataHelper.GetDataHelper("MfcStudent"))
            {
                DoSqlFor(GetArray(), helper, 1);
            }
        }

        public void DoSqlFor(JToken jtoken, MaDataHelper helper, int parentId)
        {
            foreach (var child in jtoken)
            {
                int _parentId2 = DoSql(helper, child["name"].ToString(), parentId);
                DoSqlFor(child["child"], helper, _parentId2);
                switch (AreaType)
                {
                    case AreaType.CITYAREA:
                        break;
                    case AreaType.TOWNAREA:
                        if (Convert.ToInt32(child["id"]) > 0)
                        {
                            #region 从网页抓取名称并赋值
                            try
                            {
                                var _nameList = GetNameByWeb(string.Format("http://www.xzqh.org/html/list/{0}.html", child["id"]));
                                bool _start = false;
                                foreach (var item in _nameList)
                                {
                                    if (item.IndexOf("返回顶部") > -1)
                                    {
                                        break;
                                    }
                                    if (_start && item.IndexOf("行政区划") == -1)
                                    {
                                        DoSql(helper, item, _parentId2);
                                    }
                                    if (item.IndexOf("历史沿革") > -1)
                                    {
                                        _start = true;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                this.MaLogInfo(string.Format("{0}", ex));
                            }
                            #endregion
                        }
                        break;
                    default:
                        break;
                }
            }
        }



        public int DoSql(MaDataHelper helper, string name, int parentId)
        {
            helper.SpFileValue["@area_name"] = name;
            helper.SpFileValue["@parent_id"] = parentId;
            return Convert.ToInt32(helper.SqlScalar(InsertSql, helper.SpFileValue));
        }
        #endregion

        #region 从数据库获得json

        public DataTable GetArea()
        {
            using (MaDataHelper helper = MaDataHelper.GetDataHelper("MfcStudent"))
            {
                return helper.SqlGetDataTable(SelectSql);
            }
        }
        public IList<AreaInfo> GetAreaList()
        {
            return ListHelper<AreaInfo>.DataTableToEntities(GetArea());
        }
        private IList<AreaInfo> m_areaList;
        public AreaInfo GetAreaTreeList()
        {
            m_areaList = GetAreaList();
            return GetAllTreeChild(1);
        }
        public AreaInfo GetAllTreeChild(int parentId)
        {
            var _parentInfo = m_areaList.Where(a => a.AreaId == parentId).FirstOrDefault();
            var _childList = m_areaList.Where(a => a.ParentId == parentId);
            if (_childList.Count() > 0)
            {
                _parentInfo.Child = _childList;
                foreach (var item in _childList)
                {
                    GetAllTreeChild(item.AreaId);
                }
            }
            return _parentInfo;
        }

        public void GetAllChild(int parentId, ref List<AreaInfo> allChildList)
        {
            var _childList = m_areaList.Where(a => a.ParentId == parentId);
            if (_childList.Count() > 0)
            {
                allChildList.AddRange(_childList);
                foreach (var item in _childList)
                {
                    GetAllChild(item.AreaId, ref allChildList);
                }
            }
        }
        #endregion

        #region 从其他网页抓取名称

        public List<string> GetNameByWeb(string url)
        {
            WebClient _web = new WebClient();
            string _html = _web.DownloadStringTaskAsync(url).Result;
            List<string> keywords = new List<string>();
            Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
            MatchCollection mc = reg.Matches(_html);
            foreach (Match m in mc)
            {
                string keyword = Regex.Replace(m.Groups["text"].Value, "<[^>]*>", string.Empty).Replace("..", "").Replace("·", "").Replace("&nbsp;", "");

                if (keyword.Length > 0 && !keywords.Contains(keyword))
                {
                    keywords.Add(keyword);
                }
            }
            return keywords;
        }
        #endregion

    }

    public enum AreaType
    {
        /// <summary>
        /// 城市
        /// </summary>
        CITYAREA = 1,
        /// <summary>
        /// 乡镇
        /// </summary>
        TOWNAREA = 2
    }
}
