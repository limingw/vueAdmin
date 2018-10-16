using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;
using Common;

namespace Web.kindeditor.asp.net
{
    /// <summary>
    /// imgupload 的摘要说明
    /// </summary>
    public class imguploadToOss : IHttpHandler
    {
        private HttpContext context;
        private bool compelete = false;
        private bool success = false;
        public void ProcessRequest(HttpContext context)
        {
            compelete = false;
            success = false;
            string modulename = context.Request.QueryString["modulename"] == null ? "" : context.Request.QueryString["modulename"];
            string uid = context.Request.QueryString["uid"] == null ? "" : context.Request.QueryString["uid"];
            if (String.IsNullOrEmpty(modulename))
            {
                return;
            }
            if (String.IsNullOrEmpty(uid))
            {
                return;
            }
            //文件保存目录路径


            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            //extTable.Add("flash", "swf,flv");
            //extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            //extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1024 * 1024; //最大为1M
            this.context = context;

            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }


            String dirName = context.Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }
            Stream fileStream = imgFile.InputStream; //文件流
            byte[] bytes = Util.CommonUtils.StreamToBytes(fileStream);
            string key = Util.CommonUtils.GetMD5HashFromFile(fileStream) + fileExt; //md5是key
            string path = OSSManager.Instance.GetOssPath("pic", modulename, uid);
            OSSManager.Instance.SetObject(path, key, bytes, null, CompleteCallBack); //提交文件
            while (true)
            {
                try
                {
                    if (compelete)
                    {
                        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                        Hashtable hash = new Hashtable();
                        if (success)
                        {
                            hash["error"] = 0;
                            hash["url"] = path + "/" + key;
                        }
                        else
                        {
                            hash["error"] = 0;
                            hash["message"] = "上传失败";
                        }
                        context.Response.Write(JsonMapper.ToJson(hash));
                        context.ApplicationInstance.CompleteRequest();
                        //context.Response.End();
                        break;
                    }

                }
                catch (Exception e)
                {

                    log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    _log.Error("====e.StackTrace)" + e.StackTrace);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void CompleteCallBack(bool success, string path, string key, Stream stream)
        {
            compelete = true;
            this.success = success;
        }



        private void showError(string message)
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
    }
}