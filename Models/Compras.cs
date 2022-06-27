 using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ComprasBlazor.Models
{
    public class Compras
    {
        [Key]
        public int CompraId { get; set; }
        [Required(ErrorMessage = "Digite el nombre del suplidor.")]
        public string Suplidor { get; set; }
        [Required(ErrorMessage = "Digite la fecha.")]
        public DateTime Fecha { get; set; }

        [ForeignKey("CompraId"),Required(ErrorMessage = "Una compra necesita detalles.")]
        public List<ComprasDetalle> Detalle { get; set; } = new List<ComprasDetalle>();
        [Range(1, Int32.MaxValue, ErrorMessage = "El total debe ser mayor que cero.")]
        public double Total { get; set; }

        public override string? ToString()
        {
            return $"Compra: Id={CompraId}, Fecha={Fecha}, Total={Total}";
        }
    }

}

