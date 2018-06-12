using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SPACRM.Common;
using SPACRM.Common.Exceptions;
using SPACRM.DataAccess.Repository;
using SPACRM.Entity;
using SPACRM.Interface;
using SPACRM.Entity.Entities;
using System.Transactions;
using System.Text.RegularExpressions;

namespace SPACRM.Business.ServiceImpl
{
    public class CommonService : ICommonService
    {
        private CommonRepository _repo;
       
        public CommonService()
        {
            _repo = new CommonRepository();
            
        }
        

        //public FILES UploadFile(string ext, string contentType, byte[] data, string url)
        //{
        //    FILES fileEntity = null;
        //    try
        //    {
        //        string newName = Guid.NewGuid().ToString("d") + ext;

        //        string filePath = string.Concat(AppConfig.UploadTMP, DateTime.Today.ToString("yyyyMMdd"), "\\");

        //        if (!Directory.Exists(filePath))
        //        {
        //            Directory.CreateDirectory(filePath);
        //        }
        //        string fullName = filePath + newName;
        //        using (FileStream steam = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write))
        //        {
        //            steam.Write(data, 0, data.Length);
        //        }

        //        fileEntity = new FILES();
        //        fileEntity.CONTENT_TYPE = contentType;
        //        fileEntity.FILE_NAME = fullName;
        //        fileEntity.FILE_SIZE = data.Length;
        //        fileEntity.FILE_URL = "<!--只有图片存放到外部时才有用哦--!>";
        //        fileEntity.ID = (int)_repo.Insert(fileEntity);

        //        fileEntity.FILE_URL = url + "/" + fileEntity.ID;
        //        _repo.Update(fileEntity);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BOException("上传图片发生意外错误，请稍后重试，或联系管理员", ex);
        //    }
        //    return fileEntity;
        //}

        //public FILES UploadFile(string ext, string contentType, byte[] data, string url, string remark)
        //{
        //    FILES fileEntity = null;
        //    try
        //    {
        //        string newName = Guid.NewGuid().ToString("d") + ext;
        //        string newName2 = Guid.NewGuid().ToString("d") + ext;

        //        string filePath = string.Concat(AppConfig.UploadTMP, DateTime.Today.ToString("yyyyMMdd"), "\\");

        //        if (!Directory.Exists(filePath))
        //        {
        //            Directory.CreateDirectory(filePath);
        //        }
        //        string fullName = filePath + newName;
        //        using (FileStream steam = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write))
        //        {
        //            steam.Write(data, 0, data.Length);
        //        }

        //        fileEntity = new FILES();
        //        fileEntity.CONTENT_TYPE = contentType;
        //        fileEntity.FILE_NAME = fullName;
        //        fileEntity.FILE_SIZE = data.Length;
        //        fileEntity.FILE_URL = "<!--只有图片存放到外部时才有用哦--!>";
        //        //压缩图片
        //        if (fileEntity.FILE_SIZE > 51200)  //原文件大于50k则压缩
        //        {
        //            MakeThumbnail(fullName, filePath + newName2, 20, 20, true);
        //            File.Delete(fileEntity.FILE_NAME); //删除源文件
        //            fileEntity.FILE_NAME = filePath + newName2;
        //        }
        //        //压缩图片
        //        if (remark != "")
        //            fileEntity.REMARK = remark;
        //        else
        //            fileEntity.REMARK = fullName.Split('\\')[fullName.Split('\\').Length - 1];
        //        fileEntity.ID = (int)_repo.Insert(fileEntity);

        //        fileEntity.FILE_URL = url + "/" + fileEntity.ID;
        //        _repo.Update(fileEntity);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BOException("上传图片发生意外错误，请稍后重试，或联系管理员", ex);
        //    }
        //    return fileEntity;
        //}

