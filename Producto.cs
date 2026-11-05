using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    public class Producto
    {
        [Key]
        [Required(ErrorMessage = "El código del producto es obligatorio")]
        [StringLength(50, ErrorMessage = "El código no puede superar los 50 caracteres")]
        [Display(Name = "Código de Producto")]
        public string CodigoProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio de venta es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio de Venta")]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo")]
        [Display(Name = "Stock Mínimo")]
        public int StockMinimo { get; set; }

        // Setter público necesario para el formulario y EF Core
        private int _stockActual;
        [Required(ErrorMessage = "El stock actual es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock actual no puede ser negativo")]
        [Display(Name = "Stock Actual")]
        public int StockActual
        {
            get => _stockActual;
            set => _stockActual = value >= 0 ? value : 0;
        }

        [Display(Name = "Cantidad Vendida")]
        public int CantidadVendida { get; set; } = 0;

        // Constructor vacío (obligatorio para Entity Framework Core)
        public Producto()
        {
        }

        // Constructor con parámetros
        public Producto(string codigoProducto, string nombre, decimal precioVenta, int stockMinimo, int stockActual, int cantidadVendida = 0)
        {
            CodigoProducto = codigoProducto;
            Nombre = nombre;
            PrecioVenta = precioVenta;
            StockMinimo = stockMinimo;
            StockActual = stockActual >= 0 ? stockActual : 0;
            CantidadVendida = cantidadVendida;
        }

        public bool DescontarStock(int cantidad)
        {
            if (cantidad <= 0 || _stockActual < cantidad)
                return false;

            _stockActual -= cantidad;
            CantidadVendida += cantidad;
            return true;
        }

        public bool AgregarStock(int cantidad)
        {
            if (cantidad <= 0)
                return false;

            _stockActual += cantidad;
            return true;
        }

        public bool RequiereReposicion()
        {
            return _stockActual <= StockMinimo;
        }
    }
}
