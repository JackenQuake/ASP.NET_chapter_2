using System.ComponentModel.DataAnnotations.Schema;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// ����� ��� Client - PersonStorage, ��������� � �� Clients
	/// </summary>
	[Table("Clients", Schema = "public")]
	public class Client : PersonStorage
	{
	}
}
