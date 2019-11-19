using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Fy.Common
{
    public class ConfigHelper
    {
        /// <summary>
        /// 检测是否存在appSettings 节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsExsistAppSetKey(string key)
        {
            var isExist = ConfigurationManager.AppSettings.AllKeys.Contains(key);
            return isExist;
        }

        #region 得到AppSettings中的配置字符串信息
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary> 
        /// <returns></returns>
        public static string GetAppSettingValue(string key)
        {
            var isExist = ConfigurationManager.AppSettings.AllKeys.Contains(key);
            if (!isExist)
            {
                throw new Exception($"appSettings节点配置不存在值：{key}");
            }
            return ConfigurationManager.AppSettings[key];
        }
        #endregion

        #region 获取 ConnectionStrings 中的配置字符串信息
        /// <summary>
        /// 获取 ConnectionStrings 中的配置字符串信息
        /// 一般用于数据库连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionStringValue(string key)
        {
            var settings = ConfigurationManager.ConnectionStrings[key];
            if (settings == null)
            {
                throw new Exception($"connectionStrings节点配置不存在值：{key}");

            }
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
        #endregion
    }
}
