using System;
using System.Collections.Generic;
using Lesson3.Models;
using Microsoft.Extensions.Logging;

namespace Lesson3.Repositories
{
	/// <summary>
	/// Репозиторий с информацией о людях (реализация на основе List)
	/// </summary>
	public partial class PersonsRepository : IPersonsRepository
	{
		List<PersonStorage> data;
		private readonly ILogger<PersonsRepository> logger;

		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public PersonsRepository(ILogger<PersonsRepository> logger)
		{
			this.logger = logger;
			//data = new List<PersonStorage>()
			InitializeTestData();
			logger.LogDebug("Persons repository: created.");
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных)
		/// </summary>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonStorage> GetAll()
		{
			logger.LogDebug("Persons repository: get all.");
			return data.AsReadOnly();
		}

		/// <summary>
		/// Служебная функци: ищет в коллекции запись по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Запись с указанным идентификатором</returns>
		private PersonStorage FindElement(int id)
		{
			foreach (PersonStorage e in data) if (e.Id == id) return e;
			return null;
		}

		/// <summary>
		/// Получает информацию о человеке по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Информация об указанном человеке</returns>
		public PersonStorage GetById(int id)
		{
			var e = FindElement(id);
			if (e != null)
			{
				logger.LogDebug($"Persons repository: get person {id}.");
				return e;
			} else { 
				logger.LogError($"Persons repository: get person {id} - not found!.");
				throw new ArgumentException();
			}
		}

		/// <summary>
		/// Добавление новой персоны в коллекцию
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Create(PersonStorage item)
		{
			var e = FindElement(item.Id);
			if (e != null)
			{
				logger.LogError($"Persons repository: create person {item.Id} - already exists!.");
				throw new ArgumentException();
			}
			else
			{
				logger.LogDebug($"Persons repository: create person {item.Id}.");
				data.Add(item);
			}
		}

		/// <summary>
		/// Обновление существующей персоны в коллекции
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Update(PersonStorage item)
		{
			var e = FindElement(item.Id);
			if (e != null)
			{
				logger.LogDebug($"Persons repository: update person {item.Id}.");
				data.Remove(e);
				data.Add(item);
			}
			else
			{
				logger.LogDebug($"Persons repository: update person {item.Id} - not found!.");
				throw new ArgumentException();
			}
		}

		/// <summary>
		/// Удаление персоны из коллекции
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public void Delete(int id)
		{
			var e = FindElement(id);
			if (e != null)
			{
				logger.LogDebug($"Persons repository: delete person {id}.");
				data.Remove(e);
			}
			else
			{
				logger.LogError($"Persons repository: delete person {id} - not found!.");
				throw new ArgumentException();
			}
		}
	}
}
