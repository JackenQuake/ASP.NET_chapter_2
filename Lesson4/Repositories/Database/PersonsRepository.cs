using Lesson4.Models.Data;
using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Lesson4.Repositories.Database
{
	/// <summary>
	/// Репозиторий с информацией о людях (реализация на основе Entity Framework)
	/// </summary>
	public class PersonsRepository<DB> : GenericRepository<PersonStorage, DB>, IPersonsRepository where DB : PersonStorage, new()
	{
		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected PersonsRepository(ILogger<PersonsRepository<DB>> logger, string title) : base(logger, title) { }

		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <param name="name">Слово для поиска в именах (или null, если поиск не требуется)</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IEnumerable<PersonStorage> GetPersons(string name, int skip, int take)
		{
			IQueryable<DB> res;
			if (name == null)
			{
				logger.LogDebug($"{title}: get all skip {skip} take {take}.");
				res = context.Base.Where(x => true);
			} else
			{
				logger.LogDebug($"{title}: get by name {name} skip {skip} take {take}.");
				res = context.Base.Where(x => (x.FirstName.Contains(name)) || (x.LastName.Contains(name)));
			}
			if (skip > 0) res = res.Skip(skip);
			if (take > 0) res = res.Take(take);
			return res;
		}
	}
}
