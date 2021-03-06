USE [master]
GO
/****** Object:  Database [dbo.UserAwards]    Script Date: 20.09.2020 3:08:45 ******/
CREATE DATABASE [dbo.UserAwards]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'User', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\User.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'User_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\User.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbo.UserAwards] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbo.UserAwards].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbo.UserAwards] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [dbo.UserAwards] SET ANSI_NULLS ON 
GO
ALTER DATABASE [dbo.UserAwards] SET ANSI_PADDING ON 
GO
ALTER DATABASE [dbo.UserAwards] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [dbo.UserAwards] SET ARITHABORT ON 
GO
ALTER DATABASE [dbo.UserAwards] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbo.UserAwards] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [dbo.UserAwards] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [dbo.UserAwards] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [dbo.UserAwards] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbo.UserAwards] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbo.UserAwards] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET RECOVERY FULL 
GO
ALTER DATABASE [dbo.UserAwards] SET  MULTI_USER 
GO
ALTER DATABASE [dbo.UserAwards] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbo.UserAwards] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbo.UserAwards] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbo.UserAwards] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbo.UserAwards] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbo.UserAwards] SET QUERY_STORE = OFF
GO
USE [dbo.UserAwards]
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Awards](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Awards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Links]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Links](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[AwardId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Links_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](32) NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Age] [int] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[Image] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[Links]  WITH CHECK ADD  CONSTRAINT [FK_Links_Awards] FOREIGN KEY([AwardId])
REFERENCES [dbo].[Awards] ([Id])
GO
ALTER TABLE [dbo].[Links] CHECK CONSTRAINT [FK_Links_Awards]
GO
ALTER TABLE [dbo].[Links]  WITH CHECK ADD  CONSTRAINT [FK_Links_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Links] CHECK CONSTRAINT [FK_Links_Users]
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_DeleteAwardById]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_DeleteAwardById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE 
	FROM Awards
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_DeleteLinkById]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_DeleteLinkById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE 
	FROM Links
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_DeleteUserById]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_DeleteUserById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE 
	FROM Users
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetAllAwards]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetAllAwards]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Awards
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetAllLinks]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetAllLinks]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Links
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetAllUsers]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetAllUsers] 
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Users
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetAwardById]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetAwardById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM Awards
	Where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetAwardByTitle]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetAwardByTitle]
	@Title nvarchar(30)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 *
	FROM Awards
	Where Title = @Title
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetAwardsByUserId]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetAwardsByUserId]
	@UserId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Awards.* 
	FROM (Users JOIN Links ON (Links.UserId = Users.Id)) JOIN Awards ON  (Links.AwardId = Awards.Id)
	WHERE Users.Id = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetRolesForUser]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetRolesForUser]
	@Name nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(*)
	FROM Users
	WHERE Name = @Name AND IsAdmin = 1
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetUserById]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetUserById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM Users
	Where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetUserByName]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetUserByName]
	@Name nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 * 
	FROM Users
	Where Name = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_GetUsersByAwardId]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_GetUsersByAwardId]
	@AwardId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Users.* 
	FROM (Links JOIN Awards ON (Links.AwardId = Awards.Id)) JOIN Users ON  (Links.UserId = Users.Id)
	WHERE Awards.Id = @AwardId
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_InsertAward]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_InsertAward]
	@Id uniqueidentifier,
	@Title nvarchar(30),
	@Image nvarchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Awards(Id, Title, Image)
	VALUES (@Id, @Title, @Image)
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_InsertLink]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_InsertLink]
	@Id uniqueidentifier,
	@UserId uniqueidentifier,
	@AwardId uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Links(Id, UserId, AwardId)
	VALUES (@Id, @UserId, @AwardId)
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_InsertUser]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_InsertUser]
	@Id uniqueidentifier,
	@Name nvarchar(20),
	@Password nvarchar(32) = NULL,
	@DateOfBirth datetime,
	@Age int,
	@IsAdmin bit,
	@Image nvarchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Users (Id, Name, Password, DateOfBirth, Age, IsAdmin, Image)
	VALUES (@Id, @Name, @Password, @DateOfBirth, @Age, @IsAdmin, @Image)
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_IsAccountExist]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_IsAccountExist]
	@Name nvarchar(20),
	@Password nvarchar(32)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(*)
	FROM Users
	WHERE Name = @Name AND Password = @Password
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_IsUserInRole]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_IsUserInRole]
	@Name nvarchar(20),
	@Role nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	IF @Role = 'admin' 
		SELECT COUNT(*)
		FROM Users
		WHERE Name = @Name AND IsAdmin = 1
	ELSE
		SELECT COUNT(*)
		FROM Users
		WHERE Name = @Name AND IsAdmin = 0
		
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_SetUserPassword]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_SetUserPassword]
	@Id uniqueidentifier,
	@Password nvarchar(32)
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE Users
	SET Password = @Password
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_UpdateAward]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_UpdateAward]
	@Id uniqueidentifier,
	@Image nvarchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE Awards
	SET Image = @Image
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UserAwards_UpdateUser]    Script Date: 20.09.2020 3:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAwards_UpdateUser]
	@Id uniqueidentifier,
	@Name nvarchar(20),
	@Password nvarchar(32) = NULL,
	@DateOfBirth datetime,
	@Age int,
	@IsAdmin bit,
	@Image nvarchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE Users
	SET Name = @Name, Password = @Password, DateOfBirth = @DateOfBirth, Age = @Age, IsAdmin = @IsAdmin, Image = @Image
	WHERE Id = @Id
END
GO
USE [master]
GO
ALTER DATABASE [dbo.UserAwards] SET  READ_WRITE 
GO
