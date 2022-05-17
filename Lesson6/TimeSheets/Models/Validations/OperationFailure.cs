namespace Timesheets.Models.Validations
{
	/// <summary>
	/// Описание ошибки валидации
	/// </summary>
	public sealed class OperationFailure : IOperationFailure
	{
		public string PropertyName { get; set; }
		public string Description { get; set; }
		public string Code { get; set; }
	}
}
