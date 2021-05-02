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
[CategoryId] [int] NOT NULL REFERENCES DeviceCategory(CategoryId) DEFAULT 0,
[AssignedDate] [DATE],
[EmployeeId] [int] REFERENCES EmployeeInfo(EmployeeId),
[IsAssigned] [bit] Default 0,
[AssignedBy] [varchar](255),
[QrCode] [varchar](max)
PRIMARY KEY CLUSTERED
(
[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

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

SELECT * FROM EmployeeInfo;
INSERT into EmployeeInfo(EmployeeName,EmployeeEmail,Designation) values('Ali','syed.hasan@cygrp.com','Intern');
INSERT into EmployeeInfo(EmployeeName,EmployeeEmail,Designation) values('Gurnoor','gurnoor.singh@cygrp.com','Intern');
INSERT into EmployeeInfo(EmployeeName,EmployeeEmail,Designation) values('Saksham','saksham.grover@cygrp.com','Intern');
INSERT into EmployeeInfo(EmployeeName,EmployeeEmail,Designation,Role) values('Abhinav','abhinav.mishra@cygrp.com','IT',1);


SELECT * FROM DeviceCategory;
INSERT into DeviceCategory values('Mobile');
INSERT into DeviceCategory values('Laptop');
INSERT into DeviceCategory values('Pad');

SELECT * FROM DeviceInfo;
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, EmployeeId, AssignedDate,IsAssigned,AssignedBy)
values('IMEI397689237','DellXZ7',1,3,'2021-03-15',1,'Aman');
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, IsAssigned)
values('Latitude550','DellX432',1,0);

INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, EmployeeId, AssignedDate,IsAssigned,AssignedBy)
values('IMEI397689238','DellXZ7',2,2,'2021-03-15',1,'Aman');
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, EmployeeId, AssignedDate,IsAssigned,AssignedBy)
values('IMEI397689239','DellXY7',2,1,'2021-03-15',1,'Aman');
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, EmployeeId, AssignedDate,IsAssigned,AssignedBy)
values('IMEI3979435','iphone XS max',1,3,'2021-03-15',1,'Sam');
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, EmployeeId, AssignedDate,IsAssigned,AssignedBy)
values('IMEI397689435','iphone XS',1,2,'2021-03-15',1,'Sam');
 

INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, EmployeeId, AssignedDate,IsAssigned,AssignedBy)
values('IMEI3976435','NOKIA 1100',1,1,'2021-04-15',1,'Sam');
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, IsAssigned)
values('IME3767','DellX432',2,0);
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, IsAssigned)
values('IME3765','DellX432',2,0);
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, IsAssigned)
values('IME7828','Samsung note 8',1,0);
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, IsAssigned)
values('IME5635','Ipad Air',3,0);
 
INSERT into DeviceInfo(UniqueCode, DeviceName, CategoryId, IsAssigned)
values('IME5442','Ipad Air Max',3,0);

select * from deviceinfo;
SELECT * FROM AuditTrail;


