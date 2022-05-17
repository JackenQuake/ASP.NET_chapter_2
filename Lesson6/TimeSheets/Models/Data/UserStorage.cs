namespace Timesheets.Models.Data
{
	/// <summary>
	/// �������� � ����������� ���������� � ������������
	/// </summary>
	public class UserStorage : GenericStorage
	{
		public string Name { get; set; }

		public string Password { get; set; }

		public string RefreshToken { get; set; }

		public long Expires { get; set; }

		public int Rights { get; set; }

		// Rights - ���� ������ ���� ������� �� ���������� ����������
		public const int MaskAdministrator  = 0x0001;  // �������������, ����� �������� ���
		public const int MaskChangePassword = 0x0002;  // ����� ����� ������ ���� ������

		public bool IsAdministrator() => ((Rights & MaskAdministrator) != 0);

		public bool CanChangePassword() => ((Rights & MaskChangePassword) != 0);

		/// <summary>
		/// �������� ���������� ������� ���������� ����� �� ������. ��������� ��� ���������� ������������
		/// <param name="a">������, ������� ��������� �����������</param>
		/// </summary>
		public override void Copy(object a)
		{
			base.Copy(a);
			Name = ((UserStorage)a).Name;
			Password = ((UserStorage)a).Password;
			RefreshToken = ((UserStorage)a).RefreshToken;
			Expires = ((UserStorage)a).Expires;
			Rights = ((UserStorage)a).Rights;
		}
	}
}
