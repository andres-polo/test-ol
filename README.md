# Prueba Integral Lite - Comercio

Aplicación fullstack: T-SQL, .NET 8, Angular 16+.

Ver [GUIA_SUBIR_GITHUB.md](GUIA_SUBIR_GITHUB.md) para subir el proyecto a GitHub.

## Estructura

```
test-ol/
├── sql/                    # Scripts T-SQL (ejecutar 01 a 04 en orden)
├── backend/                # API .NET 8 (Clean Architecture)
└── frontend/               # Angular (Fase 3)
```

## Validar Fase 1 + 2 (sin Frontend)

1. **SQL Server**: usar local o Docker:
   ```bash
   docker compose up -d   # Levanta SQL Server en localhost:1433 (sa / YourStrong@Passw0rd)
   ```

2. **Crear base de datos**: ejecutar los 4 scripts en `sql/` contra SQL Server (SSMS, Azure Data Studio o sqlcmd).

3. **ConnectionString** (si usas Docker): `Server=localhost;Database=ComercioDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True`

4. **Iniciar API**: `cd backend && dotnet run --project src/ComercioApi.Web`

5. **Probar con Swagger o Postman**: abrir `/swagger` o importar `ComercioApi.postman_collection.json`

6. **Login**: POST `/api/auth/login` con `{ "correo": "admin@comercio.com", "contrasena": "Admin123!" }` — la colección Postman guarda el token automáticamente

7. **Usar token**: en Swagger, Authorize con `Bearer <token>`

8. **Endpoints**: GET municipios, CRUD comerciantes, reporte CSV (solo Admin).

## Pruebas unitarias (Backend)

```bash
cd backend
dotnet test
```

No requiere SQL Server ni API levantada. Incluye tests de validadores (Login, Comerciante) con xUnit.

| Comando | Descripción |
|---------|-------------|
| `dotnet test` | Ejecuta todos los tests |
| `dotnet test --verbosity normal` | Salida más detallada |
| `dotnet test --filter "FullyQualifiedName~LoginRequest"` | Solo tests de LoginRequest |
