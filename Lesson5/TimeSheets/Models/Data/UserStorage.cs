namespace Timesheets.Models.Data
{
	/// <summary>
	/// Хранимая в репозитории информация о пользователе
	/// </summary>
	public class UserStorage : GenericStorage
	{
		public string Name { get; set; }

		public string Password { get; set; }

		public string RefreshToken { get; set; }

		public long Expires { get; set; }

		public int Rights { get; set; }

		// Rights - поле флагов прав доступа со следующими значениями
		public const int MaskAdministrator  = 0x0001;  // Администратор, может изменять все
		public const int MaskChangePassword = 0x0002;  // Имеет право менять свой пароль

		public bool IsAdministrator() => ((Rights & MaskAdministrator) != 0);

		public bool CanChangePassword() => ((Rights & MaskChangePassword) != 0);

		/// <summary>
		/// Копирует содержимое другого экземпляра этого же класса. Требуется для обновления репозиториев
		/// <param name="a">Объект, который требуется скопировать</param>
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
