using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Timesheets.Models.Data;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.List
{
	/// <summary>
	/// Универсальный репозиторий (реализация на основе List)
	/// </summary>
	public class GenericRepository<T> : RepositoryErrorHandler, IGenericRepository<T> where T : GenericStorage
	{
		protected List<T> data;

		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected GenericRepository(ILogger<GenericRepository<T>> logger, string title) : base(logger, title)
		{
			data = new List<T>();
		}

		/// <summary>
		/// Получение выборки из базы данных по условию
		/// </summary>
		/// <param name="filter">Предикат, проверяющий, что указанную запись нужно включать в выборку</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Перечислитель записей из базы данных</returns>
		protected IEnumerable<T> Get(Predicate<T> filter, int skip, int take)
		{
			logger.LogDebug($"{title}: get by predicate, skip {skip} take {take}.");
			foreach (T record in data)
			{
				if (!filter(record)) continue;
				if (skip > 0) { skip--; continue; }
				yield return record;
				if ((take > 0) && ((--take) == 0)) break;
			}
			yield break;
		}

		/// <summary>
		/// Служебная функция: ищет в коллекции запись по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Запись с указанным идентификатором</returns>
		protected T FindElement(int id)
		{
			foreach (T record in data) if (record.Id == id) return record;
			return null;
		}

		/// <summary>
		/// Получение записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Запрошенная запись</returns>
		public virtual T GetById(int id)
		{
			logger.LogDebug($"{title}: get id {id}.");
			var record = FindElement(id);
			if (record == null) ThrowNotFoundException("get", id);
			return record;
		}

		/// <summary>
		/// Добавление новой записи в базу данных
		/// </summary>
		/// <param name="item">Добавляемая запись</param>
		public virtual void Create(T item)
		{
			logger.LogDebug($"{title}: create id {item.Id}.");
			var record = FindElement(item.Id);
			if (record != null) ThrowAlreadyExistsException("create", item.Id);
			data.Add(item);
		}

		/// <summary>
		/// Изменение существующей записи в базе данных
		/// </summary>
		/// <param name="item">Изменяемая запись</param>
		public virtual void Update(T item)
		{
			logger.LogDebug($"{title}: update id {item.Id}.");
			var record = FindElement(item.Id);
			if (record == null) ThrowNotFoundException("update", item.Id);
			record.Copy(item);
		}

		/// <summary>
		/// Удаление записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public virtual void Delete(int id)
		{
			logger.LogDebug($"{title}: delete id {id}.");
			var record = FindElement(id);
			if (record == null) ThrowNotFoundException("delete", id);
			data.Remove(record);
		}
	}
}
