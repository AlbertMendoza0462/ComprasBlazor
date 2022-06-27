//using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ComprasBlazor.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "Digite el nombre del producto.")]
        public String? Descripcion { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "El costo debe ser mayor que cero.")]
        public double Costo { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public double Precio { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Seleccione la categoria.")]
        public int CategoriaId { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "La existencia debe ser mayor que cero.")]
        public double Existencia { get; set; }
    }
}

