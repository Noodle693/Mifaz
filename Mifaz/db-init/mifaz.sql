CREATE DATABASE `Mifaz`;

USE `Mifaz`;

CREATE TABLE `Users`
(
    `Id`       INT          NOT NULL AUTO_INCREMENT,
    `Username` varchar(50)  NOT NULL,
    `Password` varchar(150) NOT NULL,
    `Phone`    varchar(30)  NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Rides`
(
    `Id`              INT         NOT NULL AUTO_INCREMENT,
    `DriverId`        INT         NOT NULL,
    `Price`           DOUBLE      NOT NULL,
    `StartingDate`    DATE        NOT NULL,
    `EndingDate`      DATE        NOT NULL,
    `StartingCity`    varchar(50) NOT NULL,
    `DestinationCity` varchar(50) NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`DriverId`) REFERENCES `Users` (`Id`)
);

CREATE TABLE `RideUserAssociations`
(
    `Id`          INT NOT NULL AUTO_INCREMENT,
    `RideId`      INT NOT NULL,
    `PassengerId` INT NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`RideId`) REFERENCES `Rides` (`Id`),
    FOREIGN KEY (`PassengerId`) REFERENCES `Users` (`Id`)
);