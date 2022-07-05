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
            return !Existe(compra.CompraId) ? Insertar(compra) : Modificar(compra);
        }

        public bool Insertar(Compras compra)
        {
            _contexto.Compras.Add(compra);

            foreach (var item in compra.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);
                producto.Existencia += item.Cantidad;
            }

            bool paso = _contexto.SaveChanges() > 0;

            _contexto.Entry(compra).State = EntityState.Detached;

            return paso;
        }

        public bool Modificar(Compras compra)
        {
            var anterior = _contexto.Compras
               .Where(c => c.CompraId == compra.CompraId)
               .Include(c => c.Detalle)
               .AsNoTracking()
               .SingleOrDefault();

            foreach (var item in anterior.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);
                producto.Existencia -= item.Cantidad;
            }
            _contexto.Entry(anterior).State = EntityState.Detached;

            anterior = null;

            _contexto.Database.ExecuteSqlRaw($"DELETE FROM ComprasDetalle WHERE CompraId={compra.CompraId};");

            foreach (var item in compra.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);
                producto.Existencia += item.Cantidad;

                _contexto.Entry(item).State = EntityState.Added;
            }

            _contexto.Entry(compra).State = EntityState.Modified;

            var guardo = _contexto.SaveChanges() > 0;

            _contexto.Entry(compra).State = EntityState.Detached;

            return guardo;
        }

        public bool Eliminar(Compras compra)
        {
            _contexto.Entry(compra).State = EntityState.Deleted;

            foreach (var item in compra.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);
                producto.Existencia -= item.Cantidad;
            }

            _contexto.Database.ExecuteSqlRaw($"DELETE FROM ComprasDetalle WHERE CompraId={compra.CompraId};");

            bool paso = _contexto.SaveChanges() > 0;

            _contexto.Entry(compra).State = EntityState.Detached;

            return paso;
        }

        public Compras? Buscar(int compraId)
        {
            Compras? compra = _contexto.Compras
                .Include(c => c.Detalle)
                .Where(c => c.CompraId == compraId)
                .AsNoTracking()
                .SingleOrDefault();


            return compra;
        }

        public List<Compras> GetListByDate(DateTime desde, DateTime hasta)
        {
            var compras = _contexto.Compras
                .Where(c => desde.Date <= c.Fecha.Date && hasta.Date >= c.Fecha.Date)
                .AsNoTracking()
                .ToList();

            return compras;
        }

        public List<Compras> GetList()
        {
           List<Compras> compras = _contexto.Compras
                .AsNoTracking()
                .ToList();

            return compras;
        }
    }
}
