using Microsoft.Extensions.Logging;
using System;

namespace Lesson4.Repositories
{
	/// <summary>
	/// Класс упрощает обработку ошибок, предоставляя методы для одновременной отправки исключения и записи в лог
	/// </summary>
	public class RepositoryErrorHandler
	{
		protected readonly ILogger<RepositoryErrorHandler> logger;
		protected readonly string title;

		/// <summary>
		/// Конструктор класса; protected, поскольку класс предназначен не для использования, а как основа для потомков
		/// </summary>
		protected RepositoryErrorHandler(ILogger<RepositoryErrorHandler> logger, string title)
		{
			this.logger = logger;
			this.title = title;
			logger.LogDebug($"{title}: created.");
		}

		/// <summary>
		/// Отправить и записать в лог исключение "запись в базе данных не найдена"
		/// </summary>
		/// <param name="operation">Операция, вызвавшая исключение</param>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		protected void ThrowNotFoundException(string operation, int id)
		{
			logger.LogError($"{title}: {operation}: record {id} not found.");
			throw new RecordNotFoundException($"Record {id} not found.");
		}

		/// <summary>
		/// Отправить и записать в лог исключение "запись в базе данных уже существует"
		/// </summary>
		/// <param name="operation">Операция, вызвавшая исключение</param>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		protected void ThrowAlreadyExistsException(string operation, int id)
		{
			logger.LogError($"{title}: {operation}: record {id} already exists.");
			throw new RecordAlreadyExistsException($"Record {id} not found.");
		}

		/// <summary>
		/// Отправить и записать в лог исключение "ошибка обновления базы данных"
		/// </summary>
		/// <param name="operation">Операция, вызвавшая исключение</param>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		protected void ThrowGenericException(string operation, int id)
		{
			logger.LogError($"{title}: {operation} record {id} unknown error.");
			throw new InvalidOperationException($"Database update error.");
		}
	}

	/// <summary>
	/// Исключение "запись в базе данных не найдена"
	/// </summary>
	public class RecordNotFoundException : ArgumentException
	{
		public RecordNotFoundException(string msg) : base(msg) { }
	}

	/// <summary>
	/// Исключение "запись в базе данных уже существует"
	/// </summary>
	public class RecordAlreadyExistsException : ArgumentException
	{
		public RecordAlreadyExistsException(string msg) : base(msg) { }
	}
}
