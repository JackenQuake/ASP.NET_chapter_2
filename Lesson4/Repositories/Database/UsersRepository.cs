﻿using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lesson4.Repositories.Database
{
	/// <summary>
	/// Репозиторий с информацией о клиентах (реализация на основе Entity Framework)
	/// </summary>
	public class UsersRepository : PersonsRepository<User>, IUsersRepository
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public UsersRepository(ILogger<UsersRepository> logger) : base(logger, "Users repository (database)") { }
	}
}
