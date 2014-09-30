using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;

namespace MVCCSharp.Controllers
{
    public class SamplesController : Controller
    {
        public ActionResult Index()
        {
            return View();
		}

		#region Simple 1 - Upload multiple files
		
		public ActionResult Sample1(string myuploader)
		{
			using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
			{
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
				//the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader";
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

				uploader.InsertText = "Select a file to upload";

				//prepair html code for the view
				ViewData["uploaderhtml"] = uploader.Render();

				//if it's HTTP POST:
				if (!string.IsNullOrEmpty(myuploader))
				{
					//for single file , the value is guid string
					Guid fileguid = new Guid(myuploader);
					CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
					if (file != null)
					{
						//you should validate it here:

						//now the file is in temporary directory, you need move it to target location
						//file.MoveTo("~/myfolder/" + file.FileName);

						//set the output message
						ViewData["UploadedMessage"] = "The file " + file.FileName + " has been processed.";
					}
				}

			}

			return View();
		}

		#endregion

		#region Simple 2 - Upload multiple files

		public ActionResult Sample2(string myuploader)
		{
			using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
			{
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
				//the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader";
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

				//allow select multiple files
				uploader.MultipleFilesUpload = true;

				//tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton";

				//prepair html code for the view
				ViewData["uploaderhtml"] = uploader.Render();

				//if it's HTTP POST:
				if (!string.IsNullOrEmpty(myuploader))
				{
					List<string> processedfiles = new List<string>();
					//for multiple files , the value is string : guid/guid/guid 
					foreach(string strguid in myuploader.Split('/'))
					{
						Guid fileguid=new Guid(strguid);
						CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
						if (file != null)
						{
							//you should validate it here:

							//now the file is in temporary directory, you need move it to target location
							//file.MoveTo("~/myfolder/" + file.FileName);
							processedfiles.Add(file.FileName);
						}
					}
					if (processedfiles.Count > 0)
					{
						ViewData["UploadedMessage"] = string.Join(",",processedfiles.ToArray())+" have been processed.";
					}
				}

			}

			return View();
		}

		#endregion

		#region Simple 3 - AJAX processing files

		public ActionResult Sample3()
		{
			using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
			{
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
				//the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader";
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

				//allow select multiple files
				uploader.MultipleFilesUpload = true;

				//tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton";

				//prepair html code for the view
				ViewData["uploaderhtml"] = uploader.Render();
			}
			return View();
		}

		public ActionResult Sample3Ajax(string guidlist)
		{
			List<SampleTempJsonItem> items = new List<SampleTempJsonItem>();

			//you can use the MvcUploader.GetUploadedFile in any where
			using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
			{
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
				//the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader";
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

				foreach (string strguid in guidlist.Split('/'))
				{
					SampleTempJsonItem item = new SampleTempJsonItem();
					item.FileGuid = strguid;
					CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(new Guid(strguid));
					if (file == null)
					{
						item.Exists = false;
						item.Error = "File not exists";
						continue;
					}
					item.FileName = file.FileName;
					item.FileSize = file.FileSize;
					//process this item..
					items.Add(item);
				}
			}
			JsonResult json = new JsonResult();
			json.Data = items;
			return json;
		}

		#endregion

		#region Simple 4 - Start uploading manually

		public ActionResult Sample4(string myuploader)
		{
			using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
			{
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
				//the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader";
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

				//set the uploader do not automatically start upload after selecting files
				uploader.ManualStartUpload = true;

				//set only allow 5 files at once.
				uploader.MaxFilesLimit = 5;

				//allow select multiple files
				uploader.MultipleFilesUpload = true;

				//tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton";

				//prepair html code for the view
				ViewData["uploaderhtml"] = uploader.Render();

				//if it's HTTP POST:
				if (!string.IsNullOrEmpty(myuploader))
				{
					List<string> processedfiles = new List<string>();
					//for multiple files , the value is string : guid/guid/guid 
					foreach (string strguid in myuploader.Split('/'))
					{
						Guid fileguid = new Guid(strguid);
						CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
						if (file != null)
						{
							//you should validate it here:

							//now the file is in temporary directory, you need move it to target location
							//file.MoveTo("~/myfolder/" + file.FileName);
							processedfiles.Add(file.FileName);
						}
					}
					if (processedfiles.Count > 0)
					{
						ViewData["UploadedMessage"] = string.Join(",", processedfiles.ToArray()) + " have been processed.";
					}
				}

			}

			return View();
		}

		#endregion

		#region Simple 5 - Keep state after submitting

