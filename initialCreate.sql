CREATE TABLE Inspection (
ID int IDENTITY(1,1) PRIMARY KEY,
technicianID int FOREIGN KEY REFERENCES Technician(ID),
labID int FOREIGN KEY REFERENCES Lab(ID),
assignedDate Date,
canStartDate Date,
dueDate Date,
startDate Date,
finishDate Date,
isComplete bit DEFAULT 0
)

CREATE TABLE CorrectiveActions(
ID int IDENTITY(1,1) PRIMARY KEY,
inspectionID int FOREIGN KEY REFERENCES Inspection(ID),
labID int FOREIGN KEY REFERENCES Lab(ID),
technicianID int FOREIGN KEY REFERENCES Technician(ID),
hazardID int FOREIGN KEY REFERENCES Hazard(ID),
areaID int FOREIGN KEY REFERENCES Area(ID),
detailDesc varchar(20),
actionDesc varchar(20),
dueDate Date,
isComplete bit DEFAULT 0
)

CREATE TABLE Technician(
ID int IDENTITY(1,1) PRIMARY KEY,
firstName varchar(20),
lastName varchar(20),
loginPass varchar(20),
email varchar(40)
)

CREATE TABLE LAB(
ID int IDENTITY(1,1) PRIMARY KEY,
labName varchar(30),
school varchar(20)
)

CREATE TABLE Hazard(
ID int IDENTITY(1,1) PRIMARY KEY,
hazardName varchar(20)
)

CREATE TABLE Area(
ID int IDENTITY(1,1) PRIMARY KEY,
labID int FOREIGN KEY REFERENCES Lab(ID),
areaName varchar(20),
)