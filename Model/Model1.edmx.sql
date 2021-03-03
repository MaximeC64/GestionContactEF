
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/11/2019 07:03:12
-- Generated from EDMX file: C:\Users\Nadine\source\repos\GestionContactsEF\Model\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Contacts];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Client_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons_Client] DROP CONSTRAINT [FK_Client_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Brother_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons_Brother] DROP CONSTRAINT [FK_Brother_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Persons_Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons_Client];
GO
IF OBJECT_ID(N'[dbo].[Persons_Brother]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons_Brother];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(80)  NOT NULL,
    [Prenom] nvarchar(80)  NOT NULL,
    [Adresse] nvarchar(80)  NULL,
    [Ville] nvarchar(80)  NULL,
    [Codepostal] nvarchar(24)  NULL,
    [Email] nvarchar(80)  NULL,
    [Telephone] nvarchar(24)  NULL
);
GO

-- Creating table 'Persons_Client'
CREATE TABLE [dbo].[Persons_Client] (
    [RefClient] nvarchar(24)  NULL,
    [Societe] nvarchar(80)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Persons_Brother'
CREATE TABLE [dbo].[Persons_Brother] (
    [Poste] nvarchar(24)  NULL,
    [Fonction] nvarchar(80)  NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Persons_Client'
ALTER TABLE [dbo].[Persons_Client]
ADD CONSTRAINT [PK_Persons_Client]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Persons_Brother'
ALTER TABLE [dbo].[Persons_Brother]
ADD CONSTRAINT [PK_Persons_Brother]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'Persons_Client'
ALTER TABLE [dbo].[Persons_Client]
ADD CONSTRAINT [FK_Client_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Persons_Brother'
ALTER TABLE [dbo].[Persons_Brother]
ADD CONSTRAINT [FK_Brother_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------