@echo off
@echo off
cd /d %~dp0
echo 1:安装   2:卸载   0:退出
:s1
set /p input=请输入操作序号,按ENTER键确定:
IF %input% LSS 3 (
IF %input% GEQ 0 (
GOTO cho)
)
ECHO 您输入有误，请再次输入！
GOTO s1

:cho
if "%input%"=="1" goto 1
if "%input%"=="2" goto 2
if "%input%"=="0" goto 0


:1
set filename=Sy_Service.exe
set servicename=测试服务
set Frameworkdc=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
 
if exist "%Frameworkdc%" goto netOld 
:DispError 
echo 您的机器上没有安装 .net Framework 4.0,安装即将终止.
echo 您的机器上没有安装 .net Framework 4.0,安装即将终止  >InstallService.log
goto LastEnd 
:netOld 
cd %Frameworkdc%
echo 您的机器上安装了相应的.net Framework 4.0,可以安装本服务. 
echo 您的机器上安装了相应的.net Framework 4.0,可以安装本服务 >InstallService.log
echo.
echo. >>InstallService.log

C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe Sy_Service.exe
net start 测试服务
echo -----------------------------
echo         上业科技数据同步服务安装成功
echo -----------------------------
pause
exit


:2
net stop 测试服务
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe Sy_Service.exe /u
echo -----------------------------
echo         上业科技数据同步服务卸载成功
echo -----------------------------
pause
exit


:0
exit