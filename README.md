# Ferretería Valdivia - Sistema Web ASP.NET Core MVC

Sistema de gestión de inventario para ferretería desarrollado con **ASP.NET Core MVC**, **Entity Framework Core** y **MariaDB/MySQL**.

## Requisitos previos

- .NET 9.0 SDK
- MariaDB o MySQL (puerto 3306)
- Usuario `root` (o ajustar la cadena de conexión)

## Cómo ejecutar el proyecto (paso a paso)

### 1. Clonar el repositorio
```bash
git clone https://github.com/FABRIZIO7775220X/FerreteriaValdivia.29.git
cd FerreteriaValdivia.29
```

### 2. Crear la base de datos (IMPORTANTE)
Ejecuta el archivo **`base_de_datos.sql`** en MariaDB/MySQL.  
Este script crea la base `ferreteriavaldiviadb` y las tablas **correctas** que usa la aplicación (`Productos` y `Usuarios`).

```bash
# Ejemplo con el cliente mysql
mysql -u root -p < base_de_datos.sql
```

> **Nota:** Si antes usaste una versión vieja del script, este nuevo script elimina las tablas incorrectas y crea las correctas.

### 3. Revisar la cadena de conexión
Abre `appsettings.json` y verifica:
```json
"ConnectionStrings": {
  "ConexionMariaDB": "Server=localhost;Port=3306;Database=ferreteriavaldiviadb;User=root;Password=;"
}
```
Agrega tu contraseña de root si la tienes.

### 4. Restaurar y ejecutar
```bash
dotnet restore
dotnet run
```

Luego abre el navegador en la URL que indique la consola (normalmente https://localhost:7xxx).

### 5. (Opcional) Usar migraciones de EF Core
Si prefieres que EF cree/actualice la base:
```bash
dotnet ef database update
```

## Funcionalidades actuales

- Listado de productos (inventario)
- Agregar producto
- Editar producto
- Eliminar producto
- Ver el producto más vendido

## Problema solucionado (agregar producto)

El error al agregar productos se debía a:

1. El script SQL original creaba una tabla `productos` completamente diferente a la que espera Entity Framework (`Productos` con PK `CodigoProducto`).
2. El modelo tenía un mapeo de columna (`CodigoBarras`) inconsistente con las migraciones.
3. Faltaba manejo de errores y validaciones claras en el controlador.
4. La vista de creación no tenía scripts de validación del lado del cliente.

Todo esto fue corregido en los archivos:
- `Models/Producto.cs`
- `Data/AplicationDbContext.cs`
- `Controllers/ProductosController.cs`
- `Views/Productos/Crear.cshtml`
- `Views/Productos/Index.cshtml`
- `base_de_datos.sql`
- `README.md`
