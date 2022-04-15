namespace Lesson3.Models
{
	/// <summary>
	/// Описание персоны в коллекции для уровня Controller
	/// </summary>
	public class PersonDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Company { get; set; }
		public int Age { get; set; }
	}
}
