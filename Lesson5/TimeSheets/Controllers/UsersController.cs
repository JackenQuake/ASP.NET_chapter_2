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
	/// ���������� ��� ���������� �������������� � ������������
	/// </summary>
	public class UsersController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly ILogger<UsersController> logger;

		/// <summary>
		/// ����������� ������ � ���������� Dependency Injection
		/// </summary>
		public UsersController(IUserService userService, ILogger<UsersController> logger)
		{
			this.userService = userService;
			this.logger = logger;
			logger.LogDebug("Users controller: created.");
		}

		/// <summary>
		/// �������� �������������� ������������
		/// </summary>
		/// <param name="user">��� ������������</param>
		/// <param name="password">������ ������������</param>
		/// <returns>����� � ������-�����</returns>
		/// <response code="200">���� �� ������</response>
		/// <response code="400">���� ��� ��� ������ �������</response>
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
		/// ��������� ������-����� �� cookie-����� ���������
		/// </summary>
		/// <returns>����������� ������-�����</returns>
		/// <response code="200">���� �� ������</response>
		/// <response code="401">��� ������ �����������</response>
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
		/// ��������� ������-����� � cookie ������� ������
		/// </summary>
		/// <param name="token">����������� �����</param>
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
