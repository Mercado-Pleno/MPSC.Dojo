Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Mvc.Ajax
Imports System.Text

Namespace MVCVB.Controllers
	Public Class SamplesController
		Inherits Controller
		Public Function Index() As ActionResult
			Return View()
		End Function

#Region "Simple 1 - Upload multiple files"

		Public Function Sample1(ByVal myuploader As String) As ActionResult
			Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
				'the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.FormName = "myuploader"
                uploader.AutoUseSystemTempFolder = False


				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

				uploader.InsertText = "Select a file to upload"

				'prepair html code for the view
				ViewData("uploaderhtml") = uploader.Render()

				'if it's HTTP POST:
				If Not String.IsNullOrEmpty(myuploader) Then
					'for single file , the value is guid string
					Dim fileguid As New Guid(myuploader)
					Dim file As CuteWebUI.MvcUploadFile = uploader.GetUploadedFile(fileguid)
					If file IsNot Nothing Then
						'you should validate it here:

						'now the file is in temporary directory, you need move it to target location
						'file.MoveTo("~/myfolder/" + file.FileName);

						'set the output message
						ViewData("UploadedMessage") = "The file " & file.FileName & " has been processed."
					End If

				End If
			End Using

			Return View()
		End Function

#End Region

#Region "Simple 2 - Upload multiple files"

		Public Function Sample2(ByVal myuploader As String) As ActionResult
			Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
				'the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader"
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

				'allow select multiple files
				uploader.MultipleFilesUpload = True

				'tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton"

				'prepair html code for the view
				ViewData("uploaderhtml") = uploader.Render()

				'if it's HTTP POST:
				If Not String.IsNullOrEmpty(myuploader) Then
					Dim processedfiles As New List(Of String)()
					'for multiple files , the value is string : guid/guid/guid 
					For Each strguid As String In myuploader.Split("/"c)
						Dim fileguid As New Guid(strguid)
						Dim file As CuteWebUI.MvcUploadFile = uploader.GetUploadedFile(fileguid)
						If file IsNot Nothing Then
							'you should validate it here:

							'now the file is in temporary directory, you need move it to target location
							'file.MoveTo("~/myfolder/" + file.FileName);
							processedfiles.Add(file.FileName)
						End If
					Next
					If processedfiles.Count > 0 Then
						ViewData("UploadedMessage") = String.Join(",", processedfiles.ToArray()) & " have been processed."
					End If

				End If
			End Using

			Return View()
		End Function

#End Region

#Region "Simple 3 - AJAX processing files"

		Public Function Sample3() As ActionResult
			Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
				'the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader"
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

				'allow select multiple files
				uploader.MultipleFilesUpload = True

				'tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton"

				'prepair html code for the view
				ViewData("uploaderhtml") = uploader.Render()
			End Using
			Return View()
		End Function

		Public Function Sample3Ajax(ByVal guidlist As String) As ActionResult
			Dim items As New List(Of SampleTempJsonItem)()

			'you can use the MvcUploader.GetUploadedFile in any where
			Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
				'the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader"
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

				For Each strguid As String In guidlist.Split("/"c)
					Dim item As New SampleTempJsonItem()
					item.FileGuid = strguid
					Dim file As CuteWebUI.MvcUploadFile = uploader.GetUploadedFile(New Guid(strguid))
					If file Is Nothing Then
						item.Exists = False
						item.[Error] = "File not exists"
						Continue For
					End If
					item.FileName = file.FileName
					item.FileSize = file.FileSize
					'process this item..
					items.Add(item)
				Next
			End Using
			Dim json As New JsonResult()
			json.Data = items
			Return json
		End Function

#End Region

#Region "Simple 4 - Start uploading manually"

		Public Function Sample4(ByVal myuploader As String) As ActionResult
			Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
				'the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader"
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

				'set the uploader do not automatically start upload after selecting files
				uploader.ManualStartUpload = True

				'set only allow 5 files at once.
				uploader.MaxFilesLimit = 5

				'allow select multiple files
				uploader.MultipleFilesUpload = True

				'tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton"

				'prepair html code for the view
				ViewData("uploaderhtml") = uploader.Render()

				'if it's HTTP POST:
				If Not String.IsNullOrEmpty(myuploader) Then
					Dim processedfiles As New List(Of String)()
					'for multiple files , the value is string : guid/guid/guid 
					For Each strguid As String In myuploader.Split("/"c)
						Dim fileguid As New Guid(strguid)
						Dim file As CuteWebUI.MvcUploadFile = uploader.GetUploadedFile(fileguid)
						If file IsNot Nothing Then
							'you should validate it here:

							'now the file is in temporary directory, you need move it to target location
							'file.MoveTo("~/myfolder/" + file.FileName);
							processedfiles.Add(file.FileName)
						End If
					Next
					If processedfiles.Count > 0 Then
						ViewData("UploadedMessage") = String.Join(",", processedfiles.ToArray()) & " have been processed."
					End If

				End If
			End Using

			Return View()
		End Function

#End Region

