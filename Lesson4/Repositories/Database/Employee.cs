using Lesson4.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson4.Repositories.Database
{
	/// <summary>
	/// ����� ��� Employee - PersonStorage, ��������� � �� Employees
	/// </summary>
	[Table("Employees", Schema = "public")]
	public class Employee : PersonStorage
	{
	}
}
