CREATE DATABASE `Mifaz` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE `Mifaz`;

CREATE TABLE `Users`
(
    `Id`        INT          NOT NULL AUTO_INCREMENT,
    `Mail`      varchar(50)  NOT NULL,
    `Password`  varchar(150) NOT NULL,
    `FirstName` varchar(30)  NOT NULL,
    `LastName`  varchar(30)  NOT NULL,
    `Phone`     varchar(30)  NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Rides`
(
    `Id`          INT         NOT NULL AUTO_INCREMENT,
    `DriverId`    INT         NOT NULL,
    `Origin`      varchar(50) NOT NULL,
    `Destination` varchar(50) NOT NULL,
    `Date`        DATETIME    NOT NULL,
    `MaxCount`    INT         NOT NULL,
    `Cost`        DOUBLE      NOT NULL,
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

CREATE TABLE `SearchHistory`
(
    `Id`     INT         NOT NULL AUTO_INCREMENT,
    `UserId` INT         NOT NULL,
    `Search` varchar(50) NOT NULL,
    `Date`   DATE        NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`)
);

INSERT INTO `Users`(`Mail`, `Password`, `FirstName`, `LastName`, `Phone`)
VALUES ('admin@admin.com', 'admin', 'Melissa', 'Stahl', '0153859315');
INSERT INTO `Users`(`Mail`, `Password`, `FirstName`, `LastName`, `Phone`)
VALUES ('hans.hinterfinkel@gmail.com', 'passwort123', 'Hansi', 'Hinterfinkel', '05318590531');
INSERT INTO `Users`(`Mail`, `Password`, `FirstName`, `LastName`, `Phone`)
VALUES ('harald.schmidt@t-online.com', 'geheim01', 'Harald', 'Schmidt', '04531053019');
INSERT INTO `Users`(`Mail`, `Password`, `FirstName`, `LastName`, `Phone`)
VALUES ('Malte.Sowi@test.com', 'soGeheimMan', 'Malte', 'Sowi', '5930153154');

INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (1, 'Hof', 'Köln', '2023-04-02 12:00:00', 2, 15);
INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (1, 'Leipzig', 'Hamburg', '2023-04-16 09:45:00', 3, 20);
INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (1, 'Bayreuth', 'München', '2023-05-01 20:30:00', 4, 12);
INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (1, 'Hof', 'Bayreuth', '2023-04-01 11:20:00', 4, 5);

INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (2, 'Helmbrechts', 'Bayreuth', '2023-04-01 13:20:00', 3, 3);
INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (2, 'Helmbrechts', 'Leipzig', '2023-04-05 16:25:00', 4, 10);
INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (2, 'Helmbrechts', 'Berlin', '2023-04-06 13:55:00', 4, 15);

INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (3, 'Bayreuth', 'Bamberg', '2023-05-06 11:30:00', 4, 7);

INSERT INTO `Rides`(`DriverId`, `Origin`, `Destination`, `Date`, `MaxCount`, `Cost`)
VALUES (4, 'Bayreuth', 'Nürnberg', '2023-04-26 10:00:00', 4, 5);

INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (1, 3);
INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (1, 2);

INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (2, 4);
INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (2, 3);

INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (6, 1);
INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (6, 4);

INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (8, 1);
INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (8, 2);
INSERT INTO `RideUserAssociations`(`RideId`, `PassengerId`)
VALUES (8, 4);