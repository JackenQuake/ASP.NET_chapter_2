using Microsoft.Extensions.Logging;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.List
{
	/// <summary>
	/// Репозиторий с информацией о работниках (реализация на основе List)
	/// </summary>
	public class EmployeesRepository : PersonsRepository, IEmployeesRepository
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public EmployeesRepository(ILogger<EmployeesRepository> logger) : base(logger, "Employees repository (list)") { }
	}
}
