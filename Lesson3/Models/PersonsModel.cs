using System;
using System.Collections.Generic;
using Lesson3.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Lesson3.Models
{
	/// <summary>
	/// Реализация интерфейса уровня модели для управления информацией о людях
	/// Обеспечивает преобразование между уровнями Controller и Model
	/// и фильтрацию информации из репозитория в соответствии с запросами
	/// </summary>
	public class PersonsModel : IPersonsModel
	{
		private readonly IPersonsRepository repository;
		private readonly IMapper mapper;
		private readonly ILogger<PersonsModel> logger;

		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public PersonsModel(IPersonsRepository repository, IMapper mapper, ILogger<PersonsModel> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;
			logger.LogDebug("Persons model: created.");
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных) с фильтрацией и пагинацией
		/// Основной инструмент для выборки, позволяющий строить нужные запросы простым указанием предиката для проверки
		/// </summary>
		/// <param name="Filter">Предикат, проверяющий, что указанную запись нужно включать в ответ</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список записей из базы данных</returns>
		private IReadOnlyCollection<PersonDto> Get(Predicate<PersonStorage> Filter, int skip, int take) {
			var data = repository.GetAll();
			var result = new List<PersonDto>();
			foreach (PersonStorage e in data)
			{
				if (!Filter(e)) continue;
				if (skip > 0) { skip--; continue; }
				result.Add(mapper.Map<PersonDto>(e));
				if ((take > 0) && ((--take) == 0)) break;
			}
			return result.AsReadOnly();
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных)
		/// </summary>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonDto> GetAll()
		{
			logger.LogDebug("Persons model: get all.");
			return Get(e => true, 0, 0);
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных) с пагинацией
		/// </summary>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonDto> GetAll(int skip, int take)
		{
			logger.LogDebug($"Persons model: get all, skip {skip} take {take}.");
			return Get(e => true, skip, take);
		}

		/// <summary>
		/// Служебная функция для проверки наличия указанного слова в имени или фамилии человека
		/// </summary>
		/// <param name="item">Запись из коллекции для проверки</param>
		/// <param name="name">Слово для поиска в именах</param>
		/// <returns>Содержит ли имя или фамилия указанное слово</returns>
		private bool CheckName(PersonStorage item, string name)
		{
			return (item.FirstName.IndexOf(name) >= 0) || (item.LastName.IndexOf(name) >= 0);
		}

		/// <summary>
		/// Поиск человека по имени
		/// </summary>
		/// <param name="name">Слово для поиска в именах</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IReadOnlyCollection<PersonDto> GetByName(string name)
		{
			logger.LogDebug($"Persons model: get by name {name}.");
			return Get(e => CheckName(e, name), 0, 0);
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
			logger.LogDebug($"Persons model: get by name {name}, skip {skip} take {take}.");
			return Get(e => CheckName(e, name), skip, take);
		}

		/// <summary>
		/// Получает информацию о человеке по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Информация об указанном человеке</returns>
		public PersonDto GetById(int id)
		{
			logger.LogDebug($"Persons model: get person {id}.");
			return mapper.Map<PersonDto>(repository.GetById(id));
		}

		/// <summary>
		/// Добавление новой персоны в коллекцию
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Create(PersonDto item)
		{
			logger.LogError($"Persons model: create person {item.Id}.");
			repository.Create(mapper.Map<PersonStorage>(item));
		}

		/// <summary>
		/// Обновление существующей персоны в коллекции
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Update(PersonDto item)
		{
			logger.LogError($"Persons model: update person {item.Id}.");
			repository.Update(mapper.Map<PersonStorage>(item));
		}

		/// <summary>
		/// Удаление персоны из коллекции
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public void Delete(int id)
		{
			logger.LogError($"Persons model: delete person {id}.");
			repository.Delete(id);
		}
	}
}
