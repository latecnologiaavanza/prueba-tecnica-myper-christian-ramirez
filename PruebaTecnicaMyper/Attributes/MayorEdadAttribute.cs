using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaMyper.Attributes
{
	public class MayorEdadAttribute : ValidationAttribute
	{

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is DateTime fechaNacimiento)
			{
				var edad = DateTime.Today.Year - fechaNacimiento.Year;
				if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;

				if (edad < 18)
				{
					return new ValidationResult("El trabajador debe ser mayor de 18 años.");
				}
			}
			return ValidationResult.Success;
		}


	}
}
