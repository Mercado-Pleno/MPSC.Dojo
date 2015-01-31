@echo off
cls
SetLocal enableextensions enabledelayedexpansion

Set String=C:\Repositories\Teste\hooks\start-commit.cmd
Set Remove=\
Echo Set Result=%%String:%Remove%=%%>rmv.bat&call rmv.bat&del rmv.bat
REM A linha acima vai gerar um arquivo com o seguinte conteúdo: Set Result=%String:\=%

REM
 Echo String = %String%
REM
 Echo Remove = %Remove%
REM
 echo Result = %Result%


Set String=%Result%
Set Remove=o
Echo Set Result=%%String:%Remove%=%%>rmv.bat&call rmv.bat&del rmv.bat
REM A linha acima vai gerar um arquivo com o seguinte conteúdo: Set Result=%String:o=%


REM
 Echo String = %String%
REM
 Echo Remove = %Remove%
REM
 echo Result = %Result%

EndLocal

Pause