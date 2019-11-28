using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fy.SDK.Models
{
    public class ProductGroupRequest
    {
        #region 原始字段

        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 会员_Id
        /// </summary>
        public long MemberInfo_Id { get; set; }
        /// <summary>
        /// 语言版本
        /// </summary>
        public LanguageEnum LanguageEnum { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public long Parent_Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
         
        /// <summary>
        /// 图片1
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 图2,悬停图
        /// </summary>
        public string ImagePath2 { get; set; }
         
        /// <summary>
        /// SEO标题
        /// </summary>
        public string SeoTitle { get; set; }
        /// <summary>
        /// SEO关键字
        /// </summary>
        public string SeoKeyWords { get; set; }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string SeoDescription { get; set; }


        /// <summary>
        /// 纯文本内容简介（500字）
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 编辑器内容
        /// </summary>
        public string Content { get; set; }
         
        /// <summary>
        /// 排序 前端从小到大
        /// 前对对应  越小越靠前  可以为负数
        /// </summary>
        public int Sort { get; set; }
      

        #endregion
    }
}
