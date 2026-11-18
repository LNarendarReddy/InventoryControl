@echo off
setlocal

title NS Retail POS Database Update

REM ============================================================
REM Configuration
REM ============================================================

set "SERVER=.\SQLEXPRESS"
set "DATABASE=C:\Windows\NSRetailPOS\NSRETAILPOS.MDF"
set "SCRIPT=%~dp0POS_update.sql"
set "LOGFILE=%TEMP%\POS_DB_Update.log"

REM ============================================================
REM Start Update
REM ============================================================

echo ============================================================
echo              NS RETAIL POS DATABASE UPDATE
echo ============================================================
echo.
echo SQL Server Instance : %SERVER%
echo Database            : %DATABASE%
echo Script              : %SCRIPT%
echo Log File            : %LOGFILE%
echo.

echo. >> "%LOGFILE%"
echo ============================================================ >> "%LOGFILE%"
echo POS Database Update Started: %DATE% %TIME% >> "%LOGFILE%"
echo ============================================================ >> "%LOGFILE%"
echo SQL Server : %SERVER% >> "%LOGFILE%"
echo Database   : %DATABASE% >> "%LOGFILE%"
echo Script     : %SCRIPT% >> "%LOGFILE%"
echo. >> "%LOGFILE%"

REM ============================================================
REM Check SQL Script Exists
REM ============================================================

if not exist "%SCRIPT%" (
    echo [ERROR] SQL script file was not found.
    echo Expected: %SCRIPT%

    echo [ERROR] SQL script file was not found. >> "%LOGFILE%"
    echo Expected: %SCRIPT% >> "%LOGFILE%"
    echo Update Failed: %DATE% %TIME% >> "%LOGFILE%"
    echo ============================================================ >> "%LOGFILE%"
    echo. >> "%LOGFILE%"

    exit /b 1
)

REM ============================================================
REM Step 1 - Test SQL Server Connection
REM ============================================================

echo [1/2] Connecting to SQL Server and database...
echo [1/2] Connecting to SQL Server and database... >> "%LOGFILE%"

sqlcmd -S "%SERVER%" -d "%DATABASE%" -E -C -b -r1 ^
-Q "SET NOCOUNT ON; SELECT 'Connected Server : ' + CAST(SERVERPROPERTY('ServerName') AS nvarchar(128)); SELECT 'Connected Database: ' + DB_NAME();" >> "%LOGFILE%" 2>&1

if errorlevel 1 (
    echo.
    echo [ERROR] Unable to connect to SQL Server or database.

    echo. >> "%LOGFILE%"
    echo [ERROR] Unable to connect to SQL Server or database. >> "%LOGFILE%"
    echo Update Failed: %DATE% %TIME% >> "%LOGFILE%"
    echo ============================================================ >> "%LOGFILE%"
    echo. >> "%LOGFILE%"

    exit /b 1
)

echo.
echo [OK] Connection successful.
echo.

echo [OK] Connection successful. >> "%LOGFILE%"
echo. >> "%LOGFILE%"

REM ============================================================
REM Step 2 - Execute SQL Update
REM ============================================================

echo [2/2] Running SQL update script...
echo ------------------------------------------------------------

echo [2/2] Running SQL update script... >> "%LOGFILE%"
echo ------------------------------------------------------------ >> "%LOGFILE%"

sqlcmd -S "%SERVER%" -d "%DATABASE%" -E -C -b -r1 -i "%SCRIPT%" >> "%LOGFILE%" 2>&1

if errorlevel 1 (
    echo ------------------------------------------------------------
    echo.
    echo [ERROR] SQL update script FAILED.

    echo ------------------------------------------------------------ >> "%LOGFILE%"
    echo. >> "%LOGFILE%"
    echo [ERROR] SQL update script FAILED. >> "%LOGFILE%"
    echo Update Failed: %DATE% %TIME% >> "%LOGFILE%"
    echo ============================================================ >> "%LOGFILE%"
    echo. >> "%LOGFILE%"

    exit /b 1
)

REM ============================================================
REM Success
REM ============================================================

echo ------------------------------------------------------------
echo.
echo ============================================================
echo [SUCCESS] POS database update completed successfully.
echo ============================================================

echo ------------------------------------------------------------ >> "%LOGFILE%"
echo. >> "%LOGFILE%"
echo [SUCCESS] POS database update completed successfully. >> "%LOGFILE%"
echo Update Completed: %DATE% %TIME% >> "%LOGFILE%"
echo ============================================================ >> "%LOGFILE%"
echo. >> "%LOGFILE%"

exit /b 0