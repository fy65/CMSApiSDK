using Fy.Common;
using Fy.SDK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fy.SDK
{
    /// <summary>
    /// 代理相关接口
    /// </summary>
    public class ProxyHelper
    {
        HttpHelper httpHelper = new HttpHelper();
        private string apiDomain = "";
        public string AccessKeyId { get; set; } //"123";
        public string AccessKeySecret { get; set; }// "456";

        public ProxyHelper()
        {
            this.AccessKeyId = ConfigHelper.GetAppSettingValue("accessKeyID");
            this.AccessKeySecret = ConfigHelper.GetAppSettingValue("accessKeySecret");
            apiDomain = ConfigHelper.GetAppSettingValue("apiDomain");
        }

        #region 获取网站模板列表

        /// <summary>
        /// 获取网站模板列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="pageSize">数量</param>
        /// <returns></returns>
        public string GetWebsiteTemplateList(int page, int pageSize)
        {
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("page", page.ToStr());
            postParameters.Add("pagesize", pageSize.ToStr());

            var url = $"{apiDomain}/api/Proxy/GetWebsiteTemplateList";
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值 
                Postdata = postData
            });
            return result.Html;
        }

        #endregion

        #region 创建一个免费会员会员账号

        /// <summary>
        /// 创建一个免费会员会员账号 
        /// </summary>
        /// <param name="accountName">
        /// 1、账号长度6-20字符
        /// 2、账号只能是小写字母或数字
        /// 3、账号不能是11位纯数字
        /// 4、账号结尾不能含有\"wap\"字样（持续更新）
        /// </param>
        /// <param name="password">
        /// 1、接口密码需要明文
        /// 2、密码由8-20个字符组成，区分大小写
        /// 3、密码必须包含字母和数字
        /// </param>
        /// <returns>
        /// 返回会员Id 
        /// </returns>
        public string CreateMemberAccount(string accountName, string password)
        {
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("accountName", accountName);
            postParameters.Add("password", password);

            var url = $"{apiDomain}/api/Proxy/CreateMemberAccount";
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值 
                Postdata = postData
            });
            return result.Html;
        }

        #endregion

        #region 创建 更换模板任务

        /// <summary>
        /// 创建 更换模板任务
        /// 1、更换模板只限于免费会员
        /// 2、不能重复创建任务
        /// </summary>
        /// <param name="template_Id">模板Id</param>
        /// <param name="memberInfo_id">会员Id</param>
        /// <returns>
        /// code=0:创建任务失败
        /// code=200：创建任务成功
        /// </returns>
        public string ChangeCaseTemplate(long template_Id, long memberInfo_id)
        {
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("template_Id", template_Id.ToStr());
            postParameters.Add("memberInfo_id", memberInfo_id.ToStr());

            var url = $"{apiDomain}/api/Proxy/ChangeCaseTemplate";
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Postdata = postData
            });
            return result.Html;
        }

        #endregion

        #region 检测生成网站状态
        /// <summary>
        /// 检测创建网站结果 
        /// </summary>
        /// <param name="memberInfo_Id">会员Id</param>
        /// <returns>
        /// code=0:查询失败
        /// code=200：查询成功
        /// data=0:创建中
        /// data=1：创建成功
        /// </returns>
        public string CheckCreateTaskProgress(long memberInfo_Id)
        {
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("memberInfo_id", memberInfo_Id.ToStr());

            var url = $"{apiDomain}/api/Proxy/CheckCreateTaskProgress";
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Postdata = postData
            });
            return result.Html;
        }
        #endregion

        #region 设置会员级别

        /// <summary>
        /// 设置会员级别
        /// </summary>
        /// <param name="memberInfo_Id"></param>
        /// <param name="memberLevel"></param>
        /// <param name="openTime">开通时间</param>
        /// <param name="expireTime">到期时间</param>
        /// <returns>
        /// code=0:设置失败
        /// code=200：设置成功
        /// </returns>
        public string SetMemberLevel(long memberInfo_Id, CMSMemberLevelEnum memberLevel, DateTime openTime, DateTime expireTime)
        {
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("memberInfo_id", memberInfo_Id.ToStr());
            postParameters.Add("memberLevel", ((int)memberLevel).ToStr());

            postParameters.Add("opentime", openTime.ToDateTimeStr());
            postParameters.Add("expiretime", expireTime.ToDateTimeStr());

            var url = $"{apiDomain}/api/Proxy/SetMemberLevel";
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Postdata = postData
            });
            return result.Html;
        }
        #endregion

        #region 保存产品信息

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns>
        /// code=0:保存失败
        /// code=200：保存成功
        /// data=0:产品Id 
        /// </returns>
        public string SaveProduct(ProductRequest product)
        {
            var url = $"{apiDomain}/api/Proxy/SaveProduct";
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("product", product.ToJson());
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值 
                Postdata = postData,
            });
            return result.Html;
        }

        #endregion

        #region 保存产品分类

        /// <summary>
        /// 保存产品分类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string SaveProductGroup(ProductGroupRequest request)
        {
            var json = request.ToJson();
            var url = $"{apiDomain}/api/Proxy/SaveProductGroup";
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("jsonParam", request.ToJson());
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                //PostDataType = PostDataType.Byte,
                Postdata = postData,
            });
            return result.Html;
        }
        #endregion

        #region 获取产品分类集合

        /// <summary>
        /// 获取产品分类集合
        /// </summary>
        /// <param name="memberInfo_id"></param>
        /// <param name="languageEnum"></param>
        /// <returns></returns>
        public string GetProductGroupList(long memberInfo_id, LanguageEnum languageEnum)
        {
            var url = $"{apiDomain}/api/Proxy/GetProductGroupList";
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("memberInfo_id", memberInfo_id.ToStr());
            postParameters.Add("languageEnum", ((int)languageEnum).ToStr());

            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Postdata = postData,
            });
            return result.Html;
        }

        #endregion

        #region 获取单个产品详情

        /// <summary>
        /// 获取单个产品详情
        /// </summary>
        /// <param name="memberInfo_id"></param>
        /// <param name="languageEnum"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetProductGroupDetail(long memberInfo_id, LanguageEnum languageEnum, long id)
        {
            var url = $"{apiDomain}/api/Proxy/GetProductGroupDetail";
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("memberInfo_id", memberInfo_id.ToStr());
            postParameters.Add("languageEnum", ((int)languageEnum).ToStr());
            postParameters.Add("id", id.ToStr());

            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Postdata = postData,
            });
            return result.Html;
        }

        #endregion

        #region MyRegion
        public string GetMemberDomainList(long memberInfo_id)
        {
            var url = $"{apiDomain}/api/Proxy/GetMemberDomainList";
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("memberinfo_id", memberInfo_id.ToStr());

            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Postdata = postData,
            });
            return result.Html;
        }
        #endregion

    }
}
