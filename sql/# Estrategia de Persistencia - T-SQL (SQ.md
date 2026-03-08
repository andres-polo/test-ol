# Estrategia de Persistencia - T-SQL (SQL Server)

## Modelo de Datos (Reto 01 & 02)

- **Tablas**:
  - [cite_start]`Usuarios`: Almacena `Nombre`, `Correo`, `Password` y `Rol` (Admin/Auxiliar)[cite: 34].
  - [cite_start]`Comerciantes`: Datos básicos, `Municipio`, `Fecha Registro` y `Estado`[cite: 34].
  - [cite_start]`Establecimientos`: Vinculados a un comerciante, con `Ingresos` (2 decimales) y `Num_Empleados`[cite: 35].
- [cite_start]**Identificadores**: Todos los IDs deben ser de tipo `IDENTITY`[cite: 41].
- [cite_start]**Auditoría Automática**: Implementar Triggers para actualizar `Fecha_Actualizacion` y `Usuario_Modifica` en cada `INSERT` o `UPDATE`[cite: 42].

## Lógica de Negocio en BD (Reto 04)

- **Procedimiento Almacenado**: `sp_ReporteComerciantesActivos`.
  - [cite_start]Debe calcular: Cantidad de establecimientos, Total Ingresos y Total Empleados por cada comerciante activo[cite: 56, 57].
  - [cite_start]Ordenamiento: Descendente por cantidad de establecimientos[cite: 58].

## Datos Semilla (Reto 03)

- [cite_start]Generar scripts para: 2 Usuarios, 5 Comerciantes y 10 Establecimientos con distribución aleatoria[cite: 46, 47, 48, 50].
