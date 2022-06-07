using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Timesheets.Models.Data;
using Timesheets.Models.Interfaces;

namespace Timesheets.Controllers
{
	/// <summary>
	/// Контроллер для управления пользователями и авторизацией
	/// </summary>
	public class UsersController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly ILogger<UsersController> logger;

		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public UsersController(IUserService userService, ILogger<UsersController> logger)
		{
			this.userService = userService;
			this.logger = logger;
			logger.LogDebug("Users controller: created.");
		}

		/// <summary>
		/// Проводит аутентификацию пользователя
		/// </summary>
		/// <param name="user">Имя пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <returns>Токен и рефреш-токен</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="400">Если имя или пароль неверны</response>
		[AllowAnonymous]
		[HttpPost("/authenticate")]
		public IActionResult Authenticate([FromQuery] string user, string password)
		{
			TokenResponse token = userService.Authenticate(user, password);
			if (token is null)
			{
				return BadRequest(new { message = "Username or password is incorrect" });
			}
			SetTokenCookie(token.RefreshToken);
			return Ok(token);
		}

		/// <summary>
		/// Обновляет рефреш-токен из cookie-части заголовка
		/// </summary>
		/// <returns>Обновленный рефреш-токен</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="401">При ошибке авторизации</response>
		[Authorize]
		[HttpPost("refresh-token")]
		public IActionResult Refresh()
		{
			string oldRefreshToken = Request.Cookies["refreshToken"];
			string newRefreshToken = userService.RefreshToken(oldRefreshToken);
			if (string.IsNullOrWhiteSpace(newRefreshToken))
			{
				return Unauthorized(new { message = "Invalid token" });
			}
			SetTokenCookie(newRefreshToken);
			return Ok(newRefreshToken);
		}

		/// <summary>
		/// Обновляет рефреш-токен в cookie разделе ответа
		/// </summary>
		/// <param name="token">Сохраняемый токен</param>
		private void SetTokenCookie(string token)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = DateTime.UtcNow.AddDays(7)
			};
			Response.Cookies.Append("refreshToken", token, cookieOptions);
		}
	}
}
