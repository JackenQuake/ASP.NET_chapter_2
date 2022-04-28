using System.ComponentModel.DataAnnotations.Schema;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// Класс для Employee - PersonStorage, связанный с БД Employees
	/// </summary>
	[Table("Employees", Schema = "public")]
	public class Employee : PersonStorage
	{
	}
}
