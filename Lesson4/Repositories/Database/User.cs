using Lesson4.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson4.Repositories.Database
{
	/// <summary>
	/// ����� ��� User - PersonStorage, ��������� � �� Users
	/// </summary>
	[Table("Users", Schema = "public")]
	public class User : PersonStorage
	{
	}
}
