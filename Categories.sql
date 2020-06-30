/*
 Navicat Premium Data Transfer

 Source Server         : Sql Server Local
 Source Server Type    : SQL Server
 Source Server Version : 12004100
 Source Host           : 127.0.01:1433
 Source Catalog        : ThucTapNhom
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 12004100
 File Encoding         : 65001

 Date: 30/06/2020 22:30:28
*/


-- ----------------------------
-- Table structure for Categories
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type IN ('U'))
	DROP TABLE [dbo].[Categories]
GO

CREATE TABLE [dbo].[Categories] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Name] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CreatedAt] datetime  NOT NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL,
  [Status] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Categories] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Categories
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Categories] ON
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [CreatedAt], [UpdatedAt], [DeletedAt], [Status]) VALUES (N'1', N'thể hình', N'2020-06-30 22:27:46.000', NULL, NULL, N'1')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [CreatedAt], [UpdatedAt], [DeletedAt], [Status]) VALUES (N'2', N'bơi lội', N'2020-06-30 22:28:03.000', NULL, NULL, N'1')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [CreatedAt], [UpdatedAt], [DeletedAt], [Status]) VALUES (N'3', N'thể dục nhóm', N'2020-06-30 22:28:24.000', NULL, NULL, N'1')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [CreatedAt], [UpdatedAt], [DeletedAt], [Status]) VALUES (N'4', N'yoga', N'2020-06-30 22:28:50.000', NULL, NULL, N'1')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [CreatedAt], [UpdatedAt], [DeletedAt], [Status]) VALUES (N'5', N'võ thuật tự do', N'2020-06-30 22:29:43.000', NULL, NULL, N'1')
GO

SET IDENTITY_INSERT [dbo].[Categories] OFF
GO


-- ----------------------------
-- Auto increment value for Categories
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Categories]', RESEED, 5)
GO


-- ----------------------------
-- Primary Key structure for table Categories
-- ----------------------------
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

