using System.Collections.Generic;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Interfaces
{
	/// <summary>
	/// Интерфейс репозитория с информацией о пользователях
	/// </summary>
	public interface IUsersRepository : IGenericRepository<UserStorage>
	{
		/// <summary>
		/// Поиск пользователей по имени с пагинацией
		/// </summary>
		/// <param name="name">Слово для поиска в именах (или null, если поиск не требуется)</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список пользователей, чье имя содержат указанное слово</returns>
		public IEnumerable<UserStorage> GetUsers(string name, int skip, int take);

		/// <summary>
		/// Поиск пользователя с указанным именем
		/// </summary>
		/// <param name="name">Имя для поиска</param>
		/// <returns>Пользователь с указанным именем или null, если не найден</returns>
		public UserStorage GetUser(string name);

		/// <summary>
		/// Поиск пользователя по рефреш-токену
		/// </summary>
		/// <param name="token">Токен для поиска</param>
		/// <returns>Пользователь с указанным токеном или null, если не найден</returns>
		public UserStorage GetUserByToken(string token);
	}
}
