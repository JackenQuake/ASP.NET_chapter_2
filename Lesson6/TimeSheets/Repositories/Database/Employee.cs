using System.ComponentModel.DataAnnotations.Schema;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// ����� ��� Employee - PersonStorage, ��������� � �� Employees
	/// </summary>
	[Table("Employees", Schema = "public")]
	public class Employee : PersonStorage
	{
	}
}
