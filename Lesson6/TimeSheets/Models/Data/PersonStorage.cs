namespace Timesheets.Models.Data
{
	/// <summary>
	/// Описание человека для уровня Model
	/// </summary>
	public class PersonStorage : GenericStorage
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Company { get; set; }

		public int Age { get; set; }

		/// <summary>
		/// Копирует содержимое другого экземпляра этого же класса. Требуется для обновления репозиториев
		/// <param name="a">Объект, который требуется скопировать</param>
		/// </summary>
		public override void Copy(object a)
		{
			base.Copy(a);
			FirstName = ((PersonStorage)a).FirstName;
			LastName = ((PersonStorage)a).LastName;
			Email = ((PersonStorage)a).Email;
			Company = ((PersonStorage)a).Company;
			Age = ((PersonStorage)a).Age;
		}
	}
}
