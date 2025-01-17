USE [JMGroup]
GO
/****** Object:  Table [dbo].[Configuration]    Script Date: 2014/8/28 13:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Configuration](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](50) NULL,
	[job] [varchar](255) NULL,
	[package] [varchar](50) NULL,
	[task] [varchar](255) NULL,
	[name] [varchar](255) NOT NULL,
	[value] [varchar](5000) NOT NULL,
	[CreatedDate]  AS (getdate()),
	[CreatedBy] [varchar](255) NULL,
	[UpdatedDate] [datetime] NULL CONSTRAINT [DF_Configuration_UpdatedDate]  DEFAULT (getdate()),
	[UpdatedBy] [varchar](255) NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Staging_GL03]    Script Date: 2014/8/28 13:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_GL03](
	[GL03001] [varchar](1) NOT NULL,
	[GL03002] [varchar](12) NOT NULL,
	[GL03003] [varchar](25) NOT NULL,
	[GL03004] [varchar](35) NOT NULL,
	[GL03005] [varchar](35) NOT NULL,
	[GL03006] [varchar](12) NOT NULL,
	[GL03007] [nchar](1) NOT NULL,
	[GL03008] [varchar](2) NOT NULL,
	[GL03009] [varchar](12) NOT NULL,
	[GL03010] [varchar](12) NOT NULL,
	[GL03011] [varchar](12) NOT NULL,
	[GL03012] [varchar](12) NOT NULL,
	[GL03013] [varchar](12) NOT NULL,
	[GL03014] [nchar](1) NOT NULL,
	[GL03015] [nchar](2) NOT NULL,
	[GL03016] [nchar](1) NOT NULL,
	[GL03017] [varchar](12) NOT NULL,
	[GL03018] [varchar](2) NOT NULL,
	[GL03019] [varchar](2) NOT NULL,
	[GL03020] [varchar](8) NOT NULL,
 CONSTRAINT [Staging_GL031] PRIMARY KEY CLUSTERED 
(
	[GL03001] ASC,
	[GL03002] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staging_GL06]    Script Date: 2014/8/28 13:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_GL06](
	[GL06001] [varchar](50) NOT NULL,
	[GL06002] [varchar](50) NOT NULL,
	[GL06003] [datetime] NOT NULL,
	[GL06004] [numeric](28, 8) NOT NULL,
	[GL06005] [varchar](50) NOT NULL,
	[GL06006] [varchar](50) NOT NULL,
	[GL06007] [varchar](50) NOT NULL,
	[GL06008] [varchar](50) NOT NULL,
	[GL06009] [varchar](50) NOT NULL,
	[GL06010] [varchar](50) NOT NULL,
	[GL06011] [varchar](50) NOT NULL,
	[GL06012] [varchar](50) NOT NULL,
	[GL06013] [varchar](50) NOT NULL,
	[GL06014] [varchar](50) NOT NULL,
	[GL06015] [varchar](50) NOT NULL,
	[GL06016] [varchar](50) NOT NULL,
	[GL06017] [varchar](50) NOT NULL,
	[GL06018] [varchar](50) NOT NULL,
	[GL06019] [varchar](50) NOT NULL,
	[GL06020] [varchar](50) NOT NULL,
	[GL06021] [int] NOT NULL,
	[GL06022] [numeric](28, 8) NOT NULL,
	[GL06023] [varchar](50) NOT NULL,
	[GL06024] [varchar](50) NOT NULL,
	[GL06025] [varchar](50) NOT NULL,
	[GL06026] [varchar](50) NOT NULL,
	[GL06027] [varchar](50) NOT NULL,
	[GL06028] [varchar](50) NOT NULL,
	[GL06029] [varchar](50) NOT NULL,
	[GL06030] [varchar](50) NOT NULL,
	[GL06031] [varchar](50) NOT NULL,
	[GL06032] [varchar](50) NOT NULL,
	[GL06033] [numeric](28, 8) NOT NULL,
	[GL06034] [varchar](50) NOT NULL,
	[GL06035] [varchar](50) NOT NULL,
	[GL06036] [int] NOT NULL,
	[GL06037] [varchar](50) NOT NULL,
	[GL06038] [varchar](50) NOT NULL,
	[GL06039] [varchar](50) NOT NULL,
	[GL06040] [datetime] NOT NULL,
 CONSTRAINT [Staging_GL061] PRIMARY KEY CLUSTERED 
(
	[GL06016] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staging_GL52]    Script Date: 2014/8/28 13:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_GL52](
	[GL52001] [nchar](1) NOT NULL,
	[GL52002] [nchar](12) NOT NULL,
	[GL52003] [nchar](12) NOT NULL,
	[GL52004] [nchar](12) NOT NULL,
	[GL52005] [nchar](12) NOT NULL,
	[GL52006] [numeric](28, 8) NOT NULL,
	[GL52007] [numeric](28, 8) NOT NULL,
	[GL52008] [numeric](28, 8) NOT NULL,
	[GL52009] [numeric](28, 8) NOT NULL,
	[GL52010] [numeric](28, 8) NOT NULL,
	[GL52011] [numeric](28, 8) NOT NULL,
	[GL52012] [numeric](28, 8) NOT NULL,
	[GL52013] [numeric](28, 8) NOT NULL,
	[GL52014] [numeric](28, 8) NOT NULL,
	[GL52015] [numeric](28, 8) NOT NULL,
	[GL52016] [numeric](28, 8) NOT NULL,
	[GL52017] [numeric](28, 8) NOT NULL,
	[GL52018] [numeric](28, 8) NOT NULL,
	[GL52019] [numeric](28, 8) NOT NULL,
	[GL52020] [numeric](28, 8) NOT NULL,
	[GL52021] [numeric](28, 8) NOT NULL,
	[GL52022] [numeric](28, 8) NOT NULL,
	[GL52023] [numeric](28, 8) NOT NULL,
	[GL52024] [numeric](28, 8) NOT NULL,
	[GL52025] [numeric](28, 8) NOT NULL,
	[GL52026] [numeric](28, 8) NOT NULL,
	[GL52027] [numeric](28, 8) NOT NULL,
	[GL52028] [numeric](28, 8) NOT NULL,
	[GL52029] [numeric](28, 8) NOT NULL,
	[GL52030] [numeric](28, 8) NOT NULL,
	[GL52031] [numeric](28, 8) NOT NULL,
	[GL52032] [numeric](28, 8) NOT NULL,
	[GL52033] [numeric](28, 8) NOT NULL,
	[GL52034] [numeric](28, 8) NOT NULL,
	[GL52035] [numeric](28, 8) NOT NULL,
	[GL52036] [numeric](28, 8) NOT NULL,
	[GL52037] [numeric](28, 8) NOT NULL,
	[GL52038] [numeric](28, 8) NOT NULL,
	[GL52039] [numeric](28, 8) NOT NULL,
	[GL52040] [numeric](28, 8) NOT NULL,
	[GL52041] [numeric](28, 8) NOT NULL,
	[GL52042] [numeric](28, 8) NOT NULL,
	[GL52043] [nchar](1) NOT NULL,
	[GL52044] [numeric](28, 8) NOT NULL,
	[GL52045] [numeric](28, 8) NOT NULL,
	[GL52046] [numeric](28, 8) NOT NULL,
	[GL52047] [numeric](28, 8) NOT NULL,
	[GL52048] [numeric](28, 8) NOT NULL,
	[GL52049] [numeric](28, 8) NOT NULL,
	[GL52050] [numeric](28, 8) NOT NULL,
	[GL52051] [numeric](28, 8) NOT NULL,
	[GL52052] [numeric](28, 8) NOT NULL,
	[GL52053] [numeric](28, 8) NOT NULL,
	[GL52054] [numeric](28, 8) NOT NULL,
	[GL52055] [numeric](28, 8) NOT NULL,
	[GL52056] [numeric](28, 8) NOT NULL,
	[GL52057] [numeric](28, 8) NOT NULL,
 CONSTRAINT [Staging_GL521] PRIMARY KEY CLUSTERED 
(
	[GL52001] ASC,
	[GL52002] ASC,
	[GL52003] ASC,
	[GL52004] ASC,
	[GL52005] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staging_GL53]    Script Date: 2014/8/28 13:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_GL53](
	[GL53001] [varchar](50) NOT NULL,
	[GL53002] [varchar](50) NOT NULL,
	[GL53003] [varchar](50) NOT NULL,
	[GL53004] [varchar](50) NOT NULL,
	[GL53005] [varchar](50) NOT NULL,
	[GL53006] [varchar](50) NOT NULL,
	[GL53007] [varchar](50) NOT NULL,
	[GL53008] [numeric](28, 8) NOT NULL,
	[GL53009] [varchar](50) NOT NULL,
	[GL53010] [varchar](50) NOT NULL,
	[GL53011] [varchar](50) NOT NULL,
	[GL53012] [varchar](50) NOT NULL,
	[GL53013] [varchar](50) NOT NULL,
	[GL53014] [varchar](50) NOT NULL,
	[GL53015] [varchar](50) NOT NULL,
	[GL53016] [varchar](50) NOT NULL,
	[GL53017] [varchar](50) NOT NULL,
	[GL53018] [varchar](50) NOT NULL,
	[GL53019] [varchar](50) NOT NULL,
	[GL53020] [varchar](50) NOT NULL,
	[GL53021] [varchar](50) NOT NULL,
	[GL53022] [varchar](50) NOT NULL,
	[GL53023] [varchar](50) NOT NULL,
	[GL53024] [varchar](50) NOT NULL,
	[GL53025] [varchar](50) NOT NULL,
	[GL53026] [varchar](50) NOT NULL,
	[GL53027] [varchar](50) NOT NULL,
	[GL53028] [varchar](50) NOT NULL,
 CONSTRAINT [Staging_GL531] PRIMARY KEY CLUSTERED 
(
	[GL53001] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Configuration] ON 

INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'file', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'fileBase', N'G:\JMGroup\BI\', N'Richard', CAST(N'2014-08-04 09:50:33.473' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'cmd', N'C:\Windows\System32\cmd.exe', N'Richard', CAST(N'2014-08-04 09:50:33.473' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'cmdZipArguements', N'/c makecab  /D CompressionType=LZX /L G:\JMGroup\BI\out\ G:\JMGroup\BI\in\gl06.csv', N'Richard', CAST(N'2014-08-04 09:50:33.473' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'Export Finance CSV', N'latestId', N'0', N'BI_Staging_CreateYearlyRenewCSVforFTP', CAST(N'2014-08-04 09:50:33.473' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'companyCode', N'01', N'Richard', CAST(N'2014-08-04 10:00:52.027' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (6, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'yearCode', N'13', N'BI_Staging_CreateYearlyRenewCSVforFTP', CAST(N'2014-08-04 10:01:17.250' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (7, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpFolder', N'/shhyatt/in/', N'Richard', CAST(N'2014-08-04 10:16:11.693' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (8, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpUsername', N'shhyatt', N'Richard', CAST(N'2014-08-04 10:16:22.210' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (9, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'pwd', N'shhyatt', N'Richard', CAST(N'2014-08-04 10:16:26.607' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'SQL_GetLatestRecords', N'SELECT [GL06001]       ,[GL06003]       ,[GL06004]       ,[GL06005]       ,[GL06012]       ,[GL06013]       ,[GL06014]       ,[GL06016]       ,[GL06018] FROM [Staging_Hotel].[dbo].[Staging_GL060114] WHERE [GL06016] BETWEEN 0 AND 687  ORDER BY [GL06016] ASC', N'Richard', CAST(N'2014-08-04 11:03:24.123' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (11, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'maxId', N'0', N'Richard', CAST(N'2014-08-04 11:41:09.990' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (12, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'maxRetrievingCount', N'100000', N'Richard', CAST(N'2014-08-04 11:41:29.920' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (13, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'accountingTable', N'GL060114', N'Richard', CAST(N'2014-08-04 13:35:07.003' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (14, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'workingDirectory', N'G:\JMGroup\BI\in\', N'Richard', CAST(N'2014-08-04 13:44:41.003' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (15, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'WinSCP', N'C:\Program Files (x86)\WinSCP\WinSCP.exe', N'Richard', CAST(N'2014-08-04 14:05:31.203' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (16, N'ssis', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpArguments', N'/console /script=G:\JMGroup\BI\Template\FTPScript.txt', N'Richard', CAST(N'2014-08-04 14:06:31.637' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (17, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpWorkingDirectory', N'C:\Program Files (x86)\WinSCP', N'Richard', CAST(N'2014-08-04 14:06:45.030' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (18, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpServer', N'180.168.37.135', N'Richard', CAST(N'2014-08-04 16:14:59.957' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (19, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpPwd', N'shhyatt', N'Richard', CAST(N'2014-08-04 16:15:44.850' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (20, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpAddr', N'ftp://shhyatt:shhyatt@180.168.37.135', NULL, CAST(N'2014-08-04 17:05:27.247' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (21, N'ftp', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', N'JMGroup_CreateDailyAccountingFinanceCSVforFTP', NULL, N'ftpScript', N'# Automatically abort script on errors
option batch abort
# Disable overwrite confirmations that conflict with the previous
option confirm off
# Connect using a password
# open sftp://user:password@example.com/ -hostkey="ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx"
# Connect
# Connect as a different user
open [ftpAddr]
# Change the remote directory
cd [ftpFolder]
# Upload the file to current working directory
put [[uploadFile]
# Disconnect
close
# Exit WinSCP
exit
', NULL, CAST(N'2014-08-04 17:14:17.607' AS DateTime), NULL)
INSERT [dbo].[Configuration] ([id], [type], [job], [package], [task], [name], [value], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (22, NULL, NULL, NULL, NULL, N'isInitMode', N'0', NULL, CAST(N'2014-08-21 16:56:46.703' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Configuration] OFF
