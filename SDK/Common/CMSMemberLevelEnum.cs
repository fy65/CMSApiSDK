using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fy.SDK
{ 
    public enum CMSMemberLevelEnum
    {
        免费会员 = 0,
        /// <summary>
        ///入门版 180元,空间=1GB，网站流量=1000IP/月，可绑定1个域名
        /// </summary>
        入门版 = 201,
        /// <summary>
        /// 标准版 360元 ,空间=2GB，网站流量=2000IP/月，可绑定2个域名
        /// </summary>
        标准版 = 220,
        /// <summary>
        /// 高级版 880元  ,空间=5GB，网站流量=不限，可绑定5个域名
        /// </summary>
        高级版 = 230,
        /// <summary>
        ///专业版 1999元  ,空间=10GB，网站流量=不限，可不限域名
        /// </summary>
        专业版 = 240,
        /// <summary>
        /// 高端定制版本   ,空间=不限，网站流量=不限，可不限域名
        /// </summary>
        高端定制 = 290, 
    }
}
