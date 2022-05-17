using Microsoft.Extensions.Logging;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// Репозиторий с информацией о клиентах (реализация на основе Entity Framework)
	/// </summary>
	public class ClientsRepository : PersonsRepository<Client>, IClientsRepository
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public ClientsRepository(ILogger<ClientsRepository> logger) : base(logger, "Clients repository (database)") { }
	}
}
