@echo off  

:: ������wintab32.dll�ĳ���
setlocal enabledelayedexpansion

FOR /F "delims=" %%i in ('"%~dp0listdlls.exe" -d wintab32.dll') do (
	echo %%i | find "pid:" 

:: �����г������ȷ��
	if !errorlevel! equ 0 (
		FOR /F "delims= " %%j in ("%%i") do (
:: �ٴβ��ҳ�������
			echo %%j | find ".exe" 		
			if !errorlevel! equ 0 taskkill /f /t /im %%j		
			)
		)
	)
