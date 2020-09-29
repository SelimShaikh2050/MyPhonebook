USE master
GO

CREATE DATABASE myPhonebookDB
GO

USE myPhonebookDB
GO


CREATE TABLE tblGender
(
	genderID INT IDENTITY PRIMARY KEY,
	gender VARCHAR(6) NOT NULL,
)
GO

CREATE TABLE tblMain
(
	id INT IDENTITY PRIMARY KEY,
	firstName VARCHAR(50) NOT NULL,
	middleName VARCHAR(20),
	lastName VARCHAR(30),
	groups VARCHAR(20),
	genderID INT REFERENCES  tblGender(genderID),
	mobileNo VARCHAR (19) NOT NULL UNIQUE,
	email VARCHAR(60),
	dateOfBirth DATE
)
GO

INSERT INTO tblGender VALUES('Male'),('Female')
GO


CREATE TRIGGER sealedtblGender
    ON tblGender
    FOR DELETE, INSERT, UPDATE
    AS
    BEGIN
     ROLLBACK TRANSACTION
    END
GO

--INSERT INTO tblMain VALUES('','','','',1,'','','')
--GO


CREATE INDEX IX_tblMain_NAME
    ON tblMain
    (firstName)
GO


select * from tblGender
select * from tblMain

SET CONCAT_NULL_YIELDS_NULL OFF
SELECT M.firstName+' '+M.middleName+' '+M.lastName AS [NAME],M.mobileNo AS CONTACT,M.email EMAIL,M.dateOfBirth DOB,G.gender GENDER ,M.groups AS [GROUP]  FROM tblMain M
JOIN tblGender G ON M.genderID=G.genderID
SET CONCAT_NULL_YIELDS_NULL ON

