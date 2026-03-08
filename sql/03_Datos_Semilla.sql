-- =============================================
-- Reto 03: Datos Semilla
-- 2 Usuarios, 5 Comerciantes, 10 Establecimientos
-- Distribución aleatoria de establecimientos por comerciante
-- =============================================

USE ComercioDb;
GO

-- =============================================
-- Municipios (requeridos para Comerciantes)
-- =============================================
INSERT INTO dbo.Municipios (Nombre) VALUES
    (N'Bogotá'),
    (N'Medellín'),
    (N'Cali'),
    (N'Barranquilla'),
    (N'Cartagena'),
    (N'Bucaramanga'),
    (N'Pereira'),
    (N'Manizales'),
    (N'Villavicencio'),
    (N'Ibagué');
GO

-- =============================================
-- Usuarios: 2 registros (Administrador, Auxiliar de Registro)
-- Admin: admin@comercio.com / Admin123!
-- Auxiliar: auxiliar@comercio.com / Auxiliar123!
-- =============================================
INSERT INTO dbo.Usuarios (Nombre, Correo, PasswordHash, Rol) VALUES
    (N'Admin Sistema', 'admin@comercio.com', '$2y$10$KdQPM39v11GCPcO7ycWL/.z1F29hxLpoO9n74GBcsYWP6Bhkuc9fW', 'Administrador'),
    (N'Auxiliar Registro', 'auxiliar@comercio.com', '$2y$10$cVxJkg7PitsBB4sfJs0c/uN4Jw1nGPq7sXLxm3nQvWfBYvXjnIkDy', 'Auxiliar de Registro');
GO

-- =============================================
-- Comerciantes: 5 registros
-- =============================================
INSERT INTO dbo.Comerciantes (NombreRazonSocial, MunicipioId, Telefono, Correo, FechaRegistro, Estado) VALUES
    (N'Supermercados La Economía S.A.S', 1, '3001234567', 'contacto@laeconomia.com', '2023-01-15', 'Activo'),
    (N'Ferretería Don José', 2, '3102345678', NULL, '2023-03-22', 'Activo'),
    (N'Panadería La Esquina', 3, NULL, 'pedidos@laesquina.com', '2023-05-10', 'Inactivo'),
    (N'Tech Solutions Colombia Ltda', 1, '3203456789', 'ventas@techsolutions.co', '2023-07-08', 'Activo'),
    (N'Distribuidora El Sur', 5, '3154567890', 'info@elsur.com.co', '2023-09-30', 'Activo');
GO

-- =============================================
-- Establecimientos: 10 registros (distribución aleatoria por comerciante)
-- Comerciante 1: 3, Comerciante 2: 2, Comerciante 3: 1, Comerciante 4: 2, Comerciante 5: 2
-- =============================================
INSERT INTO dbo.Establecimientos (NombreEstablecimiento, Ingresos, NumeroEmpleados, ComercianteId) VALUES
    (N'Sucursal Norte', 15000000.50, 25, 1),
    (N'Sucursal Centro', 22000000.00, 40, 1),
    (N'Sucursal Sur', 18500000.75, 30, 1),
    (N'Ferretería Principal', 8500000.25, 8, 2),
    (N'Ferretería Express', 4200000.50, 4, 2),
    (N'Panadería Central', 3500000.00, 6, 3),
    (N'Oficina Bogotá', 45000000.00, 120, 4),
    (N'Oficina Medellín', 32000000.25, 85, 4),
    (N'Almacén Principal', 12000000.00, 15, 5),
    (N'Punto de Venta Costa', 6800000.75, 10, 5);
GO
