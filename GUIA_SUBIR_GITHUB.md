# Guía para subir el proyecto a GitHub

Proyecto con SQL (Fase 1) y .NET (Fase 2) listos.

---

## Paso 1: Crear el repositorio en GitHub

1. Entra a [github.com](https://github.com) e inicia sesión.
2. Haz clic en **New repository** (o el botón **+** → New repository).
3. Configura:
   - **Repository name**: `test-ol` (o el nombre que prefieras)
   - **Description**: `test-ol- T-SQL, .NET 8, Angular`
   - **Visibility**: Public
   - **No** marcar "Add a README file", "Add .gitignore" ni "Add license" (ya existen localmente)
4. Haz clic en **Create repository**.

---

## Paso 2: Inicializar Git e iniciar seguimiento

Abre una terminal en la carpeta del proyecto (`test-ol`) y ejecuta:

```bash
cd /Users/andresfelipepoloortega/Projects/test-ol

# Inicializar repositorio
git init

# Configurar rama principal
git branch -M main

# Ver qué archivos se van a subir
git status
```

---

## Paso 3: Añadir archivos y hacer el primer commit

```bash
# Añadir todo lo que no esté en .gitignore
git add .

# Revisar qué se va a subir
git status

# Primer commit
git commit -m "feat: Fase 1 SQL + Fase 2 .NET (Clean Architecture, JWT, CRUD, reporte CSV)"
```

---

## Paso 4: Conectar con GitHub y subir

En GitHub, en la página del nuevo repositorio verás una URL tipo:
`https://github.com/TU_USUARIO/prueba-integral-lite.git`

```bash
# Añadir el remoto (reemplaza TU_USUARIO y NOMBRE_REPO con tus datos)
git remote add origin https://github.com/TU_USUARIO/prueba-integral-lite.git

# Subir al remoto
git push -u origin main
```

Si GitHub te pide autenticación:

- **HTTPS**: usa tu usuario y un [Personal Access Token](https://github.com/settings/tokens) en lugar de la contraseña.
- **SSH**: usa una URL tipo `git@github.com:TU_USUARIO/prueba-integral-lite.git` si tienes llave SSH configurada.

---

## Paso 5: Comprobar en GitHub

Entra a la URL de tu repositorio y revisa que se vean:

- `sql/` — Scripts 01 a 04
- `backend/` — Solución .NET
- `ComercioApi.postman_collection.json`
- `README.md`
- `.gitignore`

El archivo `Prueba Integral Lite (TSQL-.NET-Angular) 2.docx` **no** debe aparecer (está en `.gitignore`).

---

## Comandos para cambios posteriores

```bash
git add .
git status
git commit -m "descripción del cambio"
git push
```
