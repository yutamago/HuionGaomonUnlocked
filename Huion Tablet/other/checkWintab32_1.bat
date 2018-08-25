@echo off  

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
