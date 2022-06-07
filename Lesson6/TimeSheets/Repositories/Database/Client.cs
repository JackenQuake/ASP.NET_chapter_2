using System.ComponentModel.DataAnnotations.Schema;
using Timesheets.Models.Data;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// Класс для Client - PersonStorage, связанный с БД Clients
	/// </summary>
	[Table("Clients", Schema = "public")]
	public class Client : PersonStorage
	{
	}
}
