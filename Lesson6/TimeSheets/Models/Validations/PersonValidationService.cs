using FluentValidation;
using FluentValidation.Results;
using System.Linq;
using Timesheets.Models.Data;

namespace Timesheets.Models.Validations
{
	internal sealed class PersonValidationService : FluentValidationService<PersonDto>, IPersonValidationService
	{
		public PersonValidationService()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("Имя не должно быть пустым").WithErrorCode("BRL-100.1")
				.Must(x => x.All(char.IsLetter)).WithMessage("Имя должно состоять только из букв").WithErrorCode("BRL-100.2");
			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Фамилия не должна быть пустой").WithErrorCode("BRL-100.3")
				.Must(x => x.All(char.IsLetter)).WithMessage("Фамилия должна состоять только из букв").WithErrorCode("BRL-100.4");
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("E-mail не должен быть пустым").WithErrorCode("BRL-100.5")
				.EmailAddress().WithMessage("Указан некорректный E-mail").WithErrorCode("BRL-100.6");
			RuleFor(x => x.Company)
				.NotEmpty().WithMessage("Название компании не должно быть пустым").WithErrorCode("BRL-100.7")
				.Must(x => x.All(char.IsLetter)).WithMessage("Название компании должно состоять только из букв").WithErrorCode("BRL-100.8");
			RuleFor(x => x.Age)
				.GreaterThan(17).WithMessage("Возраст не должен быть меньше 18").WithErrorCode("BRL-100.9")
				.LessThan(100).WithMessage("Возраст не должен быть больше 99").WithErrorCode("BRL-100.10");
		}
	}
}
