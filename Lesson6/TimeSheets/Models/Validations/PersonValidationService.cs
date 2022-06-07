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
				.NotEmpty().WithMessage("��� �� ������ ���� ������").WithErrorCode("BRL-100.1")
				.Must(x => x.All(char.IsLetter)).WithMessage("��� ������ �������� ������ �� ����").WithErrorCode("BRL-100.2");
			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("������� �� ������ ���� ������").WithErrorCode("BRL-100.3")
				.Must(x => x.All(char.IsLetter)).WithMessage("������� ������ �������� ������ �� ����").WithErrorCode("BRL-100.4");
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("E-mail �� ������ ���� ������").WithErrorCode("BRL-100.5")
				.EmailAddress().WithMessage("������ ������������ E-mail").WithErrorCode("BRL-100.6");
			RuleFor(x => x.Company)
				.NotEmpty().WithMessage("�������� �������� �� ������ ���� ������").WithErrorCode("BRL-100.7")
				.Must(x => x.All(char.IsLetter)).WithMessage("�������� �������� ������ �������� ������ �� ����").WithErrorCode("BRL-100.8");
			RuleFor(x => x.Age)
				.GreaterThan(17).WithMessage("������� �� ������ ���� ������ 18").WithErrorCode("BRL-100.9")
				.LessThan(100).WithMessage("������� �� ������ ���� ������ 99").WithErrorCode("BRL-100.10");
		}
	}
}
