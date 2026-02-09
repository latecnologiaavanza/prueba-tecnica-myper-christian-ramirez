using System.Data;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMyper.Data;
using PruebaTecnicaMyper.Models;
using PruebaTecnicaMyper.Repositories;

namespace PruebaTecnicaMyper.Services
{
	public class TrabajadorService : ITrabajadorService
	{
		private readonly ITrabajadorRepository _repo;
		private readonly IWebHostEnvironment _env;
		private const string FOTO_DEFAULT = "/fotos/default.png";

		public TrabajadorService(ITrabajadorRepository repo, IWebHostEnvironment env)
		{
			_repo = repo;
			_env = env;
		}

		public void eliminar(int id)
		{
			var trabajador = _repo.obtener(id);
			if (trabajador == null)
			{
				throw new KeyNotFoundException("Trabajador no encontrado");
			}
			_repo.eliminar(id);
		}

		public List<Trabajador> listar()
		{
			return _repo.listar();
		}

		public Trabajador obtener(int id)
		{
			return _repo.obtener(id);
		}

		public void registrar(Trabajador trabajador, IFormFile? foto)
		{
			if (_repo.listar().Any(x => x.NumeroDocumento == trabajador.NumeroDocumento))
			{
				throw new InvalidOperationException("El número de documento ya está registrado");
			}

			if (foto != null && foto.Length > 0)
			{
				string rutaFotos = Path.Combine(_env.WebRootPath, "fotos");
				Directory.CreateDirectory(rutaFotos);

				string nombre = Guid.NewGuid() + Path.GetExtension(foto.FileName);
				string ruta = Path.Combine(rutaFotos, nombre);

				using var stream = new FileStream(ruta, FileMode.Create);
				foto.CopyTo(stream);

				trabajador.FotoPath = "/fotos/" + nombre;
			}
			else
			{
				trabajador.FotoPath = FOTO_DEFAULT;
			}

			_repo.crear(trabajador);
		}

		public void actualizar(Trabajador trabajador, IFormFile? foto)
		{
			var existente = _repo.obtener(trabajador.Id);

			if (existente == null)
				throw new KeyNotFoundException("Trabajador no encontrado");

			if (_repo.listar().Any(x => x.NumeroDocumento == trabajador.NumeroDocumento && x.Id != trabajador.Id))
			{
				throw new InvalidOperationException("El número de documento ya está registrado");
			}

			existente.Nombres = trabajador.Nombres;
			existente.Apellidos = trabajador.Apellidos;
			existente.TipoDocumento = trabajador.TipoDocumento;
			existente.NumeroDocumento = trabajador.NumeroDocumento;
			existente.Sexo = trabajador.Sexo;
			existente.Direccion = trabajador.Direccion;
			existente.FechaNacimiento = trabajador.FechaNacimiento;

			if (foto != null && foto.Length > 0)
			{
				string rutaFotos = Path.Combine(_env.WebRootPath, "fotos");
				Directory.CreateDirectory(rutaFotos);

				string nombre = Guid.NewGuid() + Path.GetExtension(foto.FileName);
				string ruta = Path.Combine(rutaFotos, nombre);

				using var stream = new FileStream(ruta, FileMode.Create);
				foto.CopyTo(stream);

				existente.FotoPath = "/fotos/" + nombre;
			}
			_repo.guardar();
		}

		public List<Trabajador> listarPorSexo(string sexo)
		{
			if (string.IsNullOrEmpty(sexo))
			{
				return _repo.listar();
			}
			return _repo.listarPorSexo(sexo);
		}
	}
}