#Region "Simple 5 - Keep state after submitting"

		Public Function Sample5(ByVal myuploader As String, ByVal uploadedlist As String) As ActionResult
			Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
				'the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader"
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

				'allow select multiple files
				uploader.MultipleFilesUpload = True

				'tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton"

				'prepair html code for the view
				ViewData("uploaderhtml") = uploader.Render()

				Dim sb As New StringBuilder()

				Dim processedfiles As New List(Of Guid)()
				If Not String.IsNullOrEmpty(uploadedlist) Then
					For Each strguid As String In uploadedlist.Split("/"c)
						Dim fileguid As New Guid(strguid)
						processedfiles.Add(fileguid)
					Next
				End If
				If Not String.IsNullOrEmpty(myuploader) Then
					For Each strguid As String In myuploader.Split("/"c)
						Dim fileguid As New Guid(strguid)
						Dim file As CuteWebUI.MvcUploadFile = uploader.GetUploadedFile(fileguid)
						If file IsNot Nothing Then
							'process the file..
							processedfiles.Add(fileguid)
						End If
					Next
				End If

				sb.Append("<input type='hidden' name='uploadedlist' value='")
				For i As Integer = 0 To processedfiles.Count - 1
					If i > 0 Then
						sb.Append("/")
					End If
					sb.Append(processedfiles(i).ToString())
				Next
				sb.Append("' />")
				If processedfiles.Count > 0 Then
					Dim fileicon As String = Response.ApplyAppPathModifier("~/Resources/file.png")
					sb.Append("<div style='padding:8px;'>")
					sb.Append("Uploaded files:")
					sb.Append("</div>")
					sb.Append("<table border='1' cellspacing='0' cellpadding='4'>")
					For Each fileguid As Guid In processedfiles
						Dim file As CuteWebUI.MvcUploadFile = uploader.GetUploadedFile(fileguid)
						If file IsNot Nothing Then
							sb.Append("<tr>")
							sb.Append("<td>")
							sb.Append("<img src='").Append(fileicon).Append("' border='0'/>")
							sb.Append("</td>")
							sb.Append("<td>")
							sb.Append(HttpUtility.HtmlEncode(file.FileName))
							sb.Append("</td>")
							sb.Append("<td>")
							sb.Append(file.FileSize)
							sb.Append("</td>")
							sb.Append("</tr>")
						End If
					Next
					sb.Append("</table>")
				End If

				ViewData("listhtml") = sb.ToString()
			End Using



			Return View()
		End Function

#End Region

#Region "Advanced 1 - AJAX file manager"

		Public Function Advanced1() As ActionResult
			Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
				'the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader"
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

				'allow select multiple files
				uploader.MultipleFilesUpload = True

				'tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton"

				'prepair html code for the view
				ViewData("uploaderhtml") = uploader.Render()
			End Using
			Return View()
		End Function

		Public Function Advanced1Ajax(ByVal guidlist As String, ByVal deleteid As String) As ActionResult
			Dim manager As New FileManagerProvider()
			Dim username As String = GetCurrentUserName()

			If Not String.IsNullOrEmpty(guidlist) Then
				Using uploader As New CuteWebUI.MvcUploader(System.Web.HttpContext.Current)
					uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx")
					'the data of the uploader will render as <input type='hidden' name='myuploader'> 
					uploader.FormName = "myuploader"
					uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar"

					For Each strguid As String In guidlist.Split("/"c)
						Dim file As CuteWebUI.MvcUploadFile = uploader.GetUploadedFile(New Guid(strguid))
						If file Is Nothing Then
							Continue For
						End If
						manager.MoveFile(username, file.GetTempFilePath(), file.FileName, Nothing)
					Next
				End Using
			End If

			If Not String.IsNullOrEmpty(deleteid) Then
				Dim file As FileItem = manager.GetFileByID(username, deleteid)
				If file IsNot Nothing Then
					file.Delete()
				End If
			End If

			Dim files As FileItem() = manager.GetFiles(username)
			Array.Reverse(files)
			Dim items As FileManagerJsonItem() = New FileManagerJsonItem(files.Length - 1) {}
			Dim baseurl As String = Response.ApplyAppPathModifier("~/FileManagerDownload.ashx?user=" & username & "&file=")
			For i As Integer = 0 To files.Length - 1
				Dim file As FileItem = files(i)
				Dim item As New FileManagerJsonItem()
				item.FileID = file.FileID
				item.FileName = file.FileName
				item.Description = file.Description
				item.UploadTime = file.UploadTime.ToString("yyyy-MM-dd HH:mm:ss")
				item.FileSize = file.FileSize
				item.FileUrl = baseurl + file.FileID
				items(i) = item
			Next
			Dim json As New JsonResult()
			json.Data = items
			Return json
		End Function

#End Region


		Protected Function GetCurrentUserName() As String
			Return "Guest"
		End Function

	End Class


	Public Class SampleTempJsonItem
		Public FileGuid As String
		Public FileName As String
		Public FileSize As Integer
		Public Exists As Boolean
		Public [Error] As String
	End Class

	Public Class FileManagerJsonItem
		Public FileID As String
		Public FileName As String
		Public FileSize As Integer
		Public Description As String
		Public UploadTime As String
		Public FileUrl As String
	End Class

End Namespace