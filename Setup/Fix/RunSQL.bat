@echo off
@echo executing POS script
sqlcmd -S .\SQLEXPRESS -i POS_update.sql
rem sqlcmd -S (LocalDB)\MSSQLLocalDB -i POS_update.sql
@echo script complete
pause


