using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timesheets.Models.Data;
using Timesheets.Models.Interfaces;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
{
	/// <summary>
	/// ���������� ���������� ������ ������ ��� ���������� �������������� � ������������
	/// </summary>
	class UserService : IUserService
	{
		public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!! Im blue da ba dee da ba di da ba dee da ba di da d ba dee da ba di da ba dee";
		private readonly IUsersRepository repository;
		private readonly ILogger<UserService> logger;

		/// <summary>
		/// ����������� ������ � ���������� Dependency Injection
		/// </summary>
		public UserService(IUsersRepository repository, ILogger<UserService> logger)
		{
			this.repository = repository;
			this.logger = logger;
			logger.LogDebug("User Service: created.");
		}

		/// <summary>
		/// �������� �������������� ������������
		/// </summary>
		/// <param name="user">��� ������������</param>
		/// <param name="password">������ ������������</param>
		/// <returns>����� � ������-�����</returns>
		public TokenResponse Authenticate(string user, string password)
		{
			if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password)) return null;
			UserStorage data = repository.GetUser(user);
			if ((data == null) || (data.Password != password)) return null;
			UpdateRefreshToken(data);
			return new TokenResponse() { Token = GenerateJwtToken(data.Id, 15), RefreshToken = data.RefreshToken };
		}

		/// <summary>
		/// ��������� ������-�����
		/// </summary>
		/// <param name="token">������ ������-�����</param>
		/// <returns>����������� ������-�����</returns>
		public string RefreshToken(string token)
		{
			UserStorage data = repository.GetUserByToken(token);
			if ((data == null) || (DateTime.UtcNow.Ticks >= data.Expires)) return null;
			UpdateRefreshToken(data);
			return data.RefreshToken;
		}

		/// <summary>
		/// ����������/��������� ������-����� � ��������� data � � �����������
		/// </summary>
		/// <param name="data">������ � ������������</param>
		private void UpdateRefreshToken(UserStorage data)
		{
			data.RefreshToken = GenerateJwtToken(data.Id, 360);
			data.Expires = DateTime.Now.AddMinutes(360).Ticks;
			repository.Update(data);
		}

		/// <summary>
		/// ���������� JWT-�����
		/// </summary>
		/// <param name="id">������������� ������������</param>
		/// <param name="minutes">����� �������� ������</param>
		/// <returns>JWT-�����</returns>
		private static string GenerateJwtToken(int id, int minutes)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			byte[] key = Encoding.ASCII.GetBytes(SecretCode);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, id.ToString()) }),
				Expires = DateTime.UtcNow.AddMinutes(minutes),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}