using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fy.Common
{
    public class SignatureHelper
    {
        /// <summary>
        /// 加密签名
        /// 通过 公钥 私钥 时间戳 将三个参数字符串拼接成一个字符串进行sha1加密
        /// </summary>
        /// <param name="accessKeyId"></param>
        /// <param name="accessKeySecret"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static string GetSignature(string accessKeyId, string accessKeySecret, string timestamp)
        {
            var arr = new[] { accessKeyId, accessKeySecret, timestamp }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            return enText.ToString();
        }


        #region 转换 需要post的数据

        public static string ConvertSignString(Dictionary<string, string> queries, string accessKeyId, string accessKeySecret)
        {
            StringBuilder str = new StringBuilder();
            var timestamp = TimeHelper.GetNowTimeStamp();
            var sign = SignatureHelper.GetSignature(accessKeyId, accessKeySecret, timestamp.ToStr());
             
            str.Append($"AccessKeyId={accessKeyId}&sign={sign}&timestamp={timestamp}");
            foreach (var p in queries)
            {
                str.Append("&").Append(p.Key).Append("=").Append(p.Value);
            }

            return str.ToStr();
        }

        #endregion
    }
}
