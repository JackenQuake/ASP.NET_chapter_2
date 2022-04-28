using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Timesheets.Models.Data;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// Репозиторий с информацией о пользователях (реализация на основе Entity Framework)
	/// </summary>
	public class UsersRepository : GenericRepository<UserStorage, User>, IUsersRepository
	{
		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public UsersRepository(ILogger<UsersRepository> logger) : base(logger, "Users repository (database)") { }

		/// <summary>
		/// Поиск пользователей по имени с пагинацией
		/// </summary>
		/// <param name="name">Слово для поиска в именах (или null, если поиск не требуется)</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список пользователей, чье имя содержат указанное слово</returns>
		public IEnumerable<UserStorage> GetUsers(string name, int skip, int take)
		{
			IQueryable<User> res;
			if (name == null)
			{
				logger.LogDebug($"{title}: get all skip {skip} take {take}.");
				res = context.Base.Where(x => true);
			} else
			{
				logger.LogDebug($"{title}: get by name {name} skip {skip} take {take}.");
				res = context.Base.Where(x => x.Name.Contains(name));
			}
			if (skip > 0) res = res.Skip(skip);
			if (take > 0) res = res.Take(take);
			return res;
		}

		/// <summary>
		/// Поиск пользователя с указанным именем
		/// </summary>
		/// <param name="name">Имя для поиска</param>
		/// <returns>Пользователь с указанным именем или null, если не найден</returns>
		protected UserStorage FindUser(string name)
		{
			foreach (User e in context.Base.Where(x => (x.Name == name))) return e;
			return null;
		}

		/// <summary>
		/// Поиск пользователя с указанным именем
		/// </summary>
		/// <param name="name">Имя для поиска</param>
		/// <returns>Пользователь с указанным именем или null, если не найден</returns>
		public UserStorage GetUser(string name)
		{
			logger.LogDebug($"{title}: get by name {name}.");
			return FindUser(name);
		}

		/// <summary>
		/// Поиск пользователя по рефреш-токену
		/// </summary>
		/// <param name="token">Токен для поиска</param>
		/// <returns>Пользователь с указанным токеном или null, если не найден</returns>
		public UserStorage GetUserByToken(string token)
		{
			logger.LogDebug($"{title}: get by token {token}.");
			foreach (User e in context.Base.Where(x => (x.RefreshToken == token))) return e;
			return null;
		}

		/// <summary>
		/// Добавление нового пользователя в базу данных
		/// </summary>
		/// <param name="item">Добавляемая запись</param>
		public override void Create(UserStorage item)
		{
			var old = FindUser(item.Name);
			if (old != null) ThrowAlreadyExistsException("create", old.Id);
			base.Create(item);
		}

		/// <summary>
		/// Изменение существующего пользователя в базе данных
		/// </summary>
		/// <param name="item">Изменяемая запись</param>
		public override void Update(UserStorage item)
		{
			var old = FindUser(item.Name);
			if ((old != null) && (old.Id != item.Id)) ThrowAlreadyExistsException("update", old.Id);
			base.Update(item);
		}
	}
}
