using System.ComponentModel.DataAnnotations.Schema;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// ����� ��� User - �������� � ���� ������ ���������� � ������������
	/// </summary>
	[Table("Users", Schema = "public")]
	public class User : UserStorage
	{
	}
}
