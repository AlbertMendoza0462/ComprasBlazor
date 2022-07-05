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

        public bool Existe(int productoID)
        {
            return _contexto.Productos.Any(producto => producto.ProductoId == productoID);
        }

        public bool Guardar(Productos producto)
        {
            bool success = false;
            if (!Existe(producto.ProductoId))
            {
                success = Insertar(producto);
            }
            else
            {
                success = Modificar(producto);
            }
            return success;
        }

        public bool Insertar(Productos producto)
        {
            _contexto.Productos.Add(producto);

            return _contexto.SaveChanges() > 0;
        }

        public bool Modificar(Productos producto)
        {
            _contexto.Entry(producto).State = EntityState.Modified;

            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(producto).State = EntityState.Detached;
            return guardo;
        }

        public bool Eliminar(Productos producto)
        {
            _contexto.Entry(producto).State = EntityState.Deleted;

            return _contexto.SaveChanges() > 0;
        }

        public Productos? Buscar(int productoId)
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
