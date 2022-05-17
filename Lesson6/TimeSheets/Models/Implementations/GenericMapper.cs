using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Timesheets.Models.Data;
using Timesheets.Models.Interfaces;
using Timesheets.Models.Validations;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Models.Implementations
{
	/// <summary>
	/// ������������� ������� ��� AutoMapper ��� �������� � ����� ������
	/// ������ ����������� � ���������� �������, ����������� ������ CRUD ��������
	/// �������� protected ����� MassMap, �� ��������� �������� ������ ������� ������ Get � ������� ���������
	/// </summary>
	public class GenericMapper<TDto, TStorage> : IGenericMapper<TDto, TStorage> where TDto : GenericDto where TStorage : GenericStorage
	{
		protected readonly IGenericRepository<TStorage> repository;
		protected readonly IMapper mapper;
		protected readonly IValidationService<TDto> validationService;
		protected readonly ILogger<GenericMapper<TDto, TStorage>> logger;
		protected readonly string title;

		/// <summary>
		/// ����������� ������; protected, ��������� ����� ������������ �� ��� �������������, � ��� ������ ��� ��������
		/// </summary>
		protected GenericMapper(IGenericRepository<TStorage> repository, IMapper mapper, IValidationService<TDto> validationService, ILogger<GenericMapper<TDto, TStorage>> logger, string title)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.validationService = validationService;
			this.logger = logger;
			this.title = title;
			logger.LogDebug($"{title}: created.");
		}

		/// <summary>
		/// ����������� ���������� �� ���� ������� � ������ �������� TDto
		/// </summary>
		/// <param name="data">IEnumerable, �������������� ����� ������� �� ���� ������</param>
		/// <returns>������ ������� �� ���� ������</returns>
		protected IReadOnlyCollection<TDto> MassMap(IEnumerable<TStorage> data)
		{
			var result = new List<TDto>();
			foreach (TStorage e in data) result.Add(mapper.Map<TDto>(e));
			return result.AsReadOnly();
		}

		/// <summary>
		/// ��������� ������ �� ���� ������ �� �����
		/// </summary>
		/// <param name="id">������������� (���� � ���� ������)</param>
		/// <returns>����������� ������</returns>
		public TDto GetById(int id)
		{
			logger.LogDebug($"{title}: get person {id}.");
			return mapper.Map<TDto>(repository.GetById(id));
		}

		/// <summary>
		/// ���������� ����� ������ � ���� ������
		/// </summary>
		/// <param name="item">����������� ������</param>
		/// <returns>������ ������ ���������</returns>
		public IReadOnlyList<IOperationFailure> Create(TDto item)
		{
			logger.LogError($"{title}: create id {item.Id}.");
			var validity = validationService.ValidateEntity(item);
			if (validity.Count == 0) repository.Create(mapper.Map<TStorage>(item));
			return validity;
		}

		/// <summary>
		/// ��������� ������������ ������ � ���� ������
		/// </summary>
		/// <param name="item">���������� ������</param>
		/// <returns>������ ������ ���������</returns>
		public IReadOnlyList<IOperationFailure> Update(TDto item)
		{
			logger.LogError($"{title}: update id {item.Id}.");
			var validity = validationService.ValidateEntity(item);
			if (validity.Count == 0) repository.Update(mapper.Map<TStorage>(item));
			return validity;
		}

		/// <summary>
		/// �������� ������ �� ���� ������ �� �����
		/// </summary>
		/// <param name="id">������������� (���� � ���� ������)</param>
		public void Delete(int id)
		{
			logger.LogError($"{title}: delete id {id}.");
			repository.Delete(id);
		}
	}
}
