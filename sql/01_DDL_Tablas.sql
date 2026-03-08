-- =============================================
-- Reto 01: Modelo de Datos - Prueba Integral Lite
-- SQL Server - DDL Tablas
-- Ejecutar en orden: 01, 02, 03, 04
-- =============================================

USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ComercioDb')
    CREATE DATABASE ComercioDb;
GO

USE ComercioDb;
GO

-- =============================================
-- Tabla: Usuarios
-- =============================================
IF OBJECT_ID('dbo.Usuarios', 'U') IS NOT NULL
    DROP TABLE dbo.Usuarios;
GO

CREATE TABLE dbo.Usuarios (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(500) NOT NULL,
    Rol NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Usuarios PRIMARY KEY (Id),
    CONSTRAINT UQ_Usuarios_Correo UNIQUE (Correo),
    CONSTRAINT CK_Usuarios_Rol CHECK (Rol IN ('Administrador', 'Auxiliar de Registro'))
);
GO

CREATE UNIQUE INDEX IX_Usuarios_Correo ON dbo.Usuarios(Correo);
GO

-- =============================================
-- Tabla: Municipios (Lista de Valores para Comerciantes)
-- =============================================
IF OBJECT_ID('dbo.Municipios', 'U') IS NOT NULL
    DROP TABLE dbo.Municipios;
GO

CREATE TABLE dbo.Municipios (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Municipios PRIMARY KEY (Id),
    CONSTRAINT UQ_Municipios_Nombre UNIQUE (Nombre)
);
GO

-- =============================================
-- Tabla: Comerciantes
-- =============================================
IF OBJECT_ID('dbo.Establecimientos', 'U') IS NOT NULL
    DROP TABLE dbo.Establecimientos;
GO

IF OBJECT_ID('dbo.Comerciantes', 'U') IS NOT NULL
    DROP TABLE dbo.Comerciantes;
GO

CREATE TABLE dbo.Comerciantes (
    Id INT IDENTITY(1,1) NOT NULL,
    NombreRazonSocial NVARCHAR(200) NOT NULL,
    MunicipioId INT NOT NULL,
    Telefono NVARCHAR(20) NULL,
    Correo NVARCHAR(255) NULL,
    FechaRegistro DATE NOT NULL,
    Estado NVARCHAR(20) NOT NULL,
    FechaActualizacion DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UsuarioModifica NVARCHAR(100) NOT NULL DEFAULT 'Sistema',
    CONSTRAINT PK_Comerciantes PRIMARY KEY (Id),
    CONSTRAINT FK_Comerciantes_Municipio FOREIGN KEY (MunicipioId) REFERENCES dbo.Municipios(Id),
    CONSTRAINT CK_Comerciantes_Estado CHECK (Estado IN ('Activo', 'Inactivo'))
);
GO

CREATE INDEX IX_Comerciantes_MunicipioId ON dbo.Comerciantes(MunicipioId);
CREATE INDEX IX_Comerciantes_Estado ON dbo.Comerciantes(Estado);
CREATE INDEX IX_Comerciantes_FechaRegistro ON dbo.Comerciantes(FechaRegistro);
CREATE INDEX IX_Comerciantes_NombreRazonSocial ON dbo.Comerciantes(NombreRazonSocial);
GO

-- =============================================
-- Tabla: Establecimientos
-- =============================================
CREATE TABLE dbo.Establecimientos (
    Id INT IDENTITY(1,1) NOT NULL,
    NombreEstablecimiento NVARCHAR(200) NOT NULL,
    Ingresos DECIMAL(18,2) NOT NULL,
    NumeroEmpleados INT NOT NULL,
    ComercianteId INT NOT NULL,
    FechaActualizacion DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UsuarioModifica NVARCHAR(100) NOT NULL DEFAULT 'Sistema',
    CONSTRAINT PK_Establecimientos PRIMARY KEY (Id),
    CONSTRAINT FK_Establecimientos_Comerciante FOREIGN KEY (ComercianteId) REFERENCES dbo.Comerciantes(Id) ON DELETE CASCADE,
    CONSTRAINT CK_Establecimientos_Ingresos CHECK (Ingresos >= 0),
    CONSTRAINT CK_Establecimientos_NumeroEmpleados CHECK (NumeroEmpleados >= 0)
);
GO

CREATE INDEX IX_Establecimientos_ComercianteId ON dbo.Establecimientos(ComercianteId);
GO
