using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;

namespace MasirTest.Components
{

    public class ListHelper<T> where T : new()
    {
        /// <summary>
        /// 数据表转实体
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static IList<T> DataTableToEntities(DataTable dataTable, Hashtable hash = null)
        {
            IList<T> enties = new List<T>();
            T model;
            var properties = typeof(T).GetProperties();
            foreach (DataRow row in dataTable.Rows)
            {
                model = new T();
                foreach (var item in properties)
                {
                    var propAttr = item.GetCustomAttributes(true);
                    if (propAttr.Length > 0)
                    {
                        var columnInfo = propAttr[0] as ColumnAttribute;

                        if (row.Table.Columns.Contains(columnInfo.Name))
                        {
                            if (DBNull.Value != row[columnInfo.Name])
                            {
                                item.SetValue(model, Convert.ChangeType(row[columnInfo.Name], item.PropertyType), null);
                            }
                        }
                        else if (hash != null && hash.ContainsKey(columnInfo.Name))
                        {//用于外部替换属性值
                            item.SetValue(model, Convert.ChangeType(hash[columnInfo.Name], item.PropertyType), null);
                        }
                    }
                }
                enties.Add(model);
            }
            return enties;
        }
        /// <summary>
        /// 实体转实体
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IList<T> JsonToEntities(string json)
        {
            var _array = JArray.Parse(json);
            return JsonToEntities(_array);
        }
        /// <summary>
        /// 实体转实体
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static IList<T> JsonToEntities(JArray jArray)
        {
            IList<T> enties = new List<T>();
            T model;
            var properties = typeof(T).GetProperties();
            foreach (var row in jArray)
            {
                model = new T();
                foreach (var item in properties)
                {
                    var propAttr = item.GetCustomAttributes(true);
                    if (propAttr.Length > 0)
                    {
                        var columnInfo = propAttr[0] as ColumnAttribute;
                        if (row[columnInfo.Name] != null)
                        {
                            item.SetValue(model, Convert.ChangeType(row[columnInfo.Name].ToString(), item.PropertyType), null);
                        }
                    }
                }
                enties.Add(model);
            }
            return enties;
        }

        public static IList<T> ObjectToList(IList<object> objArray)
        {
            IList<T> enties = new List<T>();
            T model;
            var properties = typeof(T).GetProperties();
            foreach (var obj in objArray)
            {
                model = new T();
                var objProp = obj.GetType().GetProperties();
                foreach (var item in properties)
                {
                    foreach (var itemObj in objProp)
                    {
                        if (item.Name == itemObj.Name)
                        {
                            item.SetValue(model, itemObj.GetValue(obj), null);
                        }
                    }
                }
                enties.Add(model);
            }
            return enties;
        }
    }
}
