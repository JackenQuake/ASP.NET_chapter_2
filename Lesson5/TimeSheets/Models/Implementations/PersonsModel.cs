using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Timesheets.Models.Data;
using Timesheets.Models.Interfaces;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
{
	/// <summary>
	/// Реализация интерфейса уровня модели для управления информацией о людях на основе GenericMapper
	/// Добавляет конкретные варианты запросов для списков людей (поиск по имени)
	/// </summary>
	public class PersonsModel : GenericMapper<PersonDto, PersonStorage>, IPersonsModel
	{
		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected PersonsModel(IPersonsRepository repository, IMapper mapper, ILogger<PersonsModel> logger, string title) : base(repository, mapper, logger, title) { }

		/// <summary>
		/// Получение списка людей (выборки из базы данных)
		/// </summary>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonDto> GetAll()
		{
			logger.LogDebug($"{title}: get all.");
			return MassMap(((IPersonsRepository)repository).GetPersons(null, 0, 0));
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных) с пагинацией
		/// </summary>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonDto> GetAll(int skip, int take)
		{
			logger.LogDebug($"{title}: get all, skip {skip} take {take}.");
			return MassMap(((IPersonsRepository)repository).GetPersons(null, skip, take));
		}

		/// <summary>
		/// Поиск человека по имени
		/// </summary>
		/// <param name="name">Слово для поиска в именах</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IReadOnlyCollection<PersonDto> GetByName(string name)
		{
			logger.LogDebug($"{title}: get by name {name}.");
			return MassMap(((IPersonsRepository)repository).GetPersons(name, 0, 0));
		}

		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <param name="name">Слово для поиска в именах</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IReadOnlyCollection<PersonDto> GetByName(string name, int skip, int take)
		{
			logger.LogDebug($"{title}: get by name {name}, skip {skip} take {take}.");
			return MassMap(((IPersonsRepository)repository).GetPersons(name, skip, take));
		}
	}
}
