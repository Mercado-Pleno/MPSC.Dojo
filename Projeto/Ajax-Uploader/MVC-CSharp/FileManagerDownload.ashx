<%@ WebHandler Language="C#" Class="FileManagerDownload" %>

using System;
using System.IO;
using System.Web;
using CuteWebUI;
using MVCCSharp;

public class FileManagerDownload : IHttpHandler {
		public void ProcessRequest(HttpContext context)
		{
			string username = context.Request.QueryString["user"];
			string fileid = context.Request.QueryString["file"];
			FileManagerProvider provider = new FileManagerProvider();
			FileItem item=provider.GetFileByID(username, fileid);
			if (item == null)
			{
				context.Response.StatusCode = 404;
				context.Response.Write("File Not Found");
				return;
			}

			switch (System.IO.Path.GetExtension(item.FileName).ToLower())
			{
				case ".png":
					context.Response.ContentType = "image/png";
					break;
				case ".gif":
					context.Response.ContentType = "image/gif";
					break;
				case ".jpeg":
				case ".jpg":
					context.Response.ContentType = "image/jpeg";
					break;
				default:
					context.Response.ContentType = "application/otc-stream";
					break;
			}
			context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode( item.FileName )+ "\"");
			context.Response.WriteFile(item.FilePath);
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
}