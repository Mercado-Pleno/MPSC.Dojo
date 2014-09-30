<%@ WebHandler Language="C#" Class="DownloadTempPhoto" %>

using System;
using System.Web;

public class DownloadTempPhoto : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
		Guid guid = new Guid(context.Request.QueryString["guid"]);

		using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(context))
		{
			CuteWebUI.MvcUploadFile file=uploader.GetUploadedFile(guid);
			if (file == null)
				return;

			context.Response.Expires = 60 * 24 * 365; 
			context.Response.Cache.SetCacheability(HttpCacheability.Public);
			context.Response.WriteFile(file.GetTempFilePath());
		}
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}