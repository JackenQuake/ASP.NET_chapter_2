using Lesson4.Models.Data;
using System.Collections.Generic;

namespace Lesson4.Models.Interfaces
{
	/// <summary>
	/// Интерфейс уровня модели для управления информацией о людях
	/// </summary>
	public interface IPersonsModel : IGenericMapper<PersonDto, PersonStorage>
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
	}
}
