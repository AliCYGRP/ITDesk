CREATE DATABASE ITDesk

USE [ITDesk]
GO

/****** Object: Table [dbo].[StudentInfo] Script Date: 04/21/2021 12:31:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeInfo](
[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
[EmployeeName] [varchar](255) NOT NULL,
[EmployeeEmail] [varchar](255) NOT NULL,
[Designation] [varchar](255) NOT NULL,
[Password] [varchar](max) NOT NULL DEFAULT '2c9c31108265d77886569d52e0a1f883',
[Role][bit] NOT NULL,

PRIMARY KEY CLUSTERED
(
[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SELECT * FROM EmployeeInfo

CREATE TABLE [dbo].[DeviceInfo](
[DeviceId] [int] IDENTITY(1,1) NOT NULL,
[UniqueCode] [varchar](255) NOT NULL UNIQUE,
[DeviceName] [varchar](255) NOT NULL,
[DeviceType] [varchar](255) NOT NULL,
[EmployeeID][int] References EmployeeInfo(EmployeeId)DEFAULT 0,
[AssignedDate][Date] NOT NULL,
[IsAssigned][bit]Default 0,
[AssignedBy][varchar](255)NOT NULL,
[QrCode] [varchar](max),


PRIMARY KEY CLUSTERED
(
[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SELECT * from DeviceInfo;


CREATE TABLE [dbo].[AuditTrail](
[AuditId] [int] IDENTITY(1,1) NOT NULL,
[UniqueCode] [varchar](255) NOT NULL ,
[EmployeeEmail] [varchar](255) NOT NULL,
[Date][Date]NOT NULL

PRIMARY KEY CLUSTERED
(
[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SELECT * FROM AuditTrail;
