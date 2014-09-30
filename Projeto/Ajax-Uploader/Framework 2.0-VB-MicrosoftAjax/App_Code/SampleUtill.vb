Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.IO

Partial Public Class SampleUtil
    Public Shared ReadOnly Property Context() As HttpContext
        Get
            Return HttpContext.Current
        End Get
    End Property

    Public Shared Function ToUserLocalTime(ByVal time As DateTime) As DateTime
        Dim cookie As HttpCookie = Context.Request.Cookies("TimeZone")
        If cookie Is Nothing Then
            Return time
        End If
        Dim tzo As Integer = Integer.Parse(cookie.Value)
        Return time.ToUniversalTime().AddHours(tzo)
    End Function

    Public Shared Function GetAjaxMode() As String
        Dim cookie As HttpCookie
        Return GetAjaxMode(cookie)
    End Function
    Public Shared Function GetAjaxMode(ByRef cookie As HttpCookie) As String
        cookie = Context.Request.Cookies("AjaxMode")
        If Not cookie Is Nothing Then
            Select Case cookie.Value
                Case "Atlas"
                    Return "Atlas"
                Case "Magic"
                    Return "Magic"
                Case "None"
                    Return "None"
            End Select
        End If
        Return "Atlas"
    End Function

    Public Shared Sub SetAjaxMode(ByVal mode As String)
        Dim cookie As HttpCookie = New HttpCookie("AjaxMode")
        cookie.Path = "/"
        cookie.Expires = DateTime.Now.AddYears(1)
        cookie.Value = mode
        Context.Response.Cookies.Add(cookie)
    End Sub

    Public Shared Sub SetPageCache()
        If Context.Request.Browser.Browser = "Opera" Then
            Context.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        End If
    End Sub
End Class