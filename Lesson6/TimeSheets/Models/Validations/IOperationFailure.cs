namespace Timesheets.Models.Validations
{
	/// <summary>
	/// Контракт описания ошибки валидации
	/// </summary>
	public interface IOperationFailure
	{
		string PropertyName { get; }
		string Description { get; }
		string Code { get; }
	}
}
