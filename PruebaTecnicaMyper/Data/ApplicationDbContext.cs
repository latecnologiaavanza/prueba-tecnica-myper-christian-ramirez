using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMyper.Models;

namespace PruebaTecnicaMyper.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Trabajador> Trabajadores
		{
			get; set;
		}

	}
}
