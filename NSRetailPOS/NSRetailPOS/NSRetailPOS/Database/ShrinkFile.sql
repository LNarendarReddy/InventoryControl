
SELECT name ,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS AvailableSpaceInMB, FILEPROPERTY(name, 'SpaceUsed') / 128.0 AS SpaceUsed
FROM sys.database_files;

--DBCC SHRINKFILE (Database1, 8)
--DBCC SHRINKFILE (Database1_log, 8)




SELECT name ,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS AvailableSpaceInMB, FILEPROPERTY(name, 'SpaceUsed') / 128.0 AS SpaceUsed
FROM sys.database_files;
GO

ALTER DATABASE [NSRetail_Cloud] SET RECOVERY SIMPLE WITH NO_WAIT
GO

DBCC SHRINKFILE ('NSRetail_Cloud_Data', 8) 
DBCC SHRINKFILE ('NSRetail_Cloud_Log', 8)
GO

ALTER DATABASE [NSRetail_Cloud] SET RECOVERY FULL WITH NO_WAIT
GO

SELECT name ,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS AvailableSpaceInMB, FILEPROPERTY(name, 'SpaceUsed') / 128.0 AS SpaceUsed
FROM sys.database_files;