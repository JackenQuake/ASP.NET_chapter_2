using AutoMapper;
using Microsoft.Extensions.Logging;
using Timesheets.Models.Interfaces;
using Timesheets.Models.Validations;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
{
	/// <summary>
	/// Реализация интерфейса уровня модели для управления информацией о работниках
	/// </summary>
	public class EmployeesModel : PersonsModel, IEmployeesModel
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public EmployeesModel(IEmployeesRepository repository, IMapper mapper, IPersonValidationService validationService, ILogger<EmployeesModel> logger) :
			base(repository, mapper, validationService, logger, "Employees model")
		{ }
	}
}
