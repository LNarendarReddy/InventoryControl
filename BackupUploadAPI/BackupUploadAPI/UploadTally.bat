@echo off
SET StartLocation=%cd%
SET InputPath=D:\To Delete\Bambino\Script_For_Existing_Installations
SET BackupPath=D:\To Delete\Bambino\Backup
SET TallyPath=C:\Program Files\7-Zip\7z.exe
cd "%InputPath%"
"%TallyPath%" a -r "%BackupPath%\myzip_%date%.7z" *
cd %StartLocation%
BackupUploadAPI.exe Tally