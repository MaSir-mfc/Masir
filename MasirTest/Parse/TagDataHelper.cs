using Masir.Web.Parse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasirTest.Parse
{
    public class TagDataHelper
    {
        public TagEntity Tag { get; set; }
        public MaTag pTag { get; set; }

        private int m_totalCount;
        public int TotalCount
        {
            get { return m_totalCount; }
            set { m_totalCount = value; }
        }


        public TagDataHelper(TagEntity tag, MaTag pTag)
        {
            this.Tag = tag;
            this.pTag = pTag;
        }

        public DataTable GetDataTable(string methodName)
        {
            MethodInfo method = this.GetType().GetMethod(methodName, BindingFlags.Instance
                 | BindingFlags.IgnoreCase
                 | BindingFlags.Public);

            var fun = (Func<DataTable>)method.CreateDelegate(typeof(Func<DataTable>), this);

            return fun();
        }

        public DataTable NewsByClass()
        {
            int _currentPage = Convert.ToInt32(pTag["page"]);
            string _where = string.IsNullOrEmpty(Tag.Where) ? "" : " WHERE " + Tag.Where;
            string _order = string.IsNullOrEmpty(Tag.Order) ? "" : " ORDER BY " + Tag.Order;

            int _childClass = Convert.ToInt32(pTag["ChildClass"]);
            int _parentClass = Convert.ToInt32(pTag["ParentClass"]);

            if (_childClass != 0)
            {
                if (_parentClass == 0)
                {
                    string _classWhere = " [ParentClassId]=" + _childClass;
                    _where += _where == "" ? " WHERE " + _classWhere : " AND " + _classWhere;
                }
                else
                {
                    string _classWhere = " [NewsClassId]=" + _childClass + " AND [ParentClassId]=" + _parentClass;
                    _where += _where == "" ? " WHERE " + _classWhere : " AND " + _classWhere;
                }
            }

            var _dt = Masir.Data.DataEntityHelper.GetTable(
                Tag.Database,
                Tag.Table,
                _where,
                new Hashtable(),
                "*",
                _order,
                (_currentPage - 1) * int.Parse(Tag.Count),
                int.Parse(Tag.Count),
                out m_totalCount);
            return _dt;
        }

        #region 根据资讯分类获取数据（修改后，未启用)

        ///// <summary>
        ///// 资讯innerjoin测试分页
        ///// </summary>
        ///// <returns></returns>
        //public DataTable NewsByClass()
        //{
        //    int _currentPage = Convert.ToInt32(pTag["page"]);
        //    string _where = string.IsNullOrEmpty(Tag.Where) ? "" : " WHERE " + Tag.Where;
        //    string _order = string.IsNullOrEmpty(Tag.Order) ? "" : " ORDER BY " + Tag.Order;

        //    int _classId = Convert.ToInt32(pTag["Class"]);

        //    if (_classId == 0)
        //    {
        //        return NewsAll();
        //    }

        //    string _sql = "select row_number() over ( " + _order + " ) TID, A.* from [brand_news] A INNER JOIN [Fn_ZDL_NewsChildClass](" + _classId + ") B ON A.SearchClassID=B.class_id " + _where;
        //    int _totalCount = 0;

        //    DataTable _dt = DataEntityExtend.GetJoinTable(Tag.Database,
        //        _sql,
        //        (_currentPage - 1) * int.Parse(Tag.Count),
        //        int.Parse(Tag.Count),
        //        out _totalCount);

        //    return _dt;
        //}
        //public DataTable NewsAll()
        //{
        //    int _brandId = Convert.ToInt32(pTag["BrandId"]);

        //    int _currentPage = Convert.ToInt32(pTag["page"]);
        //    string _where = " WHERE BrandId=" + _brandId;
        //    string _order = " ORDER BY " + Tag.Order;

        //    var _dt = Maooo.Data.Entity.DataEntityHelper.GetTable(
        //        Tag.Database,
        //        Tag.Table,
        //        _where,
        //        new Hashtable(),
        //        "*",
        //        _order,
        //        (_currentPage - 1) * int.Parse(Tag.Count),
        //        int.Parse(Tag.Count),
        //        out m_totalCount);
        //    return _dt;
        //}

        #endregion

        public DataTable ProductByClass()
        {
            int _currentPage = Convert.ToInt32(pTag["page"]);
            string _where = string.IsNullOrEmpty(Tag.Where) ? "" : " WHERE " + Tag.Where;
            string _order = string.IsNullOrEmpty(Tag.Order) ? "" : " ORDER BY " + Tag.Order;

            int _parentClass = Convert.ToInt32(pTag["ParentClass"]);
            int _childClass = Convert.ToInt32(pTag["ChildClass"]);

            if (_childClass != 0)
            {
                if (_parentClass == 0)
                {
                    string _classWhere = "[productClass_parent_id]=" + _childClass;
                    _where += _where == "" ? " WHERE " + _classWhere : " AND " + _classWhere;
                }
                else
                {
                    string _classWhere = " [productClass_id]=" + _childClass + " AND [productClass_parent_id]=" + _parentClass;
                    _where += _where == "" ? " WHERE " + _classWhere : " AND " + _classWhere;
                }
            }

            var _dt = Masir.Data.DataEntityHelper.GetTable(
                Tag.Database,
                Tag.Table,
                _where,
                new Hashtable(),
                "*",
                _order,
                (_currentPage - 1) * int.Parse(Tag.Count),
                int.Parse(Tag.Count),
                out m_totalCount);
            return _dt;
        }

        /// <summary>
        /// 获取门店列表根据城市或关键字
        /// </summary>
        /// <returns></returns>
        public DataTable StoreListByArea()
        {
            int _currentPage = 1;// Convert.ToInt32(pTag["page"]);
            string _where = string.IsNullOrEmpty(Tag.Where) ? " WHERE 1=1 " : " WHERE " + Tag.Where;
            string _order = string.IsNullOrEmpty(Tag.Order) ? "" : " ORDER BY " + Tag.Order;

            int _area2 = Convert.ToInt32(pTag["area2"]);
            string _outletName = pTag["outlet_name"];

            if (_area2 != 0)
            {
                _where += string.Format(" AND (area1={0} or area2={1}) ", _area2, _area2);
            }
            if (!string.IsNullOrEmpty(_outletName))
            {
                _where += string.Format(" AND outlet_name like '%{0}%'", _outletName);
            }

            var _dt = Masir.Data.DataEntityHelper.GetTable(
               Tag.Database,
               Tag.Table,
               _where,
               new Hashtable(),
               "*",
               _order,
               (_currentPage - 1) * int.Parse(Tag.Count),
               int.Parse(Tag.Count),
               out m_totalCount);
            return _dt;
        }
    }
}
