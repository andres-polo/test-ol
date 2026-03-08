# Arquitectura Backend - .NET 8 (Clean Architecture)

## Estructura de Capas (AgentSkills Pattern)

1. **Core / Domain**: Entidades puras, interfaces de repositorios y reglas de validación de negocio.
2. [cite_start]**Application**: Casos de uso (Services), DTOs, Mappers y lógica de orquestación. Los controladores inyectan servicios de aplicación, nunca repositorios[cite: 71].
3. [cite_start]**Infrastructure**: Implementación de Repositorios con Entity Framework, persistencia y servicios externos (Generador CSV)[cite: 67, 107].
4. [cite_start]**Presentation (Web API)**: Controladores delgados que solo delegan en servicios de aplicación, Swagger y configuración de JWT[cite: 70, 105].

## Funcionalidades Críticas

- [cite_start]**Seguridad (Reto 05)**: Autenticación JWT con expiración de 1 hora y autorización basada en Roles[cite: 105, 106].
- **CRUD Comerciante (Reto 07)**:
  - [cite_start]Consultas paginadas (5 registros por defecto) con filtros por nombre, fecha y estado[cite: 124, 129].
  - [cite_start]Operación PATCH para cambio de estado rápido[cite: 130].
  - [cite_start]Restricción: El borrado es exclusivo para el rol Administrador[cite: 128].
- [cite_start]**Servicio de Reportes (Reto 08)**: Exportación a `.csv` usando el separador Pipe `|`, consumiendo la lógica del Reto 4[cite: 137, 140, 141].

## Calidad de Software

- [cite_start]**Principios**: Aplicación estricta de SOLID[cite: 71].
- [cite_start]**Testing**: Pruebas unitarias con xUnit/NUnit para validar lógica de negocio[cite: 69, 109].
- [cite_start]**Docker**: Inclusión de Dockerfile para orquestación[cite: 72].
