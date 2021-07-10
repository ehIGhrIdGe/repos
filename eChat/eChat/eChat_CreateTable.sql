USE Echat
 
CREATE TABLE [dbo].[ChatLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostAt] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[UserId] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChatLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
CREATE TABLE [dbo].[Users](
	[UserId] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PasswordType] [tinyint] NOT NULL,
	[PasswordSalt] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[IsAdministrator] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsAdministrator]  DEFAULT ((0)) FOR [IsAdministrator]
