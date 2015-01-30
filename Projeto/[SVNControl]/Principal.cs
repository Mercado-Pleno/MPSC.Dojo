namespace MPSC.SVNControl
{
	using System;
	using MPSC.SVNControl;
	using System.Threading;
	using System.Diagnostics;
	using System.Runtime.InteropServices;
	using System.IO;

	public static class Principal
	{
		[STAThread]
		public static int Main(String[] args)
		{
			SVNParam vSVNParam = new SVNParam(args);
			Principal.Write(vSVNParam.ToString());
			return 0;
		}

		private static void Write(String mensagem)
		{
			var vMensagem = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "\r\n" + mensagem + "\r\n";
			Principal.Write(vMensagem, Principal.OpenFileOutPut());
			Principal.Write(vMensagem, Console.OpenStandardOutput());
			Principal.Write(vMensagem, Console.OpenStandardError());
			Console.Write(vMensagem);
			Debug.Write(vMensagem);
		}

		private static Stream OpenFileOutPut()
		{
			StreamWriter vStreamWriter = new StreamWriter("c:/SVN/Log.txt", true);
			return vStreamWriter.BaseStream;
		}

		private static void Write(String mensagem, Stream stream)
		{
			foreach (byte chr in mensagem)
			{
				stream.WriteByte(chr);
				stream.Flush();
			}
			stream.Close();
			stream.Dispose();
		}
	}
}


/*
 
 
 
 here is the post-commit hook code:

set TODAYSDATE=%date%_%time%

set REV=%2
set REPOS=%1

set REPOS_NAME=myRepo

set URL=myURL
set SVN_BUILD=C:\SVN\build\%REPOS_NAME%
set SVN_SRC=http://192.168.0.1/svn/%REPOS_NAME%/trunk/
set DEPLOY=WEBSERVER
set USER=mparks
set PASS=*****
set DEPLOY_SRC=/cygdrive/c/SVN/build/%REPOS_NAME%

set DEPLOY_DEST=/cygdrive/j/Websites/%URL%/testFINIAL

set LOG=C:\SVN\logs\post_commit_log.txt

REM get who did update to log
FOR /f "tokens=*" %%a IN (
'svnlook author --revision %REV% %REPOS%'
) DO (
SET UPDUser=%%a
)

echo ============================= >> %LOG%
echo %REPOS_NAME% r%REV% %TODAYSDATE% %UPDUser% >> %LOG%

IF exist "%SVN_BUILD%" (
    echo Do Switch: >> %LOG%
    svn switch --username %USER% --password %PASS% %SVN_SRC% %SVN_BUILD% >> %LOG% 2>&1
) ELSE (
    echo initial checkout: >> %LOG%
    svn checkout --username %USER% --password %PASS% %SVN_SRC% %SVN_BUILD% >> %LOG% 2>&1
)
REM this line is not working
"C:\PROGRA~1\cwRsync\rsync" --recursive --archive --compress --verbose --quiet --chmod=ugo=rwX --exclude "**.svn**" --exclude "**uploads**" --exclude "/images/" %SVN_BUILD% %DEPLOY_DEST% >> %LOG% 2>&1
 
 
 
 
 
 
 
 */