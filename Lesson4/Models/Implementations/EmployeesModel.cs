using AutoMapper;
using Lesson4.Models.Interfaces;
using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lesson4.Models.Implementations
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
