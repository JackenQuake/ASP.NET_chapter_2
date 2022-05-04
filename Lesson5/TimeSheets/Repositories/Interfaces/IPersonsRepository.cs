using System.Collections.Generic;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Interfaces
{
	/// <summary>
	/// Интерфейс репозитория с информацией о людях
	/// </summary>
	public interface IPersonsRepository : IGenericRepository<PersonStorage>
	{
		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <param name="name">Слово для поиска в именах (или null, если поиск не требуется)</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IEnumerable<PersonStorage> GetPersons(string name, int skip, int take);
	}
}
