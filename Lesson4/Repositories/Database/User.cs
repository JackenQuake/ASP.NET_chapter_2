using Lesson4.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson4.Repositories.Database
{
	/// <summary>
	/// Класс для User - PersonStorage, связанный с БД Users
	/// </summary>
	[Table("Users", Schema = "public")]
	public class User : PersonStorage
	{
	}
}
