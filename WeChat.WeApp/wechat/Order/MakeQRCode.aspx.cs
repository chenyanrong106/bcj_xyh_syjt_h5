using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class MakeQRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["data"]))
            {
                string str = Request.QueryString["data"];

                //初始化二维码生成工具
                //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                //qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                //qrCodeEncoder.QRCodeVersion = 0;
                //qrCodeEncoder.QRCodeScale = 4;

                ////将字符串生成二维码图片
                //Bitmap image = qrCodeEncoder.Encode(str, Encoding.UTF8);

                ////保存为PNG到内存流  
                //MemoryStream ms = new MemoryStream();
                //image.Save(ms, ImageFormat.Png);

                ////输出二维码图片
                //Response.BinaryWrite(ms.GetBuffer());
                //Response.End();

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 8;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                //System.Drawing.Image image = qrCodeEncoder.Encode("4408810820 深圳－广州 小江");
                System.Drawing.Image image = qrCodeEncoder.Encode(str);
                string filename = Guid.NewGuid() + ".jpg";
                string filepath = Server.MapPath(@"~\Upload") + "\\" + filename;
                System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

                fs.Close();
                image.Dispose();
                //二维码解码
                Response.Redirect("~/upload/"+filename);
            }
        }
    }
}