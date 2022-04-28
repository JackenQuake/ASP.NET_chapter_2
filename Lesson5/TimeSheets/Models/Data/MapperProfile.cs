using AutoMapper;

namespace Timesheets.Models.Data
{
	/// <summary>
	/// Профили для преобразования данных между уровнями MVC через AutoMapper
	/// </summary>
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<PersonStorage, PersonDto>();
			CreateMap<PersonDto, PersonStorage>();
		}
	}
}
