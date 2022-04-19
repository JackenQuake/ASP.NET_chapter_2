using Lesson3.Models;
using System.Collections.Generic;

namespace Lesson3.Repositories
{
	/// <summary>
	/// Интерфейс репозитория с информацией о людях
	/// </summary>
	public interface IPersonsRepository
	{
		/// <summary>
		/// Получение списка людей (выборки из базы данных)
		/// </summary>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonStorage> GetAll();

		/// <summary>
		/// Получает информацию о человеке по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Информация об указанном человеке</returns>
		public PersonStorage GetById(int id);

		/// <summary>
		/// Добавление новой персоны в коллекцию
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Create(PersonStorage item);

		/// <summary>
		/// Обновление существующей персоны в коллекции
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Update(PersonStorage item);

		/// <summary>
		/// Удаление персоны из коллекции
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public void Delete(int id);
	}
}
