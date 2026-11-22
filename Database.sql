IF DB_ID(N'LoginDB') IS NULL
    CREATE DATABASE LoginDB;
GO

USE LoginDB;
GO

IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash VARBINARY(32) NOT NULL,
        PasswordSalt VARBINARY(16) NOT NULL
    );
END;
GO

SELECT UserID, Username, PasswordHash, PasswordSalt
FROM dbo.Users;
