CREATE DATABASE TrabajadoresPrueba;

USE TrabajadoresPrueba;
GO

CREATE TABLE dbo.Trabajador (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    TipoDocumento NVARCHAR(50) NOT NULL,
    NumeroDocumento NVARCHAR(50) NOT NULL,
    Sexo CHAR(1) NOT NULL, -- 'M' o 'F'
    FechaNacimiento DATE NULL,
    Foto VARBINARY(MAX) NULL, -- almacenar imagen en binario (opcional)
    Direccion NVARCHAR(250) NULL,
    FechaCreacion DATETIME2 DEFAULT SYSUTCDATETIME()
);
CREATE INDEX IX_Trabajador_NumeroDocumento ON dbo.Trabajador(NumeroDocumento);
GO

GO
CREATE PROCEDURE dbo.usp_ListarTrabajadores
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Nombres,
        Apellidos,
        TipoDocumento,
        NumeroDocumento,
        Sexo,
        FechaNacimiento,
        Direccion
        -- omitimos la columna Foto para listado; recuperarla por separado si hace falta
    FROM dbo.Trabajador
    ORDER BY Apellidos, Nombres;
END
GO



select*from dbo.Trabajador

INSERT INTO dbo.Trabajador (Nombres, Apellidos, TipoDocumento, NumeroDocumento, Sexo, FechaNacimiento, Direccion)
VALUES
('Juan', 'Pérez', 'DNI', '12345678', 'M', '1990-05-10', 'Av. Siempre Viva 123'),
('María', 'Gómez', 'DNI', '87654321', 'F', '1987-11-20', 'Calle Falsa 456');
GO