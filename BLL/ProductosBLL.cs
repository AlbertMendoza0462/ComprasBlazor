using Microsoft.EntityFrameworkCore;
using ComprasBlazor.DAL;
using ComprasBlazor.Models;

namespace ComprasBlazor.BLL
{
    public class ProductosBLL
    {
        private Contexto _contexto;
        public ProductosBLL(Contexto contexto)
        {
            _contexto = contexto; 
        }
       
        public Productos Buscar(int productoId)
        {
            var producto = _contexto.Productos
                .Where( p => p.ProductoId == productoId)
                .SingleOrDefault();

            return producto;
        }

        public List<Productos> GetList()
        {
           return _contexto.Productos.AsNoTracking().ToList();
        }
    }
}
