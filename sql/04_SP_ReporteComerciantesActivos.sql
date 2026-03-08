-- =============================================
-- Reto 04: Procedimiento Almacenado - Reporte Comerciantes Activos
-- Retorna: Nombre, Municipio, Teléfono, Correo, Fecha Registro, Estado,
--          Cantidad Establecimientos, Total Ingresos, Cantidad Empleados
-- Ordenado descendente por Cantidad de Establecimientos
-- =============================================

USE ComercioDb;
GO

IF OBJECT_ID('dbo.sp_ReporteComerciantesActivos', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ReporteComerciantesActivos;
GO

CREATE PROCEDURE dbo.sp_ReporteComerciantesActivos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id AS ComercianteId,
        c.NombreRazonSocial,
        m.Nombre AS Municipio,
        c.Telefono,
        c.Correo,
        c.FechaRegistro,
        c.Estado,
        ISNULL(est.CantidadEstablecimientos, 0) AS CantidadEstablecimientos,
        ISNULL(est.TotalIngresos, 0) AS TotalIngresos,
        ISNULL(est.CantidadEmpleados, 0) AS CantidadEmpleados
    FROM dbo.Comerciantes c
    INNER JOIN dbo.Municipios m ON c.MunicipioId = m.Id
    OUTER APPLY (
        SELECT
            COUNT(e.Id) AS CantidadEstablecimientos,
            SUM(e.Ingresos) AS TotalIngresos,
            SUM(e.NumeroEmpleados) AS CantidadEmpleados
        FROM dbo.Establecimientos e
        WHERE e.ComercianteId = c.Id
    ) est
    WHERE c.Estado = 'Activo'
    ORDER BY CantidadEstablecimientos DESC;
END;
GO
