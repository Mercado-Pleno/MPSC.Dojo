<%@ WebHandler Language="VB" Class="TheUploadHandler" %>

Imports System
Imports System.IO
Imports System.Web
Imports CuteWebUI

Public Class TheUploadHandler
    Implements IHttpHandler
    
    'see MyHelper.CreateMyUploader
    
	Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
		Using uploader As New CuteWebUI.MvcUploader(context)
			uploader.UploadUrl = context.Response.ApplyAppPathModifier("~/UploadHandler.ashx")
			'the data of the uploader will render as <input type='hidden' name='myuploader'> 
			uploader.FormName = "myuploader"
			uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"
            
			'let uploader process the common task and return common result
			uploader.PreProcessRequest()
            
			'if need validate the file : (after the PreProcessRequest have validated the size/extensions)
			If uploader.IsValidationRequest Then
				'get the file need be validated:
				Dim file As MvcUploadFile = uploader.GetValidatingFile()
                
				If String.Equals(Path.GetExtension(file.FileName), ".bmp", StringComparison.OrdinalIgnoreCase) Then
					uploader.WriteValidationError("My custom validation error : do not upload bmp")
					Exit Sub
				End If
                
				uploader.WriteValidationOK()
				Exit Sub
			End If
		End Using
	End Sub
    
	Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
		Get
			Return False
		End Get
	End Property
    
End Class



