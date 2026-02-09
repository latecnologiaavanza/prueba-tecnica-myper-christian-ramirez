using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Moq;
using PruebaTecnicaMyper.Models;
using PruebaTecnicaMyper.Repositories;
using PruebaTecnicaMyper.Services;

namespace PruebaTecnicaMyper.Tests.Services
{
	public class TrabajadorServiceTests
	{


		private readonly Mock<ITrabajadorRepository> _repoMock;
		private readonly Mock<IWebHostEnvironment> _envMock;
		private readonly TrabajadorService _service;

		public TrabajadorServiceTests()
		{
			_repoMock = new Mock<ITrabajadorRepository>();
			_envMock = new Mock<IWebHostEnvironment>();
			_service = new TrabajadorService(_repoMock.Object, _envMock.Object);
		}

		[Fact]
		public void Registrar_DeberiaLanzarExcepcion_CuandoDniYaExiste()
		{
			var dniDuplicado = "12345678";
			var listaExistente = new List<Trabajador> {
				new Trabajador { Id = 1, NumeroDocumento = dniDuplicado }
			};
			_repoMock.Setup(r => r.listar()).Returns(listaExistente);

			var nuevoTrabajador = new Trabajador { NumeroDocumento = dniDuplicado };

			var ex = Assert.Throws<InvalidOperationException>(() =>
				_service.registrar(nuevoTrabajador, null));

			Assert.Equal("El número de documento ya está registrado", ex.Message);
		}

		[Fact]
		public void Registrar_DeberiaAsignarFotoDefault_CuandoNoSeEnviaFoto()
		{
			_repoMock.Setup(r => r.listar()).Returns(new List<Trabajador>());
			var trabajador = new Trabajador { NumeroDocumento = "99999999" };

			_service.registrar(trabajador, null);

			Assert.Equal("/fotos/default.png", trabajador.FotoPath);
			_repoMock.Verify(r => r.crear(It.IsAny<Trabajador>()), Times.Once);
		}


		[Fact]
		public void Actualizar_DeberiaPermitirGuardar_CuandoElDniEsDelPropioTrabajador()
		{
			int idActual = 5;
			string dniActual = "11223344";

			var trabajadorEnDb = new Trabajador { Id = idActual, NumeroDocumento = dniActual };

			_repoMock.Setup(r => r.obtener(idActual)).Returns(trabajadorEnDb);
			_repoMock.Setup(r => r.listar()).Returns(new List<Trabajador> { trabajadorEnDb });

			var trabajadorEditado = new Trabajador
			{
				Id = idActual,
				NumeroDocumento = dniActual,
				Nombres = "Juan Editado"
			};

			var excepcion = Record.Exception(() => _service.actualizar(trabajadorEditado, null));

			Assert.Null(excepcion);
			_repoMock.Verify(r => r.guardar(), Times.Once);
		}

		[Fact]
		public void Actualizar_DeberiaLanzarExcepcion_CuandoDniYaLoTieneOtroUsuario()
		{
			int miId = 10;
			int otroId = 20;
			string dniDeOtro = "88888888";

			var yo = new Trabajador { Id = miId, NumeroDocumento = "11111111" };
			var elOtro = new Trabajador { Id = otroId, NumeroDocumento = dniDeOtro };

			_repoMock.Setup(r => r.obtener(miId)).Returns(yo);
			_repoMock.Setup(r => r.listar()).Returns(new List<Trabajador> { yo, elOtro });

			var misCambios = new Trabajador { Id = miId, NumeroDocumento = dniDeOtro };

			var ex = Assert.Throws<InvalidOperationException>(() =>
				_service.actualizar(misCambios, null));

			Assert.Contains("ya está registrado", ex.Message);
		}


	}
}
