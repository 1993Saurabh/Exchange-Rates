
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/30/2017 11:46:01
-- Generated from EDMX file: G:\CurrencyConverter\CurrencyConverter\Exchange.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CurrencyExchange];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ExchangeRates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExchangeRates];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ExchangeRates'
CREATE TABLE [dbo].[ExchangeRates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CurrencyCode] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [UpdatedOn] datetime  NOT NULL,
    [Rate] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ExchangeRates'
ALTER TABLE [dbo].[ExchangeRates]
ADD CONSTRAINT [PK_ExchangeRates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------