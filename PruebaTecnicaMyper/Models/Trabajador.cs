using System;
using System.ComponentModel.DataAnnotations;
using PruebaTecnicaMyper.Attributes;

namespace PruebaTecnicaMyper.Models
{
	public class Trabajador
	{

		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio")]
		public string Nombres { get; set; } = string.Empty;

		[Required(ErrorMessage = "El apellido es obligatorio")]
		public string Apellidos { get; set; } = string.Empty;

		[Display(Name = "Tipo de Documento")]
		[Required(ErrorMessage = "Tipo documento obligatorio")]
		public string TipoDocumento { get; set; } = string.Empty;

		[Display(Name = "Número de Documento")]
		[Required(ErrorMessage = "Numero documento obligatorio")]
		[MaxLength(8, ErrorMessage = "Máximo 8 caracteres")]
		public string NumeroDocumento { get; set; } = string.Empty;

		[Required(ErrorMessage = "El sexo es obligatorio")]
		public string Sexo { get; set; } = string.Empty;

		[Display(Name = "Fecha de Nacimiento")]
		[Required(ErrorMessage = "Fecha de nacimiento obligatoria")]
		[MayorEdad(ErrorMessage = "Debe ser mayor de 18 años")]
		public DateTime? FechaNacimiento { get; set; }

		[Required(ErrorMessage = "Dirección obligatoria")]
		public string Direccion { get; set; } = string.Empty;

		public string? FotoPath { get; set; }


	}
}
