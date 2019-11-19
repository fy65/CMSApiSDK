using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fy.SDK.Models
{
    /// <summary>
    /// 产品s
    /// </summary>
    public class ProductRequest
    { 
        /// <summary>
        /// 产品唯一Id
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
        /// 产品分类_Id
        /// </summary>
        public long ProductGroup_Id { get; set; }

        /// <summary>
        /// 产品标题
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品内容(使用编辑器 )
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 重发时间（列表显示这个）重发更新
        /// </summary>
        public DateTime RefreshTime { get; set; }
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
        /// 产品编号
        /// </summary>
        public string ProductNumber { get; set; }
        /// <summary>
        /// 市场价
        /// </summary>
        public double MarketPrice { get; set; }
        /// <summary>
        /// 价格，单价
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 折扣（0-100）
        /// </summary>
        public int Discount { get; set; }
        /// <summary>
        /// 折扣2
        /// </summary>
        //public int Discount2 { get; set; }
        /// <summary>
        /// 会员价
        /// </summary>
        public double MemberPrice { get; set; }
        /// <summary>
        /// 产品重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public string SaleCount { get; set; }

        /// <summary>
        /// 产品属性(特定分隔符)
        /// </summary>
        public List<ProductAttributeRequest> ProductAttributeList { get; set; }
        /// <summary>
        /// 纯文本内容简介（200字）
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 作者名称
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelePhone { get; set; }
        /// <summary>
        /// 自定义内容（自定义参数,多行参数）
        /// </summary>
        public string CustomContent { get; set; }

        /// <summary>
        /// 封面图（为了优化显示保存的时候处理）
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 产品图片集合
        /// </summary>
        public List<ProductPictureRequest> PictureList { get; set; }
    }
    public class ProductAttributeRequest
    {

        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ProductPictureRequest
    {
        /// <summary>
        /// 如果Id>0则修改图片
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 是否为封面图
        /// </summary>
        public bool IsCover { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片路径，支持远程链接
        /// </summary>
        public string Path { get; set; }
    }
}