		public ActionResult Sample5(string myuploader,string uploadedlist)
		{
			using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
			{
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
				//the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader";
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

				//allow select multiple files
				uploader.MultipleFilesUpload = true;

				//tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton";

				//prepair html code for the view
				ViewData["uploaderhtml"] = uploader.Render();

				StringBuilder sb = new StringBuilder();

				List<Guid> processedfiles = new List<Guid>();
				if (!string.IsNullOrEmpty(uploadedlist))
				{
					foreach (string strguid in uploadedlist.Split('/'))
					{
						Guid fileguid = new Guid(strguid);
						processedfiles.Add(fileguid);
					}
				}
				if (!string.IsNullOrEmpty(myuploader))
				{
					foreach (string strguid in myuploader.Split('/'))
					{
						Guid fileguid = new Guid(strguid);
						CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
						if (file != null)
						{
							//process the file..
							processedfiles.Add(fileguid);
						}
					}
				}

				sb.Append("<input type='hidden' name='uploadedlist' value='");
				for (int i = 0; i < processedfiles.Count; i++)
				{
					if (i > 0) sb.Append("/");
					sb.Append(processedfiles[i].ToString());
				}
				sb.Append("' />");
				if (processedfiles.Count > 0)
				{
					string fileicon = Response.ApplyAppPathModifier("~/Resources/file.png");
					sb.Append("<div style='padding:8px;'>"); 
					sb.Append("Uploaded files:");
					sb.Append("</div>");
					sb.Append("<table border='1' cellspacing='0' cellpadding='4'>");
					foreach (Guid fileguid in processedfiles)
					{
						CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
						if (file != null)
						{
							sb.Append("<tr>");
							sb.Append("<td>");
							sb.Append("<img src='").Append(fileicon).Append("' border='0'/>");
							sb.Append("</td>");
							sb.Append("<td>");
							sb.Append(HttpUtility.HtmlEncode(file.FileName));
							sb.Append("</td>");
							sb.Append("<td>"); 
							sb.Append(file.FileSize);
							sb.Append("</td>"); 
							sb.Append("</tr>");
						}
					}
					sb.Append("</table>");
				}

				ViewData["listhtml"] = sb.ToString();
			}



			return View();
		}

		#endregion

		#region Advanced 1 - AJAX file manager

		public ActionResult Advanced1()
		{
			using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
			{
				uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
				//the data of the uploader will render as <input type='hidden' name='myuploader'> 
				uploader.FormName = "myuploader";
				uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

				//allow select multiple files
				uploader.MultipleFilesUpload = true;

				//tell uploader attach a button
				uploader.InsertButtonID = "uploadbutton";

				//prepair html code for the view
				ViewData["uploaderhtml"] = uploader.Render();
			}
			return View();
		}

		public ActionResult Advanced1Ajax(string guidlist,string deleteid)
		{
			FileManagerProvider manager = new FileManagerProvider();
			string username=GetCurrentUserName();

			if (!string.IsNullOrEmpty(guidlist))
			{
				using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
				{
					uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
					//the data of the uploader will render as <input type='hidden' name='myuploader'> 
					uploader.FormName = "myuploader";
					uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

					foreach (string strguid in guidlist.Split('/'))
					{
						CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(new Guid(strguid));
						if (file == null)
							continue;
						manager.MoveFile(username, file.GetTempFilePath(), file.FileName, null);
					}
				}
			}

			if (!string.IsNullOrEmpty(deleteid))
			{
				FileItem file = manager.GetFileByID(username, deleteid);
				if (file != null)
				{
					file.Delete();
				}
			}

			FileItem[] files=manager.GetFiles(username);
			Array.Reverse(files);
			FileManagerJsonItem[] items = new FileManagerJsonItem[files.Length];
			string baseurl = Response.ApplyAppPathModifier("~/FileManagerDownload.ashx?user=" + username + "&file=");
			for (int i = 0; i < files.Length; i++)
			{
				FileItem file = files[i];
				FileManagerJsonItem item = new FileManagerJsonItem();
				item.FileID = file.FileID;
				item.FileName = file.FileName;
				item.Description = file.Description;
				item.UploadTime = file.UploadTime.ToString("yyyy-MM-dd HH:mm:ss");
				item.FileSize = file.FileSize;
				item.FileUrl=baseurl+file.FileID;
				items[i] = item;
			}
			JsonResult json = new JsonResult();
			json.Data = items;
			return json;
		}

		#endregion


		protected string GetCurrentUserName()
		{
			return "Guest";
		}

	}


	public class SampleTempJsonItem
	{
		public string FileGuid;
		public string FileName;
		public int FileSize;
		public bool Exists;
		public string Error;
	}

	public class FileManagerJsonItem
	{
		public string FileID;
		public string FileName;
		public int FileSize;
		public string Description;
		public string UploadTime;
		public string FileUrl;
	}

}
