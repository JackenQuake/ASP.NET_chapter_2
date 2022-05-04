using AutoMapper;
using Microsoft.Extensions.Logging;
using Timesheets.Models.Interfaces;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
{
	/// <summary>
	/// Реализация интерфейса уровня модели для управления информацией о клиентах
	/// </summary>
	public class ClientsModel : PersonsModel, IClientsModel
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public ClientsModel(IClientsRepository repository, IMapper mapper, ILogger<ClientsModel> logger) : base(repository, mapper, logger, "Clients model") { }
	}
}