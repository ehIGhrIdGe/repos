SELECT TOP (@loadnum)
	[Id]
    ,[PostAt]
    ,[Message]
    ,[Name]
FROM
	[ChatApp].[dbo].[ChatLogs]
ORDER BY [Id] DESC