
SET expectedHost=www.google.com

PING -n 1 %expectedHost%

IF ERRORLEVEL 1 GOTO PINGERROR

EXIT /B 0

:PINGERROR
ECHO Error
IPCONFIG /release
IPCONFIG /renew
EXIT /B 1
