using AutoMapper;
using Lesson4.Models.Interfaces;
using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lesson4.Models.Implementations
{
	/// <summary>
	/// ���������� ���������� ������ ������ ��� ���������� ����������� � ��������
	/// </summary>
	public class UsersModel : PersonsModel, IUsersModel
	{
		/// <summary>
		/// ����������� ������ � ���������� Dependency Injection
		/// </summary>
		public UsersModel(IUsersRepository repository, IMapper mapper, ILogger<UsersModel> logger) : base(repository, mapper, logger, "Users model") { }
	}
}