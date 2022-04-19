using AutoMapper;

namespace Lesson3.Models
{
	/// <summary>
	/// Профили для преобразования данных между уровнями MVC через AutoMapper
	/// </summary>
	public class MapperProfile : Profile
	{
		public  MapperProfile()
		{
			CreateMap<PersonStorage, PersonDto>();
			CreateMap<PersonDto, PersonStorage>();
		}
	}
}
