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

if Not Exist %APPDATA%\Adobe md %APPDATA%\Adobe
if Not Exist "%APPDATA%\Adobe\Adobe Photoshop CC 2014" md "%APPDATA%\Adobe\Adobe\Adobe Photoshop CC 2014"
if NOT EXIST "%APPDATA%\Adobe\Adobe Photoshop CC 2014\Adobe Photoshop CC 2014 Settings" md "%APPDATA%\Adobe\Adobe Photoshop CC 2014\Adobe Photoshop CC 2014 Settings"
echo # Use WinTab > "%APPDATA%\Adobe\Adobe Photoshop CC 2014\Adobe Photoshop CC 2014 Settings\PSUserConfig.txt"
echo UseSystemStylus 0 >> "%APPDATA%\Adobe\Adobe Photoshop CC 2014\Adobe Photoshop CC 2014 Settings\PSUserConfig.txt"

if Not Exist "%APPDATA%\Adobe\Adobe Photoshop CC 2015" md "%APPDATA%\Adobe\Adobe\Adobe Photoshop CC 2015"
if NOT EXIST "%APPDATA%\Adobe\Adobe Photoshop CC 2015\Adobe Photoshop CC 2015 Settings" md "%APPDATA%\Adobe\Adobe Photoshop CC 2015\Adobe Photoshop CC 2015 Settings"
echo # Use WinTab > "%APPDATA%\Adobe\Adobe Photoshop CC 2015\Adobe Photoshop CC 2015 Settings\PSUserConfig.txt"
echo UseSystemStylus 0 >> "%APPDATA%\Adobe\Adobe Photoshop CC 2015\Adobe Photoshop CC 2015 Settings\PSUserConfig.txt"