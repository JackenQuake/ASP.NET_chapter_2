using Microsoft.Extensions.Logging;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.List
{
	/// <summary>
	/// Репозиторий с информацией о клиентах (реализация на основе List)
	/// </summary>
	public class ClientsRepository : PersonsRepository, IClientsRepository
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public ClientsRepository(ILogger<ClientsRepository> logger) : base(logger, "Clients repository (list)") { }
	}
}
