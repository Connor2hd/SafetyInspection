CREATE TABLE Inpsection (
ID int IDENTITY(1,1) PRIMARY KEY,
technicianID int FOREIGN KEY REFERENCES Technician(ID),
labID int FOREIGN KEY REFERENCES Lab(ID),
assignedDate Date,
canStartDate Date,
dueDate Date,
startDate Date,
finishDate Date,
isComplete bit
)

CREATE TABLE Technician(
ID int IDENTITY(1,1) PRIMARY KEY,
firstName varchar(20),
lastName varchar(20),
loginPass varchar(20),
email varchar(20)
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