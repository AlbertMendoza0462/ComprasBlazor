using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ComprasBlazor.Models
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Digite el nombre de la categoria.")]
        public String? Descripcion { get; set; }
    }
}

