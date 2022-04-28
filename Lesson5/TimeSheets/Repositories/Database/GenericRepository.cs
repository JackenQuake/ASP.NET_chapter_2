using Microsoft.Extensions.Logging;
using Timesheets.Models.Data;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.Database
{
	/// <summary>
	/// ������������� ����������� (���������� �� ������ Entity Framework)
	/// </summary>
	public class GenericRepository<T, DB> : RepositoryErrorHandler, IGenericRepository<T> where T : GenericStorage where DB : T, new()
	{
		// ���������������� ������ ��� ����������� � �������, ����� ��� ���� ����������� GenericRepository
		protected const string ConnectionString = "Host=127.0.0.1;Database=Timesheets;Username=GeekBrains;Password=gb9452;";
		protected readonly GenericDbContext<DB> context;

		/// <summary>
		/// ����������� ������; protected, ��������� ����� ������������ �� ��� �������������, � ��� ������ ��� ��������
		/// </summary>
		protected GenericRepository(ILogger<GenericRepository<T, DB>> logger, string title) : base(logger, title)
		{
			context = new GenericDbContext<DB>(ConnectionString);
		}

		/// <summary>
		/// ��������� ������ �� ���� ������ �� �����
		/// </summary>
		/// <param name="id">������������� (���� � ���� ������)</param>
		/// <returns>����������� ������</returns>
		public virtual T GetById(int id)
		{
			logger.LogDebug($"{title}: get id {id}.");
			DB record = context.Base.Find(id);
			if (record == null) ThrowNotFoundException("get", id);
			return record;
		}

		/// <summary>
		/// ���������� ����� ������ � ���� ������
		/// </summary>
		/// <param name="item">����������� ������</param>
		public virtual void Create(T item)
		{
			logger.LogDebug($"{title}: create id {item.Id}.");
			DB record = context.Base.Find(item.Id);
			if (record != null) ThrowAlreadyExistsException("create", item.Id);
			record = new();
			record.Copy(item);
			context.Add(record);
			if (context.SaveChanges() == 0) ThrowGenericException("create", item.Id);
		}

		/// <summary>
		/// ��������� ������������ ������ � ���� ������
		/// </summary>
		/// <param name="item">���������� ������</param>
		public virtual void Update(T item)
		{
			logger.LogDebug($"{title}: update id {item.Id}.");
			DB record = context.Base.Find(item.Id);
			if (record == null) ThrowNotFoundException("update", item.Id);
			record.Copy(item);
			if (context.SaveChanges() == 0) ThrowGenericException("update", item.Id);
		}

		/// <summary>
		/// �������� ������ �� ���� ������ �� �����
		/// </summary>
		/// <param name="id">������������� (���� � ���� ������)</param>
		public virtual void Delete(int id)
		{
			logger.LogDebug($"{title}: delete id {id}.");
			DB record = context.Base.Find(id);
			if (record == null) ThrowNotFoundException("delete", id);
			context.Base.Remove(record);
			if (context.SaveChanges() == 0) ThrowGenericException("delete", id);
		}
	}
}
