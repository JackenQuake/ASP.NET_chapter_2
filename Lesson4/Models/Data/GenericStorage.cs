namespace Lesson4.Models.Data
{
	/// <summary>
	/// Информационная запись общего вида для уровня Model
	/// </summary>
	public class GenericStorage
	{
		public int Id { get; set; }

		/// <summary>
		/// Копирует содержимое другого экземпляра этого же класса. Требуется для обновления репозиториев
		/// <param name="a">Объект, который требуется скопировать</param>
		/// </summary>
		public virtual void Copy(object a)
		{
			Id = ((GenericStorage)a).Id;
		}
	}
}
