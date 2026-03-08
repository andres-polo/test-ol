# Prueba Integral Fullstack (T-SQL, .NET, Angular)

## Reglas Generales

- **Tiempo máximo de entrega:** 1 día.
- **Repositorio público** (GitHub, GitLab, Bitbucket, etc.) accesible para la calificación.
- Enviar la ruta del repositorio al correo indicado.
- Los scripts SQL deben estar numerados en orden de ejecución.
- Se debe incluir un compilado de producción del frontend (Angular) comprimido.

---

## Contexto

Se requiere construir una aplicación que permita centralizar la información de comerciantes y sus establecimientos, con el objetivo de analizar el mercado y apoyar procesos operativos de una agremiación nacional de comercio.

---

# Parte 1: T-SQL (SQL Server)

## Modelo de Datos

Crear un modelo normalizado (si es necesario) que incluya las siguientes entidades:

### Usuario
- Nombre
- Correo Electrónico
- Contraseña
- Rol (Administrador o Auxiliar de Registro)

### Comerciante
- Nombre o razón social
- Municipio
- Teléfono (opcional)
- Correo Electrónico (opcional)
- Fecha de Registro
- Estado (Activo o Inactivo)
- **Auditoría:** Fecha de actualización, Usuario

### Establecimiento
- Nombre del Establecimiento
- Ingresos (decimal con 2 decimales)
- Número de Empleados
- Comerciante Dueño (FK)
- **Auditoría:** Fecha de actualización, Usuario

**Notas:**
- El identificador de cada tabla debe ser **IDENTITY**.
- Los campos de auditoría deben actualizarse mediante **TRIGGERS** en cada inserción o actualización.

---

## Reto 02: Identificadores y Auditoría

- Definir claves primarias como `IDENTITY`.
- Crear triggers `AFTER INSERT, UPDATE` que asignen:
  - `FechaActualizacion = GETDATE()`
  - `Usuario = SYSTEM_USER` o el usuario de la aplicación (según contexto).

---

## Reto 03: Datos Semilla

Generar datos aleatorios:

- **Usuarios:** 2 registros (uno por cada rol).
- **Comerciantes:** 5 registros.
- **Establecimientos:** 10 registros (distribuidos aleatoriamente entre los comerciantes).

---

## Reto 04: Reporte de Comerciantes Activos

Crear un procedimiento almacenado o función que retorne:

- Nombre o razón social
- Municipio
- Teléfono
- Correo Electrónico
- Fecha de Registro
- Estado
- **Cantidad de Establecimientos** (calculado)
- **Total Ingresos** (suma de ingresos de sus establecimientos)
- **Cantidad de Empleados** (suma de empleados de sus establecimientos)

Ordenar de forma **descendente** por **Cantidad de Establecimientos**.

---

# Parte 2: .NET (C#) Web API

## Tecnologías y Prácticas

- .NET 8 o superior
- Entity Framework (ORM)
- Clean Architecture + principios SOLID
- Autenticación JWT (1 hora de expiración)
- Autorización por roles (Administrador / Auxiliar)
- Pruebas unitarias con xUnit o NUnit (mínimo una)
- Swagger o colección Postman
- Dockerfile para despliegue contenerizado
- Política CORS configurada

---

## Reto 05: Autenticación (Login)

- Endpoint público: `POST /api/auth/login`
- Body: `{ "correo": "", "contraseña": "" }`
- Responde con un **JWT** (incluye rol y expiración en 1 hora).

---

## Reto 06: Lista de Municipios (con Caching Opcional)

- Endpoint privado (requiere JWT): `GET /api/municipios`
- Retorna lista de valores para el campo Municipio (puede venir de BD o memoria).
- Opcional: Implementar **caching en memoria** para evitar viajes a BD.

---

## Reto 07: CRUD de Comerciante

Endpoints privados (requieren JWT) con autorización por rol.

| Operación | Método | Ruta | Descripción |
|-----------|--------|------|-------------|
| Paginada | GET | `/api/comerciantes?page=1&pageSize=5&nombre=&fechaRegistro=&estado=` | Listado paginado (por defecto 5 registros). Filtros opcionales. |
| Por Id | GET | `/api/comerciantes/{id}` | Obtiene un comerciante por ID. |
| Crear | POST | `/api/comerciantes` | Crea uno nuevo. Auditoría automática. |
| Actualizar | PUT | `/api/comerciantes/{id}` | Actualiza. Auditoría automática. |
| Eliminar | DELETE | `/api/comerciantes/{id}` | Solo **Administrador**. |
| Cambiar Estado | PATCH | `/api/comerciantes/{id}/estado` | Activa/Inactiva. |

