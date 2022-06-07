using Microsoft.Extensions.Logging;
using Timesheets.Models.Data;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// Универсальный репозиторий (реализация на основе Entity Framework)
	/// </summary>
	public class GenericRepository<T, DB> : RepositoryErrorHandler, IGenericRepository<T> where T : GenericStorage where DB : T, new()
	{
		// Конфигурационная строка для подключения к серверу, общая для всех наследников GenericRepository
		protected const string ConnectionString = "Host=127.0.0.1;Database=Timesheets;Username=GeekBrains;Password=gb9452;";
		protected readonly GenericDbContext<DB> context;

		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected GenericRepository(ILogger<GenericRepository<T, DB>> logger, string title) : base(logger, title)
		{
			context = new GenericDbContext<DB>(ConnectionString);
		}

		/// <summary>
		/// Получение записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Запрошенная запись</returns>
		public virtual T GetById(int id)
		{
			logger.LogDebug($"{title}: get id {id}.");
			DB record = context.Base.Find(id);
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
			DB record = context.Base.Find(item.Id);
			if (record != null) ThrowAlreadyExistsException("create", item.Id);
			record = new();
			record.Copy(item);
			context.Add(record);
			if (context.SaveChanges() == 0) ThrowGenericException("create", item.Id);
		}

		/// <summary>
		/// Изменение существующей записи в базе данных
		/// </summary>
		/// <param name="item">Изменяемая запись</param>
		public virtual void Update(T item)
		{
			logger.LogDebug($"{title}: update id {item.Id}.");
			DB record = context.Base.Find(item.Id);
			if (record == null) ThrowNotFoundException("update", item.Id);
			record.Copy(item);
			if (context.SaveChanges() == 0) ThrowGenericException("update", item.Id);
		}

		/// <summary>
		/// Удаление записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public virtual void Delete(int id)
		{
			logger.LogDebug($"{title}: delete id {id}.");
			DB record = context.Base.Find(id);
			if (record == null) ThrowNotFoundException("delete", id);
			context.Base.Remove(record);
			if (context.SaveChanges() == 0) ThrowGenericException("delete", id);
		}
	}
}
