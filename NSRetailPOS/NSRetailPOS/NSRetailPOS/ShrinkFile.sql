
SELECT name ,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS AvailableSpaceInMB, FILEPROPERTY(name, 'SpaceUsed') / 128.0 AS SpaceUsed
FROM sys.database_files;

--DBCC SHRINKFILE (Database1, 8)
--DBCC SHRINKFILE (Database1_log, 8)
