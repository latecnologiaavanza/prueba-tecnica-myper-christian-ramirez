using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMyper.Data;
using PruebaTecnicaMyper.Models;

namespace PruebaTecnicaMyper.Repositories
{
    public class TrabajadorRepository : ITrabajadorRepository
    {

        private readonly ApplicationDbContext _context;

        public TrabajadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void crear(Trabajador trabajador)
        {
            _context.Trabajadores.Add(trabajador);
            _context.SaveChanges();
        }

        public void eliminar(int id)
        {
            var trabajador = _context.Trabajadores.Find(id);

            if(trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
                _context.SaveChanges();
            }
        }

        public List<Trabajador> listar()
        {
            return _context.Trabajadores
                        .FromSqlRaw("EXEC sp_ListarTrabajadores")
                        .ToList();
        }

        public Trabajador obtener(int id)
        {
            return _context.Trabajadores.Find(id) ?? throw new KeyNotFoundException("Trabajador no encontrado");
        }

		public void guardar()
		{
			_context.SaveChanges();
		}

        public List<Trabajador> listarPorSexo(string sexo)
        {
            return _context.Trabajadores
                .Where(t => t.Sexo == sexo)
                .ToList();
        }
    }
}
