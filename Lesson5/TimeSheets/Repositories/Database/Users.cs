using System.ComponentModel.DataAnnotations.Schema;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// Класс для User - хранимая в базе данных информация о пользователе
	/// </summary>
	[Table("Users", Schema = "public")]
	public class User : UserStorage
	{
	}
}
