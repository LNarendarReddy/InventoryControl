@echo off
@REM sqlcmd -S .\SQLEXPRESS -i POS_update.sql
sqlcmd -S (LocalDB)\MSSQLLocalDB -i POS_update.sql

echo script complete
pause