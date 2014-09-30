<%@ WebHandler Language="VB" Class="FileManagerDownload" %>

Imports System
Imports System.IO
Imports System.Web
Imports CuteWebUI
Imports MVCVB

Public Class FileManagerDownload
    Implements IHttpHandler
	Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
		Dim username As String = context.Request.QueryString("user")
		Dim fileid As String = context.Request.QueryString("file")
		Dim provider As New FileManagerProvider()
		Dim item As FileItem = provider.GetFileByID(username, fileid)
		If item Is Nothing Then
			context.Response.StatusCode = 404
			context.Response.Write("File Not Found")
			Exit Sub
		End If
        
		Select Case System.IO.Path.GetExtension(item.FileName).ToLower()
			Case ".png"
				context.Response.ContentType = "image/png"
				Exit Select
			Case ".gif"
				context.Response.ContentType = "image/gif"
				Exit Select
			Case ".jpeg", ".jpg"
				context.Response.ContentType = "image/jpeg"
				Exit Select
			Case Else
				context.Response.ContentType = "application/otc-stream"
				Exit Select
		End Select
		context.Response.AddHeader("Content-Disposition", "attachment; filename=""" & HttpUtility.UrlEncode(item.FileName) & """")
		context.Response.WriteFile(item.FilePath)
	End Sub
    
	Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
		Get
			Return False
		End Get
	End Property
End Class