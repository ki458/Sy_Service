@echo off
@echo off
cd /d %~dp0
echo 1:��װ   2:ж��   0:�˳�
:s1
set /p input=������������,��ENTER��ȷ��:
IF %input% LSS 3 (
IF %input% GEQ 0 (
GOTO cho)
)
ECHO �������������ٴ����룡
GOTO s1

:cho
if "%input%"=="1" goto 1
if "%input%"=="2" goto 2
if "%input%"=="0" goto 0


:1
set filename=Sy_Service.exe
set servicename=���Է���
set Frameworkdc=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
 
if exist "%Frameworkdc%" goto netOld 
:DispError 
echo ���Ļ�����û�а�װ .net Framework 4.0,��װ������ֹ.
echo ���Ļ�����û�а�װ .net Framework 4.0,��װ������ֹ  >InstallService.log
goto LastEnd 
:netOld 
cd %Frameworkdc%
echo ���Ļ����ϰ�װ����Ӧ��.net Framework 4.0,���԰�װ������. 
echo ���Ļ����ϰ�װ����Ӧ��.net Framework 4.0,���԰�װ������ >InstallService.log
echo.
echo. >>InstallService.log

C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe Sy_Service.exe
net start ���Է���
echo -----------------------------
echo         ��ҵ�Ƽ�����ͬ������װ�ɹ�
echo -----------------------------
pause
exit


:2
net stop ���Է���
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe Sy_Service.exe /u
echo -----------------------------
echo         ��ҵ�Ƽ�����ͬ������ж�سɹ�
echo -----------------------------
pause
exit


:0
exit