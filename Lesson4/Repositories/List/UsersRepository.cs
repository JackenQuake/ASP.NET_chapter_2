using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lesson4.Repositories.List
{
	/// <summary>
	/// Репозиторий с информацией о клиентах (реализация на основе List)
	/// </summary>
	public class UsersRepository : PersonsRepository, IUsersRepository
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public UsersRepository(ILogger<UsersRepository> logger) : base(logger, "Users repository (list)") { }
	}
}
