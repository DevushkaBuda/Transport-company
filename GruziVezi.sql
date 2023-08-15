create database GruziVezi
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/23/2023 18:29:12
-- Generated from EDMX file: C:\Users\ÿ\Desktop\CourseProjectLogistics-master\GruziVezi\GruziVeziModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Cars_ModelCars]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cars] DROP CONSTRAINT [FK_Cars_ModelCars];
GO
IF OBJECT_ID(N'[dbo].[FK_Drivers_Cars]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drivers] DROP CONSTRAINT [FK_Drivers_Cars];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Drivers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Drivers];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Routes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Routes];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Senders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Senders];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Status];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Routes_CitiesEnd]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Routes] DROP CONSTRAINT [FK_Routes_CitiesEnd];
GO
IF OBJECT_ID(N'[dbo].[FK_Routes_CitiesStart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Routes] DROP CONSTRAINT [FK_Routes_CitiesStart];
GO
IF OBJECT_ID(N'[dbo].[FK_Senders_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Senders] DROP CONSTRAINT [FK_Senders_Company];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cars];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Company];
GO
IF OBJECT_ID(N'[dbo].[Drivers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Drivers];
GO
IF OBJECT_ID(N'[dbo].[ModelCars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModelCars];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Routes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Routes];
GO
IF OBJECT_ID(N'[dbo].[Senders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Senders];
GO
IF OBJECT_ID(N'[dbo].[Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cars'
CREATE TABLE [dbo].[Cars] (
    [id] int IDENTITY(1,1) NOT NULL,
    [number] nvarchar(10)  NOT NULL,
    [id_ModelCar] int  NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Company'
CREATE TABLE [dbo].[Company] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Drivers'
CREATE TABLE [dbo].[Drivers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [surname] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [middlename] nvarchar(50)  NULL,
    [id_Car] int  NULL
);
GO

-- Creating table 'ModelCars'
CREATE TABLE [dbo].[ModelCars] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_Sender] int  NOT NULL,
    [id_Driver] int  NOT NULL,
    [id_Route] int  NOT NULL,
    [description] nvarchar(300)  NULL,
    [id_Status] int  NOT NULL,
    [id_User] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Routes'
CREATE TABLE [dbo].[Routes] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_CityStart] int  NOT NULL,
    [adressStart] nvarchar(50)  NOT NULL,
    [id_CityEnd] int  NOT NULL,
    [adressEnd] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Senders'
CREATE TABLE [dbo].[Senders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [surname] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [middlename] nvarchar(50)  NULL,
    [id_Company] int  NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [surname] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [middlename] nvarchar(50)  NOT NULL,
    [id_Role] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Cars'
ALTER TABLE [dbo].[Cars]
ADD CONSTRAINT [PK_Cars]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Company'
ALTER TABLE [dbo].[Company]
ADD CONSTRAINT [PK_Company]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [PK_Drivers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ModelCars'
ALTER TABLE [dbo].[ModelCars]
ADD CONSTRAINT [PK_ModelCars]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Routes'
ALTER TABLE [dbo].[Routes]
ADD CONSTRAINT [PK_Routes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Senders'
ALTER TABLE [dbo].[Senders]
ADD CONSTRAINT [PK_Senders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_ModelCar] in table 'Cars'
ALTER TABLE [dbo].[Cars]
ADD CONSTRAINT [FK_Cars_ModelCars]
    FOREIGN KEY ([id_ModelCar])
    REFERENCES [dbo].[ModelCars]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cars_ModelCars'
CREATE INDEX [IX_FK_Cars_ModelCars]
ON [dbo].[Cars]
    ([id_ModelCar]);
GO

-- Creating foreign key on [id_Car] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [FK_Drivers_Cars]
    FOREIGN KEY ([id_Car])
    REFERENCES [dbo].[Cars]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Drivers_Cars'
CREATE INDEX [IX_FK_Drivers_Cars]
ON [dbo].[Drivers]
    ([id_Car]);
GO

-- Creating foreign key on [id_CityEnd] in table 'Routes'
ALTER TABLE [dbo].[Routes]
ADD CONSTRAINT [FK_Routes_CitiesEnd]
    FOREIGN KEY ([id_CityEnd])
    REFERENCES [dbo].[Cities]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Routes_CitiesEnd'
CREATE INDEX [IX_FK_Routes_CitiesEnd]
ON [dbo].[Routes]
    ([id_CityEnd]);
GO

-- Creating foreign key on [id_CityStart] in table 'Routes'
ALTER TABLE [dbo].[Routes]
ADD CONSTRAINT [FK_Routes_CitiesStart]
    FOREIGN KEY ([id_CityStart])
    REFERENCES [dbo].[Cities]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Routes_CitiesStart'
CREATE INDEX [IX_FK_Routes_CitiesStart]
ON [dbo].[Routes]
    ([id_CityStart]);
GO

-- Creating foreign key on [id_Company] in table 'Senders'
ALTER TABLE [dbo].[Senders]
ADD CONSTRAINT [FK_Senders_Company]
    FOREIGN KEY ([id_Company])
    REFERENCES [dbo].[Company]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Senders_Company'
CREATE INDEX [IX_FK_Senders_Company]
ON [dbo].[Senders]
    ([id_Company]);
GO

-- Creating foreign key on [id_Driver] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Drivers]
    FOREIGN KEY ([id_Driver])
    REFERENCES [dbo].[Drivers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Drivers'
CREATE INDEX [IX_FK_Orders_Drivers]
ON [dbo].[Orders]
    ([id_Driver]);
GO

-- Creating foreign key on [id_Route] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Routes]
    FOREIGN KEY ([id_Route])
    REFERENCES [dbo].[Routes]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Routes'
CREATE INDEX [IX_FK_Orders_Routes]
ON [dbo].[Orders]
    ([id_Route]);
GO

-- Creating foreign key on [id_Sender] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Senders]
    FOREIGN KEY ([id_Sender])
    REFERENCES [dbo].[Senders]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Senders'
CREATE INDEX [IX_FK_Orders_Senders]
ON [dbo].[Orders]
    ([id_Sender]);
GO

-- Creating foreign key on [id_Status] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Status]
    FOREIGN KEY ([id_Status])
    REFERENCES [dbo].[Status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Status'
CREATE INDEX [IX_FK_Orders_Status]
ON [dbo].[Orders]
    ([id_Status]);
GO

-- Creating foreign key on [id_User] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Users]
    FOREIGN KEY ([id_User])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Users'
CREATE INDEX [IX_FK_Orders_Users]
ON [dbo].[Orders]
    ([id_User]);
GO

-- Creating foreign key on [id_Role] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([id_Role])
    REFERENCES [dbo].[Roles]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([id_Role]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------