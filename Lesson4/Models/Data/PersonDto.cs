namespace Lesson4.Models.Data
{
	/// <summary>
	/// Описание человека для уровня Controller
	/// </summary>
	public class PersonDto : GenericDto
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Company { get; set; }

		public int Age { get; set; }
	}
}
