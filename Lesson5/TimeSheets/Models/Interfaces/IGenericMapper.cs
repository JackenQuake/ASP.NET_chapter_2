using Timesheets.Models.Data;

namespace Timesheets.Models.Interfaces
{
	/// <summary>
	/// ������������� ������� ��� AutoMapper ��� �������� � ����� ������
	/// </summary>
	public interface IGenericMapper<TDto, TStorage> where TDto : GenericDto where TStorage : GenericStorage
	{
		/// <summary>
		/// ��������� ������ �� ���� ������ �� �����
		/// </summary>
		/// <param name="id">������������� (���� � ���� ������)</param>
		/// <returns>����������� ������</returns>
		public TDto GetById(int id);

		/// <summary>
		/// ���������� ����� ������ � ���� ������
		/// </summary>
		/// <param name="item">����������� ������</param>
		public void Create(TDto item);

		/// <summary>
		/// ��������� ������������ ������ � ���� ������
		/// </summary>
		/// <param name="item">���������� ������</param>
		public void Update(TDto item);

		/// <summary>
		/// �������� ������ �� ���� ������ �� �����
		/// </summary>
		/// <param name="id">������������� (���� � ���� ������)</param>
		public void Delete(int id);
	}
}
