USE [UserAwards]
GO

/****** Объект: Table [dbo].[Users] Дата скрипта: 18.09.2020 20:06:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (20)    NOT NULL,
    [Password]    NVARCHAR (32)    NULL,
    [DateOfBirth] DATETIME         NOT NULL,
    [Age]         INT              NOT NULL,
    [IsAdmin]     BIT              NOT NULL,
    [Image]       NVARCHAR (MAX)   NULL
);


