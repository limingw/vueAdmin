using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;
using System.IO.Compression;
using ZhiFuTui.Areas.Admin.Controllers;
using BLL;
using IBLL;
using Model;

namespace Web.kindeditor.asp.net
{
    public class inavAdFile_upload : IHttpHandler
    {
        private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            string directoryExist = context.Request.QueryString["inavId"] == null ? "" : context.Request.QueryString["inavId"];
            //文件保存目录URL
            string devPath = HttpContext.Current.Request.MapPath("~/").ToString();
            string savePath = "/Areas/InavAd/";


            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            //extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            //extTable.Add("flash", "swf,flv");
            //extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            //extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
            extTable.Add("file", "zip");
            //最大文件大小
            int maxSize = 9999999;
            this.context = context;

            //文件流是否空
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                ShowError("请选择文件。");
            }

     

            //选择文件格式
            String dirName = context.Request.QueryString["file"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "file";
            }
            if (!extTable.ContainsKey(dirName))
            {
                ShowError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                ShowError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                ShowError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

        

            //string[] checkFileExist = imgFile.FileName.Split('.');
            //if (Directory.Exists(checkFileExist[0]))
            //{
            //    ShowError("文件已经存在，请重命名。");
            //}

            //创建文件夹
            //String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            //savePath += ymd + "/";

            if (!Directory.Exists(context.Server.MapPath(savePath)))
            {
                Directory.CreateDirectory(context.Server.MapPath(savePath));
            }
            // String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            //String newFileName = fileName + fileExt;
            String filePath = savePath + fileName;
            imgFile.SaveAs(context.Server.MapPath(filePath));
            //更新判断对应的素材包是否存在
            if (directoryExist != "")
            {
                bool success = CheckResourceExist(directoryExist, devPath,filePath);
                if (!success)
                {
                    File.Delete(devPath + filePath);
                    ShowError("文件更新失败,更新文件名与旧文件名不一致");
                }
                //System.Diagnostics.Debug.Write(success);
            }

            //string[] resultString = Regex.Split(filePath, "//", RegexOptions.IgnoreCase);
            try
            {
                ZipFile.ExtractToDirectory(devPath + filePath, devPath + savePath);
            }
            catch
            {
                //解压失败后删除压缩包(文件已上传，文件夹覆盖不了)
                File.Delete(devPath + filePath);
                ShowError("素材文件夹名称已经存在，请重命名。");
            }

            //解压成功后删除压缩包
            File.Delete(devPath + filePath);

            try
            {
                Hashtable hash = new Hashtable();
                hash["error"] = 0;
                //hash["url"] = savePath + imgFile.FileName.Split('.')[0];
                hash["url"] = savePath + imgFile.FileName.Replace(fileExt,"");
                context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                context.Response.Write(JsonMapper.ToJson(hash));
                context.ApplicationInstance.CompleteRequest();
                //context.Response.End();
            }
            catch (Exception e)
            {
                log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                _log.Error("提交交互类广告出错:" + e.StackTrace);
            }
        }

        private void ShowError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }


        public bool CheckResourceExist(string inavId, string devPath,string oldfilePath)
        {
            bool success = false;
            FileAttributes attr;
            InavAdTbBLL _inavAdTb = BLLContainer.Container.Resolve<IInavAdTbBLL>() as InavAdTbBLL;
            InavAdTb inavAdTb = _inavAdTb.GetUsefulInavAdByInavId(inavId);
            string filePath = devPath + inavAdTb.Path;
            if (inavAdTb == null || inavAdTb.Path == null)
            {
                return success;
            }
            try
            {
                attr = System.IO.File.GetAttributes(filePath);
            }
            catch
            {
                return success;
            }

            if (attr != FileAttributes.Directory)
            {
                return success;
            }

            if (Directory.Exists(filePath))
            {
                if (oldfilePath.Replace(".zip","") == inavAdTb.Path)
                {
                    Directory.Delete(filePath, true);
                    success = true;
                }
          
            }
            return success;
        }

    }
}