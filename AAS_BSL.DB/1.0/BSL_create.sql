CREATE TABLE [BSL_Company](
    CompanyId INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(155) NOT NULL,
    OrganizationId VARCHAR(155) NOT NULL,
    CreatedDate DATETIME NOT NULL,git remote add origin https://github.com/TomBellCentegra/AAS_BSL.git
    IsSubscribed BIT DEFAULT 0 NOT NULL,
    SecretId INT NOT NULL FOREIGN KEY REFERENCES [BSL_Secret] (SecretId),
GO

CREATE TABLE [BSL_Secret](
    SecretId  INT NOT NULL PRIMARY KEY IDENTITY,
    [SharedKey] VARCHAR(155) NOT NULL,
    [SecretKey] VARCHAR(155) NOT NULL,
)