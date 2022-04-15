using System.Collections.Generic;

namespace Lesson3.Models
{
	/// <summary>
	/// Интерфейс уровня модели для управления информацией о людях
	/// </summary>
	public interface IPersonsModel
	{
		/// <summary>
		/// Получение списка людей (выборки из базы данных)
		/// </summary>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonDto> GetAll();

		/// <summary>
		/// Получение списка людей (выборки из базы данных) с пагинацией
		/// </summary>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список записей из базы данных</returns>
		public IReadOnlyCollection<PersonDto> GetAll(int skip, int take);

		/// <summary>
		/// Поиск человека по имени
		/// </summary>
		/// <param name="name">Слово для поиска в именах</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IReadOnlyCollection<PersonDto> GetByName(string name);

		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <param name="name">Слово для поиска в именах</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		public IReadOnlyCollection<PersonDto> GetByName(string name, int skip, int take);

		/// <summary>
		/// Получает информацию о человеке по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Информация об указанном человеке</returns>
		public PersonDto GetById(int id);

		/// <summary>
		/// Добавление новой персоны в коллекцию
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Create(PersonDto item);

		/// <summary>
		/// Обновление существующей персоны в коллекции
		/// </summary>
		/// <param name="item">Информация о человеке</param>
		public void Update(PersonDto item);

		/// <summary>
		/// Удаление персоны из коллекции
		/// </summary>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		public void Delete(int id);
	}
}
