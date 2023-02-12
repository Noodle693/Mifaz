CREATE DATABASE [Mifaz];
GO

USE [Mifaz];
GO

CREATE TABLE Users (
                       Id INT NOT NULL IDENTITY,
                       Username varchar(50),
                       Password varchar(50),
                       PRIMARY KEY (Id)
);
GO