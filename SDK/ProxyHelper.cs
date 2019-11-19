using Fy.Common;
using Fy.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private string apiDomain ="";
        public string AccessKeyId { get; set; } //"123";
        public string AccessKeySecret { get; set; }// "456";
        public ProxyHelper(string keyId, string secret)
        {
            this.AccessKeyId = keyId;
            this.AccessKeySecret = secret;
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
        /// <param name="accountName">账号 4-20字符，账号只能是小写字母或数字</param>
        /// <param name="password">明文：密码由8-20个字符组成，区分大小写，密码必须包含字母和数字</param>
        /// <returns></returns>
        public string CreateMemberAccount(string accountName, string password)
        {
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("accountname", accountName);
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
        /// </summary>
        /// <param name="template_Id">模板Id</param>
        /// <param name="memberInfo_id">会员Id</param>
        /// <returns></returns>
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
        public string SetMemberLevel(long memberInfo_Id, CMSMemberLevelEnum memberLevel)
        {
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("memberInfo_id", memberInfo_Id.ToStr());
            postParameters.Add("memberLevel", ((int)memberLevel).ToStr());

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
                ContentType = "application/x-www-form-urlencoded;",//返回类型    可选项有默认值 
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
            var url = $"{apiDomain}/api/Proxy/SaveProductGroup";
            Dictionary<String, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("jsonParam", request.ToJson());
            var postData = SignatureHelper.ConvertSignString(postParameters, AccessKeyId, AccessKeySecret);

            var result = httpHelper.GetHtml(new HttpItem()
            {
                URL = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded;",
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
                ContentType = "application/x-www-form-urlencoded;",
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
                ContentType = "application/x-www-form-urlencoded;",
                Postdata = postData,
            });
            return result.Html;
        }

        #endregion

    }
}
