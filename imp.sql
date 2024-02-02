USE [master]
GO
/****** Object:  Database [ImperiaD]    Script Date: 24.12.2023 20:07:36 ******/
CREATE DATABASE [ImperiaD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ImperiaD', FILENAME = N'K:\sql\MSSQL15.MSSQLSERVER\MSSQL\DATA\ImperiaD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ImperiaD_log', FILENAME = N'K:\sql\MSSQL15.MSSQLSERVER\MSSQL\DATA\ImperiaD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ImperiaD] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ImperiaD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ImperiaD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ImperiaD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ImperiaD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ImperiaD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ImperiaD] SET ARITHABORT OFF 
GO
ALTER DATABASE [ImperiaD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ImperiaD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ImperiaD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ImperiaD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ImperiaD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ImperiaD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ImperiaD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ImperiaD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ImperiaD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ImperiaD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ImperiaD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ImperiaD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ImperiaD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ImperiaD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ImperiaD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ImperiaD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ImperiaD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ImperiaD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ImperiaD] SET  MULTI_USER 
GO
ALTER DATABASE [ImperiaD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ImperiaD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ImperiaD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ImperiaD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ImperiaD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ImperiaD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ImperiaD] SET QUERY_STORE = OFF
GO
USE [ImperiaD]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_users] [int] NULL,
	[LoginDateTime] [datetime] NULL,
	[TypeVxod] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[merch]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[merch](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[photo] [nvarchar](50) NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [text] NOT NULL,
	[manufacturer] [nvarchar](50) NOT NULL,
	[price] [int] NOT NULL,
	[discount] [int] NULL,
 CONSTRAINT [PK_merch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_users] [int] NOT NULL,
	[id_status] [int] NOT NULL,
	[id_point] [int] NOT NULL,
	[order_date] [date] NOT NULL,
	[code] [int] NOT NULL,
	[cost] [int] NOT NULL,
	[discount] [int] NULL,
	[total_cost] [int] NULL,
	[delivery_days] [int] NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[point]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[point](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_point] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sostav]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sostav](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_orders] [int] NOT NULL,
	[id_merch] [int] NOT NULL,
	[count] [int] NULL,
	[quantity] [int] NULL,
	[total_cost] [int] NULL,
	[discount] [int] NULL,
 CONSTRAINT [PK_sostav] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[status]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[status](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[status_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[type_user]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[type_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_type_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 24.12.2023 20:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[photo] [nvarchar](50) NULL,
	[id_type] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LoginHistory] ON 

INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (1, 3, CAST(N'2023-12-24T19:01:39.283' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (2, 2, CAST(N'2023-12-24T19:02:29.773' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (3, 1, CAST(N'2023-12-24T19:02:39.547' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (4, 3, CAST(N'2023-12-24T19:07:03.723' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (5, 3, CAST(N'2023-12-24T19:07:08.097' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (6, 1, CAST(N'2023-12-24T19:07:29.933' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (7, 3, CAST(N'2023-12-24T19:08:43.363' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (8, 3, CAST(N'2023-12-24T19:41:34.910' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (9, 1, CAST(N'2023-12-24T19:41:47.380' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (10, 1, CAST(N'2023-12-24T19:43:24.773' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (11, 1, CAST(N'2023-12-24T19:51:00.160' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (12, 1, CAST(N'2023-12-24T19:59:36.930' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (13, 1, CAST(N'2023-12-24T20:00:41.033' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (14, 3, CAST(N'2023-12-24T20:02:11.183' AS DateTime), N'Успешно')
INSERT [dbo].[LoginHistory] ([id], [id_users], [LoginDateTime], [TypeVxod]) VALUES (15, 2, CAST(N'2023-12-24T20:02:32.617' AS DateTime), N'Успешно')
SET IDENTITY_INSERT [dbo].[LoginHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[merch] ON 

INSERT [dbo].[merch] ([id], [photo], [name], [description], [manufacturer], [price], [discount]) VALUES (1, N'/photo/kurtkaDiesel.jpeg', N'Куртка ', N'Куртка DIESEL для мальчика', N'Италия', 16770, NULL)
INSERT [dbo].[merch] ([id], [photo], [name], [description], [manufacturer], [price], [discount]) VALUES (2, N'/photo/tolstDiesel.png', N'Толстовка', N'Толстовка DIESEL для девочки', N'Италия', 16100, NULL)
INSERT [dbo].[merch] ([id], [photo], [name], [description], [manufacturer], [price], [discount]) VALUES (3, N'/photo/kurtkaBarKids.jpeg', N'Куртка', N'Куртка BARROW KIDS для мальчика', N'Италия', 32290, 10)
INSERT [dbo].[merch] ([id], [photo], [name], [description], [manufacturer], [price], [discount]) VALUES (4, N'/photo/dzinsPinko.jpg', N'Джинсы', N'Джинсы PINKO для девочки', N'Италия', 16640, NULL)
INSERT [dbo].[merch] ([id], [photo], [name], [description], [manufacturer], [price], [discount]) VALUES (5, N'/photo/rukzDiesel.png', N'Рюкзак', N'Рюкзак DIESEL для мальчика', N'Италия', 16830, NULL)
INSERT [dbo].[merch] ([id], [photo], [name], [description], [manufacturer], [price], [discount]) VALUES (6, N'/photo/tolstBarrow.png', N'Толстовка', N'Толстовка BARROW KIDS для девочки', N'Италия', 19560, NULL)
SET IDENTITY_INSERT [dbo].[merch] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([id], [id_users], [id_status], [id_point], [order_date], [code], [cost], [discount], [total_cost], [delivery_days]) VALUES (1, 3, 1, 1, CAST(N'2023-12-24' AS Date), 454, 32290, 10, 32280, 6)
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[point] ON 

INSERT [dbo].[point] ([id], [name]) VALUES (1, N'ЦЕНТРАЛЬНЫЙ ДЕТСКИЙ МАГАЗИН')
INSERT [dbo].[point] ([id], [name]) VALUES (2, N'ТРЦ ЕВРОПЕЙСКИЙ')
INSERT [dbo].[point] ([id], [name]) VALUES (3, N'ТРЦ РИО')
INSERT [dbo].[point] ([id], [name]) VALUES (4, N'ТРЦ "Океания" 3 этаж')
INSERT [dbo].[point] ([id], [name]) VALUES (5, N'Outlet Village Белая Дача')
INSERT [dbo].[point] ([id], [name]) VALUES (6, N'ТЦ "АЭРОБУС"')
INSERT [dbo].[point] ([id], [name]) VALUES (7, N'Vnukovo Premium Outlet')
INSERT [dbo].[point] ([id], [name]) VALUES (8, N'ТРК "ВЕГАС"')
INSERT [dbo].[point] ([id], [name]) VALUES (9, N'Сочи

- ТГ GRAND MARINA')
INSERT [dbo].[point] ([id], [name]) VALUES (10, N'Вологда
-
Магия Детства, ТЦ Форум, 3 этаж')
SET IDENTITY_INSERT [dbo].[point] OFF
GO
SET IDENTITY_INSERT [dbo].[sostav] ON 

INSERT [dbo].[sostav] ([id], [id_orders], [id_merch], [count], [quantity], [total_cost], [discount]) VALUES (1, 1, 3, 1, 1, 32290, 10)
SET IDENTITY_INSERT [dbo].[sostav] OFF
GO
SET IDENTITY_INSERT [dbo].[status] ON 

INSERT [dbo].[status] ([id], [status_name]) VALUES (1, N'Новый
')
INSERT [dbo].[status] ([id], [status_name]) VALUES (2, N'Отменен')
INSERT [dbo].[status] ([id], [status_name]) VALUES (3, N'Завершен')
SET IDENTITY_INSERT [dbo].[status] OFF
GO
SET IDENTITY_INSERT [dbo].[type_user] ON 

INSERT [dbo].[type_user] ([id], [role]) VALUES (1, N'Администратор')
INSERT [dbo].[type_user] ([id], [role]) VALUES (2, N'Менеджер')
INSERT [dbo].[type_user] ([id], [role]) VALUES (3, N'Клиент')
SET IDENTITY_INSERT [dbo].[type_user] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [login], [password], [name], [photo], [id_type]) VALUES (1, N'halida', N'12345', N'Халида', N'/res/hal.png', 1)
INSERT [dbo].[users] ([id], [login], [password], [name], [photo], [id_type]) VALUES (2, N'elena', N'12345', N'Елена', N'/res/lena.png', 2)
INSERT [dbo].[users] ([id], [login], [password], [name], [photo], [id_type]) VALUES (3, N'dozo', N'12345', N'dozopravka', N'/res/dp.png', 3)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_users] FOREIGN KEY([id_users])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_users]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_point] FOREIGN KEY([id_point])
REFERENCES [dbo].[point] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_point]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_status] FOREIGN KEY([id_status])
REFERENCES [dbo].[status] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_status]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_users] FOREIGN KEY([id_users])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_users]
GO
ALTER TABLE [dbo].[sostav]  WITH CHECK ADD  CONSTRAINT [FK_sostav_merch] FOREIGN KEY([id_merch])
REFERENCES [dbo].[merch] ([id])
GO
ALTER TABLE [dbo].[sostav] CHECK CONSTRAINT [FK_sostav_merch]
GO
ALTER TABLE [dbo].[sostav]  WITH CHECK ADD  CONSTRAINT [FK_sostav_orders] FOREIGN KEY([id_orders])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[sostav] CHECK CONSTRAINT [FK_sostav_orders]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_type_user] FOREIGN KEY([id_type])
REFERENCES [dbo].[type_user] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_type_user]
GO
USE [master]
GO
ALTER DATABASE [ImperiaD] SET  READ_WRITE 
GO
