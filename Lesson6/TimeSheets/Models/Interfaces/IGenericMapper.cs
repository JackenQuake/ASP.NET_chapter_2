using System.Collections.Generic;
using Timesheets.Models.Data;
using Timesheets.Models.Validations;

namespace Timesheets.Models.Interfaces
{
	/// <summary>
	/// Универсальная обертка над AutoMapper для операций с базой данных
	/// </summary>
	public interface IGenericMapper<TDto, TStorage> where TDto : GenericDto where TStorage : GenericStorage
	{
		/// <summary>
		/// Получение записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Запрошенная запись</returns>
		public TDto GetById(int id);

		/// <summary>
		/// Добавление новой записи в базу данных
		/// </summary>
		/// <param name="item">Добавляемая запись</param>
		public IReadOnlyList<IOperationFailure> Create(TDto item);

		/// <summary>
		/// Изменение существующей записи в базе данных
		/// </summary>
		/// <param name="item">Изменяемая запись</param>
		public IReadOnlyList<IOperationFailure> Update(TDto item);

		/// <summary>
		/// Удаление записи из базы данных по ключу
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public void Delete(int id);
	}
}
