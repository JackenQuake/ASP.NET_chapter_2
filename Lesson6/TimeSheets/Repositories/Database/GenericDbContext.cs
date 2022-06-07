using Microsoft.EntityFrameworkCore;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// Контекст базы данных
	/// </summary>
	public class GenericDbContext<DB> : DbContext where DB : GenericStorage
	{
		public DbSet<DB> Base { get; set; }
		private readonly string ConnectionString;

		public GenericDbContext(string ConnectionString)
		{
			this.ConnectionString = ConnectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DB>();
		}
	}
}
