namespace Timesheets.Models.Data
{
	/// <summary>
	/// Токены, возвращаемые в ответ на запрос авторизации
	/// </summary>
	public class TokenResponse
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }
	}
}
