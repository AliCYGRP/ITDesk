use ITDesk;
CREATE TABLE [EmployeeInfo](
[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
[EmployeeName] [varchar](255) NOT NULL,
[EmployeeEmail] [varchar](255) NOT NULL,
[Designation] [varchar](255) NOT NULL,
[Password] [varchar](max) NOT NULL DEFAULT '2c9c31108265d77886569d52e0a1f883',
[Role] [bit] NOT NULL DEFAULT 0 
PRIMARY KEY CLUSTERED
(
[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SELECT * FROM EmployeeInfo;
INSERT into EmployeeInfo(EmployeeName,EmployeeEmail,Designation) values('Ali','syed.hasan@cygrp.com','Intern');

CREATE TABLE [DeviceCategory](
[CategoryId] [int] IDENTITY(1,1) NOT NULL,
[DeviceType] [varchar](255) NOT NULL
PRIMARY KEY CLUSTERED
(
[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [DeviceInfo](
[DeviceId] [int] IDENTITY(1,1) NOT NULL,
[UniqueCode] [varchar](255) NOT NULL UNIQUE,
[DeviceName] [varchar](255) NOT NULL,
[CategoryId] [int] NOT NULL REFERENCES DeviceCategory(CategoryId) ,
[EmployeeId] [int]  REFERENCES EmployeeInfo(EmployeeId) DEFAULT 0,
[AssignedDate] [DATE] NOT NULL,
[IsAssigned] [bit] Default 0,
[AssignedBy] [varchar](255) NOT NULL,
[QrCode] [varchar](max)
PRIMARY KEY CLUSTERED
(
[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SELECT * froM DeviceInfo;


CREATE TABLE [AuditTrail](
[AuditId] [int] IDENTITY(1,1) NOT NULL,
[UniqueCode] [varchar](255) NOT NULL,
[EmployeeEmail] [varchar](255) NOT NULL,
[Date] [Date] NOT NULL
PRIMARY KEY CLUSTERED
(
[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SELECT * FROM AuditTrail;