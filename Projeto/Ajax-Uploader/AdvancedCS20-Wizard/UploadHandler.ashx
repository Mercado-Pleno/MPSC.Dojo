<%@ WebHandler Language="C#" Class="TheUploadHandler" %>

using System;
using System.Web;
using CuteWebUI;

public class TheUploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
		using (MvcUploader uploader = new MvcUploader(context))
		{
			uploader.AllowedFileExtensions = "*.png,*.jpg,*.gif,*.bmp";
			uploader.PreProcessRequest();
			if (uploader.IsValidationRequest)
			{
				//verify the uploader.CurrentFileGuid and uploader.WriteVerifyError("myerror");

				Guid guid = uploader.CurrentFileGuid;

				uploader.WriteValidationOK();
			}
		}
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}





