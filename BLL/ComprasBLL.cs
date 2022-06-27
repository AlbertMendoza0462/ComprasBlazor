using Microsoft.EntityFrameworkCore;
using ComprasBlazor.DAL;
using ComprasBlazor.Models;

namespace ComprasBlazor.BLL
{
    public class ComprasBLL
    {
        private Contexto _contexto;
        public ComprasBLL(Contexto contexto)
        {
            _contexto = contexto; 
        }

        public bool Existe(int compraID)
        {
            return _contexto.Compras.Any(compra => compra.CompraId == compraID);
        }

        public bool Guardar(Compras compra)
        {
            bool success = false;
            if (!Existe(compra.CompraId))
            {
                success = Insertar(compra);
            }
            else
            {
                success = Modificar(compra);
            }
            return success;
        }

        public bool Insertar(Compras compra)
        {
            foreach (var item in compra.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);

                producto.Existencia += item.Cantidad;

            }
            _contexto.Compras.Add(compra);
            return (_contexto.SaveChanges() > 0);
        }

        public bool Modificar(Compras compra)
        {
            var dbTmp = Buscar(compra.CompraId);

            foreach (var item in dbTmp.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);

                producto.Existencia -= item.Cantidad;

            }

            foreach (var item in compra.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);

                producto.Existencia += item.Cantidad;

            }

            var lista = new List<ComprasDetalle>(compra.Detalle);

            foreach (var item in compra.Detalle)
            {
                item.CompraDetalleId = 0;

            }

            _contexto.Entry(compra).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return (_contexto.SaveChanges() > 0);
        }

        public bool Eliminar(Compras compra)
        {
            compra = Buscar(compra.CompraId);

            foreach (var item in compra.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);

                producto.Existencia -= item.Cantidad;

            }
            _contexto.Entry(compra).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return (_contexto.SaveChanges() > 0);
        }

        public Compras? Buscar(int compraId)
        {
            return _contexto.Compras
                .Include(c => c.Detalle)
                .Where(c => c.CompraId == compraId)
                .SingleOrDefault();
        }

        public List<Compras> BuscarPorFecha(DateTime desde, DateTime hasta)
        {
            var compras = _contexto.Compras
                .Where(c => desde.Date <= c.Fecha.Date && hasta.Date >= c.Fecha.Date)
                .AsNoTracking().ToList();

            return compras;
        }

        public List<Compras> GetList()
        {
           return _contexto.Compras.AsNoTracking().ToList();
        }
    }
}
