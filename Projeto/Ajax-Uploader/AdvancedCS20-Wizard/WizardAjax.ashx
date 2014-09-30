<%@ WebHandler Language="C#" Class="WizardAjax" %>

using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

public class WizardAjax : IHttpHandler {

	HttpContext context;
	
    public void ProcessRequest (HttpContext _context) {
		
		context=_context;

		try
		{
			ProcessRequest();
		}
		catch (Exception x)
		{
			WriteError("Exception", x.ToString());
		}
	}
	
	private void ProcessRequest()
	{
		context.Response.ContentType = "text/plain";

		Guid guid = new Guid(context.Request.QueryString["WizardID"]);

		string filepath=context.Server.MapPath("Data/" + guid.ToString() + ".config");
		if (!File.Exists(filepath))
		{
			WriteError("InvalidWizardID",null);
			return;
		}
		
		XmlDocument doc = new XmlDocument();
		doc.Load(filepath);

		if (doc.DocumentElement.GetAttribute("userid") != "SetUserIdHereForSecurity")
		{
			WriteError("InvalidUserID", null);
			return;
		}
		
		XmlElement nodeProfile = (XmlElement)doc.DocumentElement.SelectSingleNode("profile");
		
		string method=context.Request.QueryString["Method"];
		switch (method)
		{
			case "Load":
				break;
			case "SaveProfile":
				if (nodeProfile == null)
				{
					nodeProfile = doc.CreateElement("profile");
					doc.DocumentElement.AppendChild(nodeProfile);
				}
				string savename = context.Request.Form["name"];
				string saveemail = context.Request.Form["email"];
				if (savename == null || savename.Trim().Length == 0)
				{
					WriteError("InvalidProfileName",null);
					return;
				}
				if (saveemail == null || saveemail.Trim().Length == 0)
				{
					WriteError("InvalidProfileEmail", null);
					return;
				}
				nodeProfile.SetAttribute("name", savename.Trim());
				nodeProfile.SetAttribute("email", saveemail.Trim());
				doc.Save(filepath);
				break;
			case "AddFiles":
				string filelist=context.Request.Form["files"];
				if (string.IsNullOrEmpty(filelist))
					throw (new Exception("argument error , invalid files"));
				using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(context))
				{
					foreach (string fileguid in filelist.Split('/'))
					{
						CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(new Guid(fileguid));
						if (file == null)
							continue;

						//validate file extension again:
						string ext = Path.GetExtension(file.FileName);
						if (string.IsNullOrEmpty(ext))
							continue;
						switch (ext.ToLower())
						{
							case ".jpg":
							case ".png":
							case ".gif":
							case ".bmp":
								break;
							default:
								continue;
						}

						XmlElement nodeFile = doc.CreateElement("file");
						nodeFile.SetAttribute("guid", file.FileGuid.ToString());
						nodeFile.SetAttribute("name", file.FileName);
						nodeFile.SetAttribute("size", file.FileSize.ToString());

						doc.DocumentElement.AppendChild(nodeFile);
					}
				}
				doc.Save(filepath);
				break;
			case "DeleteFile":
				Guid delfileguid = new Guid(context.Request.Form["fileguid"]);
				foreach (XmlElement nodeFile in doc.DocumentElement.SelectNodes("file"))
				{
					if (new Guid(nodeFile.GetAttribute("guid")) == delfileguid)
					{
						doc.DocumentElement.RemoveChild(nodeFile);
						doc.Save(filepath);
						break;
					}
				}
				break;
			default:
				throw (new Exception("invalid method!"));
		}
		
		//Load data:
		
		StringBuilder sb = new StringBuilder();
		sb.Append("{");
		
		if (nodeProfile != null)
		{
			sb.Append("profile:{");
			sb.Append("name:");
			sb.Append(EncodeJScriptVariable(nodeProfile.GetAttribute("name")));
			sb.Append(",email:");
			sb.Append(EncodeJScriptVariable(nodeProfile.GetAttribute("email")));
			sb.Append("},");
		}
		
		sb.Append("files:[");
		int fileindex = 0;
		foreach (XmlElement nodeFile in doc.DocumentElement.SelectNodes("file"))
		{
			if (fileindex > 0)
				sb.Append(",");
			sb.Append("{");
			sb.Append("guid:");
			sb.Append(EncodeJScriptVariable(nodeFile.GetAttribute("guid")));
			sb.Append(",name:");
			sb.Append(EncodeJScriptVariable(nodeFile.GetAttribute("name")));
			sb.Append(",size:");
			sb.Append(EncodeJScriptVariable(nodeFile.GetAttribute("size")));
			sb.Append("}");

			fileindex++;
		}
		sb.Append("]");
		
		sb.Append("}");
		context.Response.Write(sb.ToString());
		
    }

	void WriteError(string status,string message)
	{
		context.Response.Write("{error:'" + status + "',errormessage:" + EncodeJScriptVariable (message)+ "}");
	}
 
    public bool IsReusable {
        get {
            return false;
        }
    }



	static public string EncodeJScriptVariable(string str)
	{
		if (str == null) return "null";
		return "'" + EncodeJScriptString(str) + "'";
	}
	static public string EncodeJScriptString(string str)
	{
		if (str == null) return string.Empty;
		Regex re = new Regex("\\\\|\\\"|\\\r|\\\n|\\\'|\\<|\\>|\\&", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
		return re.Replace(str, new MatchEvaluator(EncodeJScriptString_1));
	}
	static private string EncodeJScriptString_1(Match m)
	{
		int code = (int)m.Value[0];
		string chars = "0123456789ABCDEF";
		int a1 = code & 0xF;
		int a2 = (code & 0xF0) / 0x10;

		return "\\x" + chars[a2] + chars[a1];
	}

}


