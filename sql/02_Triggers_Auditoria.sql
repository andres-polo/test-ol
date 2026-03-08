-- =============================================
-- Reto 02: Triggers de Auditoría
-- Actualiza FechaActualizacion y UsuarioModifica en INSERT/UPDATE
-- =============================================

USE ComercioDb;
GO

-- =============================================
-- Trigger: Comerciantes
-- =============================================
IF OBJECT_ID('dbo.trg_Comerciantes_Auditoria', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Comerciantes_Auditoria;
GO

CREATE TRIGGER dbo.trg_Comerciantes_Auditoria
ON dbo.Comerciantes
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE c
    SET
        FechaActualizacion = GETUTCDATE(),
        UsuarioModifica = ISNULL(CAST(SESSION_CONTEXT(N'UsuarioId') AS NVARCHAR(100)), 'Sistema')
    FROM dbo.Comerciantes c
    INNER JOIN inserted i ON c.Id = i.Id;
END;
GO

-- =============================================
-- Trigger: Establecimientos
-- =============================================
IF OBJECT_ID('dbo.trg_Establecimientos_Auditoria', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Establecimientos_Auditoria;
GO

CREATE TRIGGER dbo.trg_Establecimientos_Auditoria
ON dbo.Establecimientos
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE e
    SET
        FechaActualizacion = GETUTCDATE(),
        UsuarioModifica = ISNULL(CAST(SESSION_CONTEXT(N'UsuarioId') AS NVARCHAR(100)), 'Sistema')
    FROM dbo.Establecimientos e
    INNER JOIN inserted i ON e.Id = i.Id;
END;
GO
