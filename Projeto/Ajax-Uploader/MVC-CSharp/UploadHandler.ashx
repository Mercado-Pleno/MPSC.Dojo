<%@ WebHandler Language="C#" Class="TheUploadHandler" %>

using System;
using System.IO;
using System.Web;
using CuteWebUI;

public class TheUploadHandler : IHttpHandler {

	//see MyHelper.CreateMyUploader
    
    public void ProcessRequest (HttpContext context) {
		using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(context))
		{
			uploader.UploadUrl = context.Response.ApplyAppPathModifier("~/UploadHandler.ashx");
			//the data of the uploader will render as <input type='hidden' name='myuploader'> 
			uploader.FormName = "myuploader";
			uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
			
			//let uploader process the common task and return common result
			uploader.PreProcessRequest();

			//if need validate the file : (after the PreProcessRequest have validated the size/extensions)
			if (uploader.IsValidationRequest)
			{
				//get the file need be validated:
				MvcUploadFile file = uploader.GetValidatingFile();
				
				if (string.Equals(Path.GetExtension(file.FileName), ".bmp", StringComparison.OrdinalIgnoreCase))
				{
					uploader.WriteValidationError("My custom validation error : do not upload bmp");
					return;
				}

				uploader.WriteValidationOK();
				return;
			}
		}
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}





