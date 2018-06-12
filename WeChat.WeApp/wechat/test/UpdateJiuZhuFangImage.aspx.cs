using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;

namespace SPACRM.WebApp.wechat.test
{
    public partial class UpdateJiuZhuFangImage : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Pet_JiuZhu_Info> list = nvbo.GetAllJiuZhuList();
            foreach (var j in list)
            {
                try
                {
                    if (j.Img1 != null && j.CImg == null)
                        j.CImg = CutImage2(j.Img1, 1, j.CImg, true, false); //截一张长图，八张方图，一张logo
                    //if (j.Img1 != null && j.SImg1 == null)
                        j.SImg1 = CutImage2(j.Img1, 2,j.SImg1);
                    //if (j.Img2 != null && j.SImg2 == null)
                        j.SImg2 = CutImage2(j.Img2, 2, j.SImg2);
                    //if (j.Img3 != null && j.SImg3 == null)
                        j.SImg3 = CutImage2(j.Img3, 2, j.SImg3);
                    //if (j.Img4 != null && j.SImg4 == null)
                        j.SImg4 = CutImage2(j.Img4, 2, j.SImg4);
                    //if (j.Img5 != null && j.SImg5 == null)
                        j.SImg5 = CutImage2(j.Img5, 2, j.SImg5);
                    //if (j.Img6 != null && j.SImg6 == null)
                        j.SImg6 = CutImage2(j.Img6, 2, j.SImg6);
                    //if (j.Img7 != null && j.SImg7 == null)
                        j.SImg7 = CutImage2(j.Img7, 2, j.SImg7);
                    //if (j.Img8 != null && j.SImg8 == null)
                        j.SImg8 = CutImage2(j.Img8, 2, j.SImg8);
                        string himg = CutImage2(j.HeadImage, 2, j.HeadImage, true, false);
                    if (himg != j.HeadImage && himg != "")
                        j.HeadImage = WebUrl + himg;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "1<br>");
                }
                try
                {


                    nvbo.SaveJiuZhuInfo(j);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "2<br>");
                }


            }
            Response.Write("ok");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(int.Parse(TextBox1.Text));
            if (j != null)
            {
                try
                {
                    //if(j.Img1!=null&&j.CImg==null)
                    j.CImg = CutImage2(j.Img1, 1, j.CImg, true, false); //截一张长图，八张方图，一张logo
                    //if (j.Img1 != null && j.SImg1 == null)
                    j.SImg1 = CutImage2(j.Img1, 2, j.SImg1);
                    //if (j.Img2 != null && j.SImg2 == null)
                    j.SImg2 = CutImage2(j.Img2, 2, j.SImg2);
                    //if (j.Img3 != null && j.SImg3 == null)
                    j.SImg3 = CutImage2(j.Img3, 2, j.SImg3);
                    //if (j.Img4 != null && j.SImg4 == null)
                    j.SImg4 = CutImage2(j.Img4, 2, j.SImg4);
                    //if (j.Img5 != null && j.SImg5 == null)
                    j.SImg5 = CutImage2(j.Img5, 2, j.SImg5);
                    //if (j.Img6 != null && j.SImg6 == null)
                    j.SImg6 = CutImage2(j.Img6, 2, j.SImg6);
                    //if (j.Img7 != null && j.SImg7 == null)
                    j.SImg7 = CutImage2(j.Img7, 2, j.SImg7);
                    //if (j.Img8 != null && j.SImg8 == null)
                    j.SImg8 = CutImage2(j.Img8, 2, j.SImg8);
                    string himg = CutImage2(j.HeadImage, 2, j.HeadImage, true, false);
                    if (himg != j.HeadImage && himg != "")
                        j.HeadImage = WebUrl + himg;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "1<br>");
                }
                try
                {


                    nvbo.SaveJiuZhuInfo(j);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "2<br>");
                }


            }
            Response.Write("ok");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            List<Pet_JiuZhu_Info> list = nvbo.GetAllJiuZhuList();
            foreach (var j in list)
            {
                try
                {
                    j.CImg = CutImage2(j.Img1, 1, j.CImg, true, false); //截一张长图，八张方图，一张logo
                        j.SImg1 = CutImage2(j.Img1, 2,j.SImg1);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "1<br>");
                }
                try
                {


                    nvbo.SaveJiuZhuInfo(j);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "2<br>");
                }


            }
            Response.Write("ok");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            List<Pet_JiuZhu_News> list = nvbo.GetAllJiuZhuNewsList();
            foreach (var j in list)
            {
                try
                {
                    if (j.Img1 != null && j.SImg1 == null)
                    j.SImg1 = CutImage2(j.Img1, 2,j.SImg1);
                    if (j.Img2 != null && j.SImg2 == null)
                        j.SImg2 = CutImage2(j.Img2, 2, j.SImg2);
                    if (j.Img3 != null && j.SImg3 == null)
                        j.SImg3 = CutImage2(j.Img3, 2, j.SImg3);
                    if (j.Img4 != null && j.SImg4 == null)
                        j.SImg4 = CutImage2(j.Img4, 2, j.SImg4);
                    if (j.Img5 != null && j.SImg5 == null)
                        j.SImg5 = CutImage2(j.Img5, 2, j.SImg5);
                    if (j.Img6 != null && j.SImg6 == null)
                        j.SImg6 = CutImage2(j.Img6, 2, j.SImg6);
                    if (j.Img7 != null && j.SImg7 == null)
                        j.SImg7 = CutImage2(j.Img7, 2, j.SImg7);
                    if (j.Img8 != null && j.SImg8 == null)
                        j.SImg8 = CutImage2(j.Img8, 2, j.SImg8);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "1<br>");
                }
                try
                {
                    nvbo.SaveJiuZhuNews(j);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "2<br>");
                }
            }
            Response.Write("ok");
        }

        /// <summary>
        /// 所有项目添加水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button5_Click(object sender, EventArgs e)
        {
            List<Pet_JiuZhu_Info> list = nvbo.GetAllJiuZhuList();
            foreach (var j in list)
            {
                try
                {
                    ImageAddShuiYin(j.Img1);
                    ImageAddShuiYin(j.Img2);
                    ImageAddShuiYin(j.Img3);
                    ImageAddShuiYin(j.Img4);
                    ImageAddShuiYin(j.Img5);
                    ImageAddShuiYin(j.Img6);
                    ImageAddShuiYin(j.Img7);
                    ImageAddShuiYin(j.Img8);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "1<br>");
                }
            }
            Response.Write("ok");
        }
        /// <summary>
        /// 单个项目添加水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button6_Click(object sender, EventArgs e)
        {
            Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(int.Parse(TextBox2.Text));
            if (j != null)
            {
                try
                {
                    ImageAddShuiYin(j.Img1);
                    ImageAddShuiYin(j.Img2);
                    ImageAddShuiYin(j.Img3);
                    ImageAddShuiYin(j.Img4);
                    ImageAddShuiYin(j.Img5);
                    ImageAddShuiYin(j.Img6);
                    ImageAddShuiYin(j.Img7);
                    ImageAddShuiYin(j.Img8);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "<br>");
                }


            }
            Response.Write("ok");
        }
        /// <summary>
        /// 所有动态添加水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button7_Click(object sender, EventArgs e)
        {
            List<Pet_JiuZhu_News> list = nvbo.GetAllJiuZhuNewsList();
            foreach (var j in list)
            {
                try
                {
                    ImageAddShuiYin(j.Img1);
                    ImageAddShuiYin(j.Img2);
                    ImageAddShuiYin(j.Img3);
                    ImageAddShuiYin(j.Img4);
                    ImageAddShuiYin(j.Img5);
                    ImageAddShuiYin(j.Img6);
                    ImageAddShuiYin(j.Img7);
                    ImageAddShuiYin(j.Img8);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString() + ex.StackTrace + "1<br>");
                }
            }
            Response.Write("ok");
        }
    }
}