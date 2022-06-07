using System.Collections.Generic;

namespace Timesheets.Models.Validations
{
	public interface IValidationService<TEntity> where TEntity : class
	{
		IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item);
	}
}
