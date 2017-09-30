/*----------------------------------------------------------------
 *  Copyright (C) 2017 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：AreaInfo
 *  所属项目：
 *  创建用户：马发才
 *  创建时间：2017/9/29 17:37:18
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

using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasirTest.Ajax
{
    public class AreaInfo
    {
        #region 属性

        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "area_id")]
        public int AreaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "area_name")]
        public string AreaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "parent_id")]
        public int ParentId { get; set; }
        
        #endregion
        /// <summary>
        /// 子类集合
        /// </summary>
        public IEnumerable<AreaInfo> Child { get; set; }

    }
}
