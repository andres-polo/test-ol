# Comercio API - Backend .NET 8

## Requisitos

- .NET 8 SDK
- SQL Server (local, Docker o Azure)

## Configuración

1. **Ejecutar scripts SQL** (en orden):

   ```bash
   # Desde la raíz del proyecto test-ol
   sqlcmd -S localhost -d master -i sql/01_DDL_Tablas.sql
   sqlcmd -S localhost -d ComercioDb -i sql/02_Triggers_Auditoria.sql
   sqlcmd -S localhost -d ComercioDb -i sql/03_Datos_Semilla.sql
   sqlcmd -S localhost -d ComercioDb -i sql/04_SP_ReporteComerciantesActivos.sql
   ```

   O usar SQL Server Management Studio / Azure Data Studio ejecutando los 4 archivos en orden.

2. **Cadena de conexión** en `appsettings.json`:
   - Por defecto: `Server=localhost;Database=ComercioDb;Trusted_Connection=True;TrustServerCertificate=True`

3. **Usuarios de prueba** (después de ejecutar datos semilla):
   - Admin: `admin@comercio.com` / `Admin123!`
   - Auxiliar: `auxiliar@comercio.com` / `Auxiliar123!`

## Ejecutar

```bash
dotnet run --project src/ComercioApi.Web
```

Swagger: https://localhost:7xxx/swagger (o el puerto que indique la consola)

## Docker

### Levantar / iniciar Docker

1. **Iniciar Docker Desktop** (macOS/Windows):
   - Abre la aplicación **Docker Desktop** desde el menú o búsqueda
   - Espera a que el icono de Docker indique que está listo

2. **Verificar que Docker está corriendo**:

   ```bash
   docker info
   ```

   Si responde con la información del sistema, Docker está activo.

### Opción A: Todo con docker-compose (recomendado)

Desde la raíz del proyecto `test-ol`:

```bash
# Construir imagen de la API (si no existe) y levantar API + SQL Server
docker compose up -d

# Ejecutar scripts sql/ contra localhost:1433 (sa / StrongPassword123!)
```

La API usa `Server=sqlserver` internamente (red Docker). Swagger: http://localhost:8080/swagger

### Opción B: API con docker run (SQL Server ya corriendo en Docker)

Si SQL Server ya está en Docker (ej. `docker compose up -d` solo sqlserver), la API debe usar `host.docker.internal` para alcanzar el host:

```bash
# Desde la carpeta backend/
docker build -t comercio-api .
docker run -p 8080:8080 -e ConnectionStrings__DefaultConnection="Server=host.docker.internal;Database=ComercioDb;User Id=sa;Password=StrongPassword123!;TrustServerCertificate=True" comercio-api
```

**Nota:** `localhost` en la connection string no funciona desde dentro del contenedor; la API necesita `host.docker.internal` o estar en la misma red que SQL (Opción A).

## Pruebas unitarias

```bash
dotnet test
```

No requiere SQL Server ni API en ejecución. Los tests usan xUnit y validan lógica de aplicación (validadores FluentValidation).

| Comando | Descripción |
|---------|-------------|
| `dotnet test` | Ejecuta todos los tests |
| `dotnet test --verbosity normal` | Salida detallada por test |
| `dotnet test --filter "FullyQualifiedName~LoginRequest"` | Solo tests del validador de Login |
