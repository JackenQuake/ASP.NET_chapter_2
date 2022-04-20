using Microsoft.Extensions.Logging;
using System;

namespace Lesson4.Repositories
{
	/// <summary>
	/// ����� �������� ��������� ������, ������������ ������ ��� ������������� �������� ���������� � ������ � ���
	/// </summary>
	public class RepositoryErrorHandler
	{
		protected readonly ILogger<RepositoryErrorHandler> logger;
		protected readonly string title;

		/// <summary>
		/// ����������� ������; protected, ��������� ����� ������������ �� ��� �������������, � ��� ������ ��� ��������
		/// </summary>
		protected RepositoryErrorHandler(ILogger<RepositoryErrorHandler> logger, string title)
		{
			this.logger = logger;
			this.title = title;
			logger.LogDebug($"{title}: created.");
		}

		/// <summary>
		/// ��������� � �������� � ��� ���������� "������ � ���� ������ �� �������"
		/// </summary>
		/// <param name="operation">��������, ��������� ����������</param>
		/// <param name="id">������������� (���� � ���� ������)</param>
		protected void ThrowNotFoundException(string operation, int id)
		{
			logger.LogError($"{title}: {operation}: record {id} not found.");
			throw new RecordNotFoundException($"Record {id} not found.");
		}

		/// <summary>
		/// ��������� � �������� � ��� ���������� "������ � ���� ������ ��� ����������"
		/// </summary>
		/// <param name="operation">��������, ��������� ����������</param>
		/// <param name="id">������������� (���� � ���� ������)</param>
		protected void ThrowAlreadyExistsException(string operation, int id)
		{
			logger.LogError($"{title}: {operation}: record {id} already exists.");
			throw new RecordAlreadyExistsException($"Record {id} not found.");
		}

		/// <summary>
		/// ��������� � �������� � ��� ���������� "������ ���������� ���� ������"
		/// </summary>
		/// <param name="operation">��������, ��������� ����������</param>
		/// <param name="id">������������� (���� � ���� ������)</param>
		protected void ThrowGenericException(string operation, int id)
		{
			logger.LogError($"{title}: {operation} record {id} unknown error.");
			throw new InvalidOperationException($"Database update error.");
		}
	}

	/// <summary>
	/// ���������� "������ � ���� ������ �� �������"
	/// </summary>
	public class RecordNotFoundException : ArgumentException
	{
		public RecordNotFoundException(string msg) : base(msg) { }
	}

	/// <summary>
	/// ���������� "������ � ���� ������ ��� ����������"
	/// </summary>
	public class RecordAlreadyExistsException : ArgumentException
	{
		public RecordAlreadyExistsException(string msg) : base(msg) { }
	}
}
