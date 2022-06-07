using System.Collections.Generic;
using Timesheets.Models.Data;
using Timesheets.Models.Validations;

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
		public IReadOnlyList<IOperationFailure> Create(TDto item);

		/// <summary>
		/// ��������� ������������ ������ � ���� ������
		/// </summary>
		/// <param name="item">���������� ������</param>
		public IReadOnlyList<IOperationFailure> Update(TDto item);

		/// <summary>
		/// �������� ������ �� ���� ������ �� �����
		/// </summary>
		/// <param name="id">������������� (���� � ���� ������)</param>
		public void Delete(int id);
	}
}
