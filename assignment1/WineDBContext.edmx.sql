
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/30/2015 17:26:51
-- Generated from EDMX file: E:\AC#\cis237assignment5\assignment1\WineDBContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BeverageACullen];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Beverages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Beverages];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Beverages'
CREATE TABLE [dbo].[Beverages] (
    [id] nvarchar(10)  NOT NULL,
    [name] nchar(100)  NOT NULL,
    [pack] nchar(20)  NOT NULL,
    [price] decimal(19,4)  NOT NULL,
    [active] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Beverages'
ALTER TABLE [dbo].[Beverages]
ADD CONSTRAINT [PK_Beverages]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------