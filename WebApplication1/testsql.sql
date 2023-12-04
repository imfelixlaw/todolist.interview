USE [todo]
GO
ALTER TABLE [dbo].[users] DROP CONSTRAINT [DF_users_status]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/19/2023 3:31:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
DROP TABLE [dbo].[users]
GO
/****** Object:  Table [dbo].[todo]    Script Date: 11/19/2023 3:31:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[todo]') AND type in (N'U'))
DROP TABLE [dbo].[todo]
GO
USE [master]
GO
/****** Object:  Database [todo]    Script Date: 11/19/2023 3:31:50 PM ******/
DROP DATABASE [todo]
GO
/****** Object:  Database [todo]    Script Date: 11/19/2023 3:31:50 PM ******/
CREATE DATABASE [todo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'todo', FILENAME = N'/var/opt/mssql/data/todo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'todo_log', FILENAME = N'/var/opt/mssql/data/todo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [todo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [todo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [todo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [todo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [todo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [todo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [todo] SET ARITHABORT OFF 
GO
ALTER DATABASE [todo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [todo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [todo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [todo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [todo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [todo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [todo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [todo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [todo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [todo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [todo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [todo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [todo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [todo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [todo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [todo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [todo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [todo] SET RECOVERY FULL 
GO
ALTER DATABASE [todo] SET  MULTI_USER 
GO
ALTER DATABASE [todo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [todo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [todo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [todo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [todo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [todo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'todo', N'ON'
GO
ALTER DATABASE [todo] SET QUERY_STORE = ON
GO
ALTER DATABASE [todo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [todo]
GO
/****** Object:  Table [dbo].[todo]    Script Date: 11/19/2023 3:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[todo](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[duedate] [date] NULL,
	[status] [tinyint] NOT NULL,
	[tags] [nvarchar](255) NULL,
	[owner] [uniqueidentifier] NOT NULL,
	[priority] [tinyint] NULL,
 CONSTRAINT [PK_todo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/19/2023 3:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [uniqueidentifier] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[todo] ([id], [name], [description], [duedate], [status], [tags], [owner], [priority]) VALUES (N'19d7cf8e-789a-4796-8b2b-78d7b5e80072', N'testing new todo 1', N'new todo testing', CAST(N'2023-11-19' AS Date), 0, NULL, N'f3285af3-3600-4fcf-a784-81abff7ed8e7', NULL)
GO
INSERT [dbo].[todo] ([id], [name], [description], [duedate], [status], [tags], [owner], [priority]) VALUES (N'fe342c2f-5423-4b67-94f8-cad82b4a2346', N'testing new todo 1', N'new todo testing', CAST(N'2023-11-19' AS Date), 0, NULL, N'f3285af3-3600-4fcf-a784-81abff7ed8e7', NULL)
GO
INSERT [dbo].[todo] ([id], [name], [description], [duedate], [status], [tags], [owner], [priority]) VALUES (N'0e9b5cd1-bfb1-4406-b5b4-cf4a9ba2af33', N'test3', N'test3', NULL, 2, N'', N'f3285af3-3600-4fcf-a784-81abff7ed8e7', NULL)
GO
INSERT [dbo].[todo] ([id], [name], [description], [duedate], [status], [tags], [owner], [priority]) VALUES (N'b18f289b-4d94-4c34-88cc-efe5259714cf', N'testing new todo 1', N'Just to update new desc', CAST(N'2024-11-19' AS Date), 0, NULL, N'f3285af3-3600-4fcf-a784-81abff7ed8e7', NULL)
GO
INSERT [dbo].[todo] ([id], [name], [description], [duedate], [status], [tags], [owner], [priority]) VALUES (N'da2d2e3b-5cab-4eea-ae04-f96414ad8747', N'test1', N'test1', NULL, 1, N'', N'f3285af3-3600-4fcf-a784-81abff7ed8e7', NULL)
GO
INSERT [dbo].[users] ([id], [email], [password], [status]) VALUES (N'f3285af3-3600-4fcf-a784-81abff7ed8e7', N'user1', N'password1', 1)
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_status]  DEFAULT ((1)) FOR [status]
GO
USE [master]
GO
ALTER DATABASE [todo] SET  READ_WRITE 
GO
