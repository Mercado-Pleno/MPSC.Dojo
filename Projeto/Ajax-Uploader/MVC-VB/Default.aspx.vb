Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim originalPath As String = Request.Path
		HttpContext.Current.RewritePath(Request.ApplicationPath, False)
		Dim httpHandler As IHttpHandler = New MvcHttpHandler()
		httpHandler.ProcessRequest(HttpContext.Current)
		HttpContext.Current.RewritePath(originalPath, False)
	End Sub

End Class