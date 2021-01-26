SELECT
	[Id]
    ,[PostAt]
    ,[Message]
    ,[Name]
FROM
	[ChatApp].[dbo].[ChatLogs] C
INNER JOIN
	[ChatApp].[dbo].[Users] U
ON
	C.Name = U.UserName