Set Arquivo="c:\Arquivo.txt"

setlocal enableextensions enabledelayedexpansion

Set FullScript=%0
Set REPOS="%1"
Echo Set ScriptFile=%%FullScript:%REPOS%\hooks\=%%>a.bat&call a.bat&del a.bat
endlocal

if "%ScriptFile%" == "start-commit.cmd" (
	Set USER="%2"
)
if "%ScriptFile%" == "pre-commit.cmd" (
	Set TXNNAME="%2"
)
if "%ScriptFile%" == "post-commit.cmd" (
	Set REV="%2"
)
Set CAPAB="%3"

echo Script = %Script%>>%Arquivo%
echo REPOS = %REPOS%>>%Arquivo%
echo REV = %REV%>>%Arquivo%
echo TXNNAME = %TXNNAME%>>%Arquivo%
echo USER = %USER%>>%Arquivo%
echo CAPAB = %CAPAB%>>%Arquivo%
Echo.>>%Arquivo%
Echo.>>%Arquivo%

