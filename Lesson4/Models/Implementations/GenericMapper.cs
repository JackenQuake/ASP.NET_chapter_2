using AutoMapper;
using Lesson4.Models.Data;
using Lesson4.Models.Interfaces;
using Lesson4.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Lesson4.Models.Implementations
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
		protected readonly ILogger<GenericMapper<TDto, TStorage>> logger;
		protected readonly string title;

		/// <summary>
		/// ����������� ������; protected, ��������� ����� ������������ �� ��� �������������, � ��� ������ ��� ��������
		/// </summary>
		protected GenericMapper(IGenericRepository<TStorage> repository, IMapper mapper, ILogger<GenericMapper<TDto, TStorage>> logger, string title)
		{
			this.repository = repository;
			this.mapper = mapper;
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
		public void Create(TDto item)
		{
			logger.LogError($"{title}: create id {item.Id}.");
			repository.Create(mapper.Map<TStorage>(item));
		}

		/// <summary>
		/// ��������� ������������ ������ � ���� ������
		/// </summary>
		/// <param name="item">���������� ������</param>
		public void Update(TDto item)
		{
			logger.LogError($"{title}: update id {item.Id}.");
			repository.Update(mapper.Map<TStorage>(item));
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