**Campos para crear/actualizar:**
- Nombre o razón social (obligatorio)
- Municipio (obligatorio)
- Teléfono (opcional)
- Correo Electrónico (opcional, con validación de formato)
- Fecha de Registro (obligatorio)
- Estado (obligatorio)

**Notas:**
- Los campos de auditoría (`FechaActualizacion`, `Usuario`) se asignan automáticamente (no se reciben en el body).
- Validaciones de tipos, obligatoriedad y formato.
- Respuestas HTTP estandarizadas.

---

## Reto 08: Reporte CSV de Comerciantes Activos

- Endpoint privado (solo **Administrador**): `GET /api/reportes/comerciantes-activos`
- Genera un archivo plano `.csv` con separador **pipe (`|`)**.
- Columnas: Nombre, Municipio, Teléfono, Correo, FechaRegistro, Estado, CantidadEstablecimientos, TotalIngresos, CantidadEmpleados.
- Los campos calculados deben obtenerse reutilizando la lógica del **Reto 4** (función/procedimiento).

---

# Parte 3: Angular (Frontend)

## Tecnologías y Prácticas

- Angular 16 o superior
- Estado global (a elección: NgRx, Akita, Services con Signals, etc.)
- Pruebas unitarias con **Jest** (mínimo una)
- Seguridad OWASP (protección contra XSS, uso de HttpOnly cookies o almacenamiento seguro del token)
- Diseño basado en prototipos (ver imágenes en documento original, se deben respetar layouts)

---

## Reto 09: Página de Login

- Campos:
  - Correo Electrónico
  - Contraseña
  - Checkbox "Acepto términos y condiciones" (obligatorio para habilitar botón)
- Al enviar, consumir endpoint del **Reto 5**.
- El token JWT debe almacenarse de forma segura (por ejemplo, en memoria con refresh token o HttpOnly cookies si se implementa backend).
- Encabezado tras login: muestra **Nombre del Usuario** y **Rol**.
- Diseño debe seguir el prototipo (fondo libre).

---

## Reto 10: Página Home (Listado de Comerciantes)

- Tabla con columnas:
  - Nombre o Razón Social
  - Teléfono
  - Correo Electrónico
  - Fecha Registro
  - Cantidad de Establecimientos
  - Estado
  - Acciones (editar, activar/inactivar, eliminar)
- Paginador con opciones: 5, 10, 15 registros por página.
- Botones:
  - **Editar:** redirige a formulario (Reto 11).
  - **Activar/Inactivar:** consume endpoint PATCH del Reto 7.
  - **Eliminar:** visible solo para Administrador, consume DELETE del Reto 7.
  - **Descargar CSV:** visible solo para Administrador, consume endpoint del Reto 8.
  - **Crear Nuevo:** redirige a formulario de creación.
  - **Cerrar sesión** (ubicación libre).
- Consumir endpoints del **Reto 7** y **Reto 8** según corresponda.
- Diseño basado en prototipo.

---

## Reto 11: Página Formulario (Creación/Actualización de Comerciante)

- Campos del formulario:
  - Nombre o Razón Social (input)
  - Municipio o Ciudad (selector - consume endpoint del Reto 6)
  - Teléfono (input opcional)
  - Correo Electrónico (input opcional, validación email)
  - Fecha de Registro (date picker)
  - Estado (selector: Activo/Inactivo)
  - Checkbox "¿Posee establecimientos?" (solo informativo)
- Validaciones visuales en cada campo (tipo, longitud, obligatoriedad).
- Al **editar**, en el footer se debe mostrar:
  - Sumatoria de ingresos de todos los establecimientos del comerciante.
  - Sumatoria de cantidad de empleados.
  - Estos datos se cargan desde la base de datos (data semilla del Reto 3), ya que no hay APIs para establecimientos.
- Consumir endpoints del **Reto 7** para crear/actualizar.
- Diseño basado en prototipo.

---

## Entregables Adicionales

- Scripts SQL numerados (ej: `01_create_tables.sql`, `02_triggers.sql`, `03_seed_data.sql`, `04_stored_procedures.sql`).
- Dockerfile para backend.
- Colección de Postman o Swagger UI funcionando.
- Build de producción del frontend comprimido.
