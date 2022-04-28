using Timesheets.Models.Data;

namespace Timesheets.Models.Interfaces
{
	/// <summary>
	/// Интерфейс уровня модели для управления пользователями и авторизацией
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Проводит аутентификацию пользователя
		/// </summary>
		/// <param name="user">Имя пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <returns>Структуру с токеном и рефреш-токеном</returns>
		TokenResponse Authenticate(string user, string password);

		/// <summary>
		/// Обновляет рефреш-токен
		/// </summary>
		/// <param name="token">Старый рефреш-токен</param>
		/// <returns>Обновленный рефреш-токен</returns>
		string RefreshToken(string token);
	}
}
