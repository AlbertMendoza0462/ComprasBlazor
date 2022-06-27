using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComprasBlazor.Models
{
    public class ComprasDetalle
    {
        [Key]
        public int CompraDetalleId { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Un detalle debe pertenecer a una compra.")]
        public int CompraId { get; set; }
        public int ProductoId { get; set; }

        [NotMapped, Required(ErrorMessage = "Seleccione el producto.")]
        public string Descripcion { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Digite la cantidad del producto.")]
        public double Cantidad { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "El costo debe ser mayor que cero.")]
        public double Costo { get; set; }


        [NotMapped]
        public double Importe { 
            get { return Cantidad * Costo; }
        }
        
        public override string? ToString()
        {
            return $"Detalle # {CompraDetalleId}, ProductoId= {ProductoId} , cantidad={Cantidad} ";
        }
    }
}

