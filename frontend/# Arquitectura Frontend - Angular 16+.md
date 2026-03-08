# Arquitectura Frontend - Angular 16+

## Gestión de Estado y Seguridad

- [cite_start]**Estado Global**: Implementar un Store (como NGXS o Signals) para manejar la sesión y datos compartidos[cite: 152].
- [cite_start]**Seguridad OWASP**: Asegurar que las credenciales no sean visibles en el debug y manejar JWT de forma segura[cite: 154, 191].

## Componentes y Vistas

- [cite_start]**Login (Reto 09)**: Formulario con validaciones, checkbox de términos y visualización de perfil en el header tras el éxito[cite: 185, 189, 190].
- **Home (Reto 10)**:
  - [cite_start]Tabla dinámica con paginación configurable (5, 10, 15)[cite: 209, 210].
  - [cite_start]Acciones condicionadas: El botón "Eliminar" y "Descargar CSV" solo aparecen para Administradores[cite: 214, 216].
- **Formulario (Reto 11)**:
  - [cite_start]Formulario reactivo para Crear/Editar[cite: 292].
  - [cite_start]Selector de municipios alimentado por el API (con caché opcional)[cite: 112, 113, 294].
  - [cite_start]Footer informativo: Sumatoria de ingresos y empleados al editar un comerciante[cite: 300].

## Calidad y Entrega

- [cite_start]**Validaciones**: Feedback visual mediante Toasts o mensajes de error en inputs[cite: 304, 305].
- [cite_start]**Unit Test**: Pruebas con Jest para componentes críticos[cite: 153].
- [cite_start]**Build**: Generar versión comprimida para producción[cite: 170].
