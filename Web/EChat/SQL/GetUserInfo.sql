SELECT 
	[UserId]
    ,[UserName]
    ,[PasswordType]
    ,[PasswordSalt]
    ,[Password]
    ,[IsAdministrator]
FROM
	[ChatApp].[dbo].[Users]
WHERE
    [UserId] = @userid