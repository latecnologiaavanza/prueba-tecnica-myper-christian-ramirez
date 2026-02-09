using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMyper.Models;
using PruebaTecnicaMyper.Services;

namespace PruebaTecnicaMyper.Controllers
{
	public class TrabajadorController : Controller
	{
		private readonly ITrabajadorService _service;

		public TrabajadorController(ITrabajadorService service)
		{
			_service = service;
		}

		public IActionResult Index(string sexo)
		{
			var trabajadores = string.IsNullOrEmpty(sexo)
				? _service.listar()
				: _service.listarPorSexo(sexo);

			ViewBag.SexoSeleccionado = sexo;
			return View(trabajadores);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Save([FromForm] Trabajador trabajador, IFormFile? foto)
		{
			ModelState.Remove("foto");

			if (!ModelState.IsValid) return PartialView("_Create", trabajador);

			try
			{
				if (trabajador.Id == 0)
					_service.registrar(trabajador, foto);
				else
					_service.actualizar(trabajador, foto);

				return Content("Success");
			}
			catch (InvalidOperationException ex)
			{
				ModelState.AddModelError("NumeroDocumento", ex.Message);
				return PartialView("_Create", trabajador);
			}
			catch (Exception)
			{
				return BadRequest("Error interno del servidor.");
			}
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult confirmDelete(int id)
		{
			_service.eliminar(id);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult getById(int id)
		{
			var trabajador = _service.obtener(id);
			return Json(trabajador);
		}
	}
}
