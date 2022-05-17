using Timesheets.Models.Data;

namespace Timesheets.Models.Interfaces
{
	/// <summary>
	/// ��������� ������ ������ ��� ���������� �������������� � ������������
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// �������� �������������� ������������
		/// </summary>
		/// <param name="user">��� ������������</param>
		/// <param name="password">������ ������������</param>
		/// <returns>��������� � ������� � ������-�������</returns>
		TokenResponse Authenticate(string user, string password);

		/// <summary>
		/// ��������� ������-�����
		/// </summary>
		/// <param name="token">������ ������-�����</param>
		/// <returns>����������� ������-�����</returns>
		string RefreshToken(string token);
	}
}