        ///// <SUMMARY>
        ///// 生成缩略图//带压缩图片不压缩22k压缩2k
        ///// </SUMMARY>
        ///// <PARAM name="originalImagePath" />原始路径
        ///// <PARAM name="thumbnailPath" />生成缩略图路径
        ///// <PARAM name="width" />缩略图的宽
        ///// <PARAM name="height" />缩略图的高
        ////是否压缩图片质量
        //public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, bool Ys)
        //{
        //    //获取原始图片  
        //    System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
        //    //缩略图画布宽高 
        //    int towidth = 0;
        //    int toheight = 0;
        //    if (originalImage.Width > 1800) //图片像素大于1800，则缩放四倍
        //    {
        //        width = originalImage.Width / 4;
        //        height = originalImage.Height / 4;
        //        towidth = width;
        //        toheight = height;
        //    }
        //    else if (originalImage.Width > 800) //图片像素大于800，则缩放2倍
        //    {
        //        width = originalImage.Width / 2;
        //        height = originalImage.Height / 2;
        //        towidth = width;
        //        toheight = height;
        //    }
        //    else  //图片不是太大，则保持原大小
        //    {
        //        towidth = originalImage.Width; //width;
        //        toheight = originalImage.Height; //height;
        //        width = towidth;
        //        height = toheight;
        //    }
        //    //原始图片写入画布坐标和宽高(用来设置裁减溢出部分)  
        //    int x = 0;
        //    int y = 0;
        //    int ow = originalImage.Width;
        //    int oh = originalImage.Height;
        //    //原始图片画布,设置写入缩略图画布坐标和宽高(用来原始图片整体宽高缩放)  
        //    int bg_x = 0;
        //    int bg_y = 0;
        //    int bg_w = towidth;
        //    int bg_h = toheight;
        //    //倍数变量  
        //    double multiple = 0;
        //    //获取宽长的或是高长与缩略图的倍数  
        //    if (originalImage.Width >= originalImage.Height)
        //        multiple = (double)originalImage.Width / (double)width;
        //    else
        //        multiple = (double)originalImage.Height / (double)height;
        //    //上传的图片的宽和高小等于缩略图  
        //    if (ow <= width && oh <= height)
        //    {
        //        //缩略图按原始宽高  
        //        bg_w = originalImage.Width;
        //        bg_h = originalImage.Height;
        //        //空白部分用背景色填充  
        //        bg_x = Convert.ToInt32(((double)towidth - (double)ow) / 2);
        //        bg_y = Convert.ToInt32(((double)toheight - (double)oh) / 2);
        //    }
        //    //上传的图片的宽和高大于缩略图  
        //    else
        //    {
        //        //宽高按比例缩放  
        //        bg_w = Convert.ToInt32((double)originalImage.Width / multiple);
        //        bg_h = Convert.ToInt32((double)originalImage.Height / multiple);
        //        //空白部分用背景色填充  
        //        bg_y = Convert.ToInt32(((double)height - (double)bg_h) / 2);
        //        bg_x = Convert.ToInt32(((double)width - (double)bg_w) / 2);
        //    }
        //    //新建一个bmp图片,并设置缩略图大小.  
        //    System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
        //    //新建一个画板  
        //    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        //    //设置高质量插值法  
        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
        //    //设置高质量,低速度呈现平滑程度  
        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //    //清空画布并设置背景色  
        //    g.Clear(System.Drawing.ColorTranslator.FromHtml("#FFF"));
        //    //在指定位置并且按指定大小绘制原图片的指定部分  
        //    //第一个System.Drawing.Rectangle是原图片的画布坐标和宽高,第二个是原图片写在画布上的坐标和宽高,最后一个参数是指定数值单位为像素  
        //    g.DrawImage(originalImage, new System.Drawing.Rectangle(bg_x, bg_y, bg_w, bg_h), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

        //    if (Ys)
        //    {

        //        System.Drawing.Imaging.ImageCodecInfo encoder = GetEncoderInfo("image/jpeg");
        //        try
        //        {
        //            if (encoder != null)
        //            {
        //                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
        //                //设置 jpeg 质量为 60
        //                encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)50);
        //                bitmap.Save(thumbnailPath, encoder, encoderParams);
        //                encoderParams.Dispose();

        //            }
        //        }
        //        catch (System.Exception e)
        //        {
        //            //throw e;
        //        }
        //        finally
        //        {
        //            originalImage.Dispose();
        //            bitmap.Dispose();
        //            g.Dispose();
        //        }

        //    }
        //    else
        //    {

        //        try
        //        {
        //            //获取图片类型  
        //            string fileExtension = System.IO.Path.GetExtension(originalImagePath).ToLower();
        //            //按原图片类型保存缩略图片,不按原格式图片会出现模糊,锯齿等问题.  
        //            switch (fileExtension)
        //            {
        //                case ".gif": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif); break;
        //                case ".jpg": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
        //                case ".bmp": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp); break;
        //                case ".png": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png); break;
        //            }
        //        }
        //        catch (System.Exception e)
        //        {
        //            throw e;
        //        }
        //        finally
        //        {
        //            originalImage.Dispose();
        //            bitmap.Dispose();
        //            g.Dispose();
        //        }

        //    }

        //}

        //private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
        //{
        //    //根据 mime 类型，返回编码器
        //    System.Drawing.Imaging.ImageCodecInfo result = null;
        //    System.Drawing.Imaging.ImageCodecInfo[] encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        //    for (int i = 0; i < encoders.Length; i++)
        //    {
        //        if (encoders[i].MimeType == mimeType)
        //        {
        //            result = encoders[i];
        //            break;
        //        }

        //    }
        //    return result;

        //}

        //public FILES GetUploadFile(int id)
        //{
        //    return _repo.GetFILES(id);
        //}

        public int DeleteFiles(int id)
        {
            return _repo.DeleteFiles(id);
        }

    }
}
