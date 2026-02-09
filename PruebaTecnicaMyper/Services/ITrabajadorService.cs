using PruebaTecnicaMyper.Models;

namespace PruebaTecnicaMyper.Services
{
	public interface ITrabajadorService
	{

		List<Trabajador> listar();
		void registrar(Trabajador trabajador, IFormFile foto);
		Trabajador obtener(int id);
		void eliminar(int id);
		void actualizar(Trabajador trabajador, IFormFile foto);
		List<Trabajador> listarPorSexo(string sexo);

	}
}
