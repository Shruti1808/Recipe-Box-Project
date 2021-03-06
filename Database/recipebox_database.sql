CREATE DATABASE [recipebox]
GO

USE [recipebox]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 3/1/2017 4:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categories_recipe]    Script Date: 3/1/2017 4:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories_recipe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[recipe_id] [int] NULL,
	[category_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[recipe]    Script Date: 3/1/2017 4:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[ingredients] [varchar](255) NULL,
	[instructions] [varchar](255) NULL,
	[cook_time] [varchar](255) NULL,
	[rating] [int] NULL
) ON [PRIMARY]

GO
