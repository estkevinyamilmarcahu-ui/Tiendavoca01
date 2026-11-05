-- ============================================================
-- BASE DE DATOS CORRECTA PARA FerreteriaValdivia (EF Core)
-- ============================================================
-- Este script crea el esquema que REALMENTE usa la aplicación
-- (coincide con las migraciones de Entity Framework Core).
-- ============================================================

CREATE DATABASE IF NOT EXISTS ferreteriavaldiviadb
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE ferreteriavaldiviadb;

-- Desactivar temporalmente las verificaciones de FK
SET FOREIGN_KEY_CHECKS = 0;

-- Eliminar tablas antiguas (si existían de la versión incorrecta)
DROP TABLE IF EXISTS Detalle_Venta;
DROP TABLE IF EXISTS Pago;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS productos;          -- tabla vieja (minúscula)
DROP TABLE IF EXISTS Productos;
DROP TABLE IF EXISTS Usuarios;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Empleado;
DROP TABLE IF EXISTS Proveedor;
DROP TABLE IF EXISTS Categoria;

SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================
-- Tabla Productos (la que usa el controlador y el modelo)
-- ============================================================
CREATE TABLE Productos (
    CodigoProducto  VARCHAR(50)     NOT NULL,
    Nombre          VARCHAR(150)    NOT NULL,
    PrecioVenta     DECIMAL(18,2)   NOT NULL,
    StockMinimo     INT             NOT NULL DEFAULT 0,
    StockActual     INT             NOT NULL DEFAULT 0,
    CantidadVendida INT             NOT NULL DEFAULT 0,
    PRIMARY KEY (CodigoProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================
-- Tabla Usuarios
-- ============================================================
CREATE TABLE Usuarios (
    IdUsuario       INT             NOT NULL AUTO_INCREMENT,
    NombreCompleto  LONGTEXT        NOT NULL,
    Username        LONGTEXT        NOT NULL,
    Activo          TINYINT(1)      NOT NULL DEFAULT 1,
    PRIMARY KEY (IdUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Datos de ejemplo (opcional)
INSERT INTO Productos (CodigoProducto, Nombre, PrecioVenta, StockMinimo, StockActual, CantidadVendida) VALUES
('P001', 'Martillo de acero', 45.50, 5, 20, 0),
('P002', 'Destornillador Phillips', 12.00, 10, 35, 3),
('P003', 'Cinta métrica 5m', 18.75, 8, 12, 1);

SELECT 'Base de datos creada correctamente. Ahora puedes ejecutar la aplicación.' AS Mensaje;
