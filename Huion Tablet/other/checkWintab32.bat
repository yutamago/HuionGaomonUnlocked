@echo off  
  
:: BatchGotAdmin  
:-------------------------------------  
REM  --> Check for permissions  
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"  
  
REM --> If error flag set, we do not have admin.  
if '%errorlevel%' NEQ '0' (  
    echo Requesting administrative privileges...  
    goto UACPrompt  
) else ( goto gotAdmin )  
  
:UACPrompt  
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"  
    echo UAC.ShellExecute "%~s0", "", "", "runas", 1 >> "%temp%\getadmin.vbs"  
  
    "%temp%\getadmin.vbs"  
    exit /B  
  
:gotAdmin  
    if exist "%temp%\getadmin.vbs" ( del "%temp%\getadmin.vbs" )  
    pushd "%CD%"  
    CD /D "%~dp0"  
:-------------------------------------- 
echo;

:: 查找有wintab32.dll的程序
setlocal enabledelayedexpansion

FOR /F "delims=" %%i in ('"%~dp0listdlls.exe" -d wintab32.dll') do (
	echo %%i | find "pid:" 

:: 查找有程序的正确行
	if !errorlevel! equ 0 (
		FOR /F "delims= " %%j in ("%%i") do (
:: 再次查找程序名字
			echo %%j | find ".exe" 		
			if !errorlevel! equ 0 taskkill /f /t /im %%j		
			)
		)
	)
