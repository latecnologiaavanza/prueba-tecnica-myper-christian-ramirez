CREATE DATABASE TrabajadoresPrueba;
    
USE TrabajadoresPrueba;

IF OBJECT_ID('dbo.Trabajadores', 'U') IS NOT NULL
    DROP TABLE dbo.Trabajadores;
GO

CREATE TABLE Trabajadores
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL, 
    TipoDocumento NVARCHAR(50) NOT NULL,
    NumeroDocumento NVARCHAR(50) NOT NULL,
    Sexo NVARCHAR(20) NOT NULL,
    FechaNacimiento DATE NULL,
    Direccion NVARCHAR(200) NOT NULL, 
    FotoPath NVARCHAR(300) NULL
);
GO

IF OBJECT_ID('dbo.sp_ListarTrabajadores', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ListarTrabajadores;
GO

CREATE PROCEDURE dbo.sp_ListarTrabajadores
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        ISNULL(Nombres, '') AS Nombres,
        ISNULL(Apellidos, '') AS Apellidos,
        ISNULL(TipoDocumento, '') AS TipoDocumento,
        ISNULL(NumeroDocumento, '') AS NumeroDocumento,
        ISNULL(Sexo, '') AS Sexo,
        FechaNacimiento,
        ISNULL(Direccion, '') AS Direccion,
        FotoPath
    FROM dbo.Trabajadores;
END;

EXEC dbo.sp_ListarTrabajadores;