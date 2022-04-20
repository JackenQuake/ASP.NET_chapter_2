using Lesson4.Models.Data;
using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Lesson4.Repositories.List
{
	/// <summary>
	/// Репозиторий с информацией о людях (реализация на основе List)
	/// </summary>
	public partial class PersonsRepository : GenericRepository<PersonStorage>, IPersonsRepository
	{
		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected PersonsRepository(ILogger<PersonsRepository> logger, string title) : base(logger, title)
		{
			InitializeTestData();
		}

		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <param name="name">Слово для поиска в именах (или null, если поиск не требуется)</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IEnumerable<PersonStorage> GetPersons(string name, int skip, int take)
		{
			Predicate<PersonStorage> filter = (name == null) ? (e => true) : (e => (e.FirstName.Contains(name)) || (e.LastName.Contains(name)));
			return Get(filter, skip, take);
		}
	}
}
