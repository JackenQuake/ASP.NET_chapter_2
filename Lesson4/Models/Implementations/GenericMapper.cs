using AutoMapper;
using Lesson4.Models.Data;
using Lesson4.Models.Interfaces;
using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Lesson4.Models.Implementations
{
	/// <summary>
	/// Универсальная обертка над AutoMapper для операций с базой данных
	/// Помимо объявленных в интерфейсе методов, реализующих четыре CRUD операции
	/// добавлен protected метод MassMap, на основании которого удобно строить методы Get с разными условиями
	/// </summary>
	public class GenericMapper<TDto, TStorage> : IGenericMapper<TDto, TStorage> where TDto : GenericDto where TStorage : GenericStorage
	{
		protected readonly IGenericRepository<TStorage> repository;
		protected readonly IMapper mapper;
		protected readonly ILogger<GenericMapper<TDto, TStorage>> logger;
		protected readonly string title;

		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected GenericMapper(IGenericRepository<TStorage> repository, IMapper mapper, ILogger<GenericMapper<TDto, TStorage>> logger, string title)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;
			this.title = title;
			logger.LogDebug($"{title}: created.");
		}

		/// <summary>
		/// Преобразует полученную из базы выборку в список объектов TDto
		/// </summary>
		/// <param name="data">IEnumerable, представляющий собой выборку из базы данных</param>
		/// <returns>Список записей из базы данных</returns>
		protected IReadOnlyCollection<TDto> MassMap(IEnumerable<TStorage> data)
		{
			var result = new List<TDto>();
			foreach (TStorage e in data) result.Add(mapper.Map<TDto>(e));
			return result.AsReadOnly();
		}

		/// <summary>
		/// Получение записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Запрошенная запись</returns>
		public TDto GetById(int id)
		{
			logger.LogDebug($"{title}: get person {id}.");
			return mapper.Map<TDto>(repository.GetById(id));
		}

		/// <summary>
		/// Добавление новой записи в базу данных
		/// </summary>
		/// <param name="item">Добавляемая запись</param>
		public void Create(TDto item)
		{
			logger.LogError($"{title}: create id {item.Id}.");
			repository.Create(mapper.Map<TStorage>(item));
		}

		/// <summary>
		/// Изменение существующей записи в базе данных
		/// </summary>
		/// <param name="item">Изменяемая запись</param>
		public void Update(TDto item)
		{
			logger.LogError($"{title}: update id {item.Id}.");
			repository.Update(mapper.Map<TStorage>(item));
		}

		/// <summary>
		/// Удаление записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public void Delete(int id)
		{
			logger.LogError($"{title}: delete id {id}.");
			repository.Delete(id);
		}
	}
}
