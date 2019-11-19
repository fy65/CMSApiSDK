using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace Fy.Common
{
    public static class UtilHelper
    {

        #region 字符串转换
        /// <summary>
        /// 弃用方法，请用新方法 o.ToStr()
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        [Obsolete]//标记该方法已弃用
        public static string ConvertToString(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != string.Empty)
                {
                    return o.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 新版对象转字符串
        /// </summary> 
        /// <returns></returns>
        public static string ToStr(this object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != string.Empty)
                {
                    return o.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        public static string ToStr(this Stream stream)
        {
            try
            {
                if (null == stream)
                {
                    return string.Empty;
                }
                //stream.Position = 0;
                //byte[] byts = new byte[stream.Length];
                //stream.Read(byts, 0, byts.Length);
                //string req = System.Text.Encoding.Default.GetString(byts);
                //return req;
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 转换成 Int64
        /// <summary>
        /// 弃用方法，请用新方法 o.ToInt64();
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        [Obsolete]//标记该方法已弃用
        public static long ConvertToInt64(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    long num = Convert.ToInt64(o);
                    return num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        /// <summary>
        /// 对象转64位数字
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static long ToInt64(this object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    long num = Convert.ToInt64(o);
                    return num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 对象转double
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static double ToDouble(this object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    double num = Convert.ToDouble(o);
                    return num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        public static bool ToBool(this object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    bool b = Convert.ToBoolean(o);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 转换成   Int32
        /// <summary>
        /// 弃用方法，请用新方法 o.ToInt32();
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        [Obsolete]//标记该方法已弃用
        public static int ConvertToInt32(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    int num = Convert.ToInt32(o);
                    return num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 对象转32位数字
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        public static int ToInt32(this object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    int num = Convert.ToInt32(o);
                    return num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region 日期格式转换
        /// <summary>
        /// 转换成日期格式--转换失败返回当前默认时间
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    return Convert.ToDateTime(o);
                }
                else
                {
                    return default(DateTime);
                }
            }
            catch
            {
                return default(DateTime);
            }
        }
        /// <summary>
        /// 转换成日期格式--转换失败 返回null
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime2(this object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    return Convert.ToDateTime(o);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 日期字符串
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToDateTimeStr(this object o, string format = "yyyy-MM-dd HH:mm")
        {
            string str = "";
            try
            {

                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    if (o.ToStr() == "0001/1/1 0:00:00")
                    {
                        return str;
                    }
                    else
                    {
                        return Convert.ToDateTime(o).ToString(format, System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    }
                }
                else
                {
                    return str;
                }
            }
            catch
            {
                return str;
            }
        }
        #endregion

        #region JSON操作
        public static string ToJson(this object objData)
        {
            try
            {
                var iso = new IsoDateTimeConverter();
                iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                return JsonConvert.SerializeObject(objData, iso);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static T DeserializeObject<T>(this string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        #endregion
    }
}