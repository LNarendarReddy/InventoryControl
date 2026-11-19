@ECHO OFF
if _%1_==_payload_  goto :payload

:getadmin
    echo %~nx0: elevating self
    set vbs=%temp%\getadmin.vbs
    echo Set UAC = CreateObject^("Shell.Application"^)                >> "%vbs%"
    echo UAC.ShellExecute "%~s0", "payload %~sdp0 %*", "", "runas", 1 >> "%vbs%"
    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
goto :eof

:payload
    echo %~nx0: running payload with parameters:
    echo %*
    echo ---------------------------------------------------
    cd /d %2
    shift
    shift
    @ECHO Initiating NS Retail POS kill
    TASKKILL /F /FI "IMAGENAME eq NSRetailPOS.exe"
    @ECHO.
    @ECHO POS Killed if running
    ROBOCOPY /IS /V /COPYALL /IT "%cd%" "C:\Windows\NSRetailPOS" *.* 
    @ECHO copy complete, launching POS
    cd /d C:\Windows\NSRetailPOS
    start NSRetailPOS.exe
goto :eof
