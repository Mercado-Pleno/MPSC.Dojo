<%@ Page language="VB"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en"> 
<head>
	<title>Demo - Ajax Uploader in ASP.NET 1.1</title>
    <link rel="stylesheet" href="../main.css" type="text/css" />
</head>

<body bottomMargin="0" leftMargin="0" topMargin="5">
   <form id="Form1" method="post" runat="server">
   <div id="Common">
		<div id="CommonHeader">	
			<a href="../default.htm"><img alt="Ajax Uploader" src="../sampleimages/logo.gif" border=0></a>
			<br />
		</div>
		<div class="CommonTabBar">
			<a class="CommonSimpleTabStripTab" href="../default.htm" title="Home">Home</a> | 
			<a class="CommonSimpleTabStripSelectedTab" href="default.aspx">Demo(VB)</a> | 
			<a class="CommonSimpleTabStripTab" href="../Deployment.htm">Deployment</a> | 
			<a class="CommonSimpleTabStripTab" href="http://CuteSoft.net/forums/" title="File Upload Forums">Forums</a> | 
			<a class="CommonSimpleTabStripTab" href="http://ajaxuploader.com/Order.aspx" title="Purchase">Order</a>
        </div>
		<div class="CommonBody">
	       
			<table cellspacing="15" cellpadding="0" width="100%" border="0">
			    <tr>
			        <td colspan="2">
			            <h4>Online Demo (VB)</h4>
	                    <p>In order to assist developers evaluate Ajax Uploader, as well as to provide tools for rapid application development, we supply several samples and online interactive demonstrations.</p>
          
			        </td>
			    </tr>
				<tr>
				    <td valign="top">
		                <p>
                            <b>1. <a href="simple-upload.aspx" target="_blank">Simple Upload with Progress</a></b> <br />
                            A basic sample demonstrating the use of the Upload control.
                        </p>
                        <p>
                            <b>2. <a href="selecting-multiple-files.aspx" target="_blank">Selecting multiple files for upload </a></b> <br />
                           Ajax Uploader allows you to select multiple files and upload multiple files at once. 
                        </p>
                        <p>
                            <b>3. <a href="simple-upload-UI.aspx" target="_blank">Simple Upload with Progress (Customizing the UI)</a></b> <br />
                            A sample demonstrating how to customize the look and feel of file upload controls. 
                        </p>
                        <p>
                            <b>4. <a href="simple-upload-Validation.aspx" target="_blank">Simple Upload with Progress (Custom Validation)</a></b> <br />
                            A sample demonstrating how to create user-defined validation functions for an upload control.
                        </p>
                        <p>
                            <b>5. <a href="multiple-files-upload.aspx" target="_blank">Uploading multiple files like GMail</a></b> <br />
                           Google's GMail has a nice way of allowing you to upload multiple files. Rather than showing you 10 file upload boxes at once, the user attaches a file, you can click a button to add another attachment. 
                        </p>
				        <p>
                            <b>6. <a href="Large-File-Upload.aspx" target="_blank">Large File Upload</a></b> <br />
                          Ajax Uploader allows you to upload large files to a server with the low server memory consumption.
                        </p>
                    </td>				    
				    <td valign="top">
                        <p>
                            <b>7. <a href="Persist-upload-file.aspx" target="_blank">Persist Uploaded File through Postback</a></b> <br />
                            A sample demonstrating how to persist the selected files during page postbacks. Ajax Uploader can hold an object temporarily and you can save it anytime you want.
                        </p>
                        <p>
                            <b>8. <a href="Simulate-File-Upload.aspx" target="_blank">Simulate File Upload</a></b> <br />
                            A sample demonstrating how to use .Upload method to simulate a file upload event.
                        </p>
			      			            <p>
				            <b>9. <a href="Start-uploading-manually.aspx" target="_blank">Start uploading manually</a></b> <br />
				            This sample demonstrates how to start uploading manually after file selection vs automatically.
			            </p>
			        </td>
				</tr>
			</table>           
            
            <br />
		    <div id="footer">
			    <p><a href="http://cutesoft.net">Copyright 2003-2008 CuteSoft.Net. All rights reserved.</a></p>
		    </div>
		</div>
	</div>
	</form>
</body>
</html>