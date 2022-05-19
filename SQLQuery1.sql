CREATE DATABASE transs;
Go 
USE [transs]
GO
/****** Object:  Table [dbo].[cars]    Script Date: 17/05/2022 6:28:29 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cars](
	[id] [int] IDENTITY(1, 1) NOT NULL ,
	[employee_id] [int] NOT NULL,
	[Make] [varchar](64) NOT NULL,
	[Model] [varchar](64) NOT NULL,
	[under_warranty] [bit] NOT NULL,
	[insurance] [smalldatetime] NOT NULL,
	[added_date] [smalldatetime] DEFAULT GetDate() NOT NULL,
 CONSTRAINT [PK_cars] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 17/05/2022 6:28:29 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[id] [int] IDENTITY(1, 1) NOT NULL,
	[firstname] [varchar](64) NOT NULL,
	[lastname] [varchar](64) NOT NULL,
	[phone] [varchar](12) NOT NULL,
 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[repairs]    Script Date: 17/05/2022 6:28:29 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[repairs](
	[id] [int] IDENTITY(1, 1) NOT NULL,
	[car_id] [int] NOT NULL,
	[details] [text] NOT NULL,
	[date] [smalldatetime] DEFAULT GetDate() NOT NULL,
 CONSTRAINT [PK_repairs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 17/05/2022 6:28:29 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1, 1) NOT NULL,
	[login] [varchar](64) NOT NULL,
	[password] [varchar](18) NOT NULL,
	[user_mail] [varchar](50) NOT NULL,
	[last_login_date] [smalldatetime] DEFAULT GetDate() NOT NULL,
	[counter] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cars]  WITH CHECK ADD  CONSTRAINT [FK_cars_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([id])
GO
ALTER TABLE [dbo].[cars] CHECK CONSTRAINT [FK_cars_employee]
GO
ALTER TABLE [dbo].[repairs]  WITH CHECK ADD  CONSTRAINT [FK_repairs_cars] FOREIGN KEY([car_id])
REFERENCES [dbo].[cars] ([id])
GO
ALTER TABLE [dbo].[repairs] CHECK CONSTRAINT [FK_repairs_cars]
GO