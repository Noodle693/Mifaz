CREATE DATABASE `Mifaz`;

USE `Mifaz`;

CREATE TABLE `Users`
(
    Id       INT NOT NULL AUTO_INCREMENT,
    Username varchar(50),
    Password varchar(150),
    PRIMARY KEY (Id)
);