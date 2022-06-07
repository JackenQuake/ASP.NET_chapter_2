using AutoMapper;
using Microsoft.Extensions.Logging;
using Timesheets.Models.Interfaces;
using Timesheets.Models.Validations;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
{
	/// <summary>
	/// ���������� ���������� ������ ������ ��� ���������� ����������� � ��������
	/// </summary>
	public class ClientsModel : PersonsModel, IClientsModel
	{
		/// <summary>
		/// ����������� ������ � ���������� Dependency Injection
		/// </summary>
		public ClientsModel(IClientsRepository repository, IMapper mapper, IPersonValidationService validationService, ILogger<ClientsModel> logger) :
			base(repository, mapper, validationService, logger, "Clients model")
		{ }
	}
}