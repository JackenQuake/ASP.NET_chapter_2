namespace Timesheets.Models.Validations
{
	/// <summary>
	/// �������� �������� ������ ���������
	/// </summary>
	public interface IOperationFailure
	{
		string PropertyName { get; }
		string Description { get; }
		string Code { get; }
	}
}
