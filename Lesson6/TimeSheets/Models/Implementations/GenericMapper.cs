using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Timesheets.Models.Data;
using Timesheets.Models.Interfaces;
using Timesheets.Models.Validations;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
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
		protected readonly IValidationService<TDto> validationService;
		protected readonly ILogger<GenericMapper<TDto, TStorage>> logger;
		protected readonly string title;

		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected GenericMapper(IGenericRepository<TStorage> repository, IMapper mapper, IValidationService<TDto> validationService, ILogger<GenericMapper<TDto, TStorage>> logger, string title)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.validationService = validationService;
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
		/// <returns>Список ошибок валидации</returns>
		public IReadOnlyList<IOperationFailure> Create(TDto item)
		{
			logger.LogError($"{title}: create id {item.Id}.");
			var validity = validationService.ValidateEntity(item);
			if (validity.Count == 0) repository.Create(mapper.Map<TStorage>(item));
			return validity;
		}

		/// <summary>
		/// Изменение существующей записи в базе данных
		/// </summary>
		/// <param name="item">Изменяемая запись</param>
		/// <returns>Список ошибок валидации</returns>
		public IReadOnlyList<IOperationFailure> Update(TDto item)
		{
			logger.LogError($"{title}: update id {item.Id}.");
			var validity = validationService.ValidateEntity(item);
			if (validity.Count == 0) repository.Update(mapper.Map<TStorage>(item));
			return validity;
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
