@echo off
cls
REM
setlocal enableextensions enabledelayedexpansion

Set FullString=C:\Repositories\Teste\hooks\start-commit.cmd
Set Remove=o

Echo Set Result=%%FullString:%Remove%=%%>rmv.bat&call rmv.bat&del rmv.bat

REM
 Echo FullString = %FullString%
REM
 Echo Remove = %Remove%
REM
 echo Result = %Result%

rem
endlocal
