RMDIR /S /Q bin
RMDIR /S /Q obj
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe Fb.csproj
copy /Y lib\*.* bin\Debug
bin\Debug\Fb.exe
pause