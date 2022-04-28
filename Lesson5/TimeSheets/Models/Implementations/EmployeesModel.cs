using AutoMapper;
using Microsoft.Extensions.Logging;
using Timesheets.Models.Interfaces;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
{
	/// <summary>
	/// ���������� ���������� ������ ������ ��� ���������� ����������� � ����������
	/// </summary>
	public class EmployeesModel : PersonsModel, IEmployeesModel
	{
		/// <summary>
		/// ����������� ������ � ���������� Dependency Injection
		/// </summary>
		public EmployeesModel(IEmployeesRepository repository, IMapper mapper, ILogger<EmployeesModel> logger) : base(repository, mapper, logger, "Employees model") { }
	}
}
