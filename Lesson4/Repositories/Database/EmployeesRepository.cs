using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lesson4.Repositories.Database
{
	/// <summary>
	/// Репозиторий с информацией о работниках (реализация на основе Entity Framework)
	/// </summary>
	public class EmployeesRepository : PersonsRepository<Employee>, IEmployeesRepository
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public EmployeesRepository(ILogger<EmployeesRepository> logger) : base(logger, "Employees repository (database)") { }
	}
}
