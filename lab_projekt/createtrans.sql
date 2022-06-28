CREATE DATABASE trans;
GO
Use trans
Go
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Login] [varchar](64) NOT NULL,
	[Password] [varchar](18) NOT NULL,
	[Admin] [bit] NOT NULL)

 CREATE TABLE [dbo].[Repairs](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[TruckId] [int] NOT NULL,
	[Name] [varchar](12) NOT NULL,
	[Date] [datetime] NOT NULL)
CREATE TABLE [dbo].[Drivers](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Firstname] [varchar](64) NOT NULL,
	[Lastname] [varchar](64) NOT NULL,
	[Phone] [varchar](12) NOT NULL)
CREATE TABLE [dbo].[Trucks](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[DriverId] [int] NOT NULL,
	[Plate] [varchar](64) NOT NULL,
	[Brand] [varchar](64) NOT NULL,
	[Model] [varchar](64) NOT NULL,	
	[VIN] [varchar](64) NOT NULL,	
	[Insurance] [datetime] NOT NULL,
	[TechReview] [datetime] NOT NULL,
	[TachoLeg] [datetime] NOT NULL)

	GO
	ALTER TABLE [dbo].[Trucks]
	ADD CONSTRAINT FK_Trucks_Drivers FOREIGN KEY (DriverId)
	REFERENCES [dbo].[Drivers] (Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE;

	GO
	ALTER TABLE [dbo].[Repairs]
	ADD CONSTRAINT FK_Repairs_Trucks FOREIGN KEY (TruckId)
	REFERENCES [dbo].[Trucks] (Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE;

	GO

INSERT INTO dbo.Users(login, password, admin) 
VALUES ('admin', 'admin', 1)  ,('nieadmin', 'admin', 0)

INSERT INTO dbo.Drivers(Firstname, Lastname, Phone) 
VALUES ('Daniel', 'Trucker', '+4800011556'), ('Pawel', 'Mann', '+48993945729'), ('Dave', 'Trucker', '+44112233455')

INSERT INTO dbo.Trucks(DriverId, Plate, Brand, Model,VIN, Insurance, TechReview, TachoLeg) 
VALUES (1, 'KR05EX', 'DAF', 'XC100','WF0EXXGCDE9B59994', '2022-07-19', '2022-07-19', '2022-07-19'), (2, 'LU09EX', 'MAN', '12.205','WF0EXXGCDE9B59994', '2022-07-19', '2022.06.01', '2022-07-19'), (3, 'DW11EX', 'IVECO', 'PC100','WF0EXXGCDE9B59994', '2022-07-19', '2023.06.06', '2022-07-19')

Use trans
Go
