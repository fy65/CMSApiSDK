using Fy.SDK;
using Fy.SDK.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMSApiTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void msg(string message)
        {
            txtMsg.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + message.ToString();
        }

        ProxyHelper proxyHelper = new ProxyHelper("0cef0ec20f75439a90de0714e125dc92", "8caf4f8e25824b9da18f6296ca96d9e7");
        long memberInfo_Id = 23191L;
        LanguageEnum languageEnum = LanguageEnum.中文;
         
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("只是一个测试按钮而已");
        }

        #region 注册账号

        private void button3_Click(object sender, EventArgs e)
        {
            var jsonResult = proxyHelper.CreateMemberAccount("fyjzdemo1", "Tkw123456");
            msg(jsonResult);
        }
        #endregion

        #region 获取代理可用的网站模板

        private void button7_Click(object sender, EventArgs e)
        {
            var jsonResult = proxyHelper.GetWebsiteTemplateList(1, 100);
            msg(jsonResult);
        }
        #endregion

        #region 选择一个模板=创建 更换模板任务

        private void button8_Click(object sender, EventArgs e)
        {
            long template_Id = 1;
            long memberInfo_id = 1;

            var jsonResult = proxyHelper.ChangeCaseTemplate(1, 2);
            msg(jsonResult);
        }
        #endregion

        #region 检测生成结果

        private void button10_Click(object sender, EventArgs e)
        {
            memberInfo_Id = 1491;
            var jsonResult = proxyHelper.CheckCreateTaskProgress(memberInfo_Id);
            msg(jsonResult);

        }

        #endregion

        #region 开通会员=设置会员级别

        private void button9_Click(object sender, EventArgs e)
        {
            memberInfo_Id = 1491;
            var jsonResult = proxyHelper.SetMemberLevel(memberInfo_Id, CMSMemberLevelEnum.入门版);
            msg(jsonResult);
        }
        #endregion

        #region 新增产品分类

        private void btn_AddProductGroup_Click(object sender, EventArgs e)
        {
            var request = new ProductGroupRequest();
            request.Id = 0;
            request.MemberInfo_Id = memberInfo_Id;
            request.LanguageEnum = languageEnum;
            request.Parent_Id = 0L;//
            request.Name = "分类名称"; //

            request.FilePath = "";//分类主图
            request.ImagePath2 = "";//分类悬停图

            request.SeoTitle = "";
            request.SeoKeyWords = "";
            request.SeoDescription = "";
            request.Summary = "";//检测
            request.SubTitle = "";//副标题
            request.Content = "";//内容


            var jsonResult = proxyHelper.SaveProductGroup(request);
            msg(jsonResult);
        }
        #endregion

        #region 获取所有产品分类

        private void button2_Click(object sender, EventArgs e)
        {
            var jsonResult = proxyHelper.GetProductGroupList(memberInfo_Id, languageEnum);//获取产品分类列表
            msg(jsonResult);
        }
        #endregion

        #region 获取单个产品分类明细

        private void button5_Click(object sender, EventArgs e)
        {
            var id = 15603;//产品分类Id
            var jsonResult = proxyHelper.GetProductGroupDetail(memberInfo_Id, languageEnum, id);//获取产品分类列表
            msg(jsonResult);
        }
        #endregion

        #region 新增产品

        private void button6_Click(object sender, EventArgs e)
        {

            //产品图片集合
            List<ProductPictureRequest> pictureList = new List<ProductPictureRequest>();
            pictureList.Add(new ProductPictureRequest() { IsCover = true, Name = "图片1-新增", Id = 0, Path = "https://at.alicdn.com/t/1568532643759.jpg" });
            pictureList.Add(new ProductPictureRequest() { IsCover = false, Name = "图片2-修改", Id = 10, Path = "https://img.alicdn.com/tfs/TB10uJLdW5s3KVjSZFNXXcD3FXa-2230-780.jpg" });

            //产品额外属性

            List<ProductAttributeRequest> attributeList = new List<ProductAttributeRequest>();
            attributeList.Add(new ProductAttributeRequest() { Name = "规格", Value = "长方体" });
            attributeList.Add(new ProductAttributeRequest() { Name = "颜色", Value = "红色" });

            ProductRequest product = new ProductRequest();
            product.Id = 0;//Id>0则为修改
            product.ProductGroup_Id = 15576L;//产品分类Id

            product.MemberInfo_Id = memberInfo_Id;//代理下的会员Id
            product.Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-产品标题";
            product.LanguageEnum = languageEnum;
            product.PictureList = pictureList; //产品图片集合
            product.ImagePath = "https://at.alicdn.com/t/1568532643759.jpg";//封面显示图
            product.ProductAttributeList = attributeList;
            product.Summary = "简介";
            product.CustomContent = "多行参数\r\n颜色：红色\r\n尺寸：100cm\r\n";//换行请用\r\n
            product.Content = "产品内容";
            product.AuthorName = "作者admin";
            product.Price = 10;//价格 
            product.Quantity = "1000";//库存
            product.Unit = "个";
            product.TelePhone = "0755";

            //独立的TDK
            product.SeoTitle = "";
            product.SeoKeyWords = "";
            product.SeoDescription = "";

            var jsonResult = proxyHelper.SaveProduct(product);
            msg(jsonResult);

        }



        #endregion

        
    }
}
