
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/25/2020 21:19:25
-- Generated from EDMX file: C:\Users\pesha\source\repos\ExpenseM\ExpenseM\Entities\ExpenseMDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ExpenseMDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [PhoneNo] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [UserType] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [CreatedAt] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TransactionTyoe] nvarchar(max)  NOT NULL,
    [Amount] nvarchar(max)  NOT NULL,
    [RecurrentStatus] nvarchar(max)  NOT NULL,
    [StartDate] nvarchar(max)  NOT NULL,
    [EndDate] nvarchar(max)  NOT NULL,
    [CreatedAt] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_UserTransaction]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTransaction'
CREATE INDEX [IX_FK_UserTransaction]
ON [dbo].[Transactions]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------