using Lesson4.Models.Data;
using Lesson4.Models.Interfaces;
using Lesson4.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Lesson4.Controllers
{
	/// <summary>
	/// Контроллер для управления информацией о сотрудниках
	/// </summary>
	[ApiController]
	[Route("/")]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeesModel model;
		private readonly ILogger<EmployeesController> logger;

		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public EmployeesController(IEmployeesModel model, ILogger<EmployeesController> logger)
		{
			this.model = model;
			this.logger = logger;
			logger.LogDebug("Employees controller: created.");
		}

		/// <summary>
		/// Получает информацию о человеке по идентификатору
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /employees/{id}
		/// </remarks>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Информация об указанном человеке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если ключ в базе данных отсутствует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpGet("/employees/{id}")]
		public IActionResult GetById([FromRoute] int id)
		{
			logger.LogDebug($"Employees controller: get employee {id}.");
			try
			{
				var data = model.GetById(id);
				return Ok(data);
			} catch (RecordNotFoundException)
			{
				return NotFound($"Employee {id} does not exist in database.");
			} catch (Exception e)
			{
				logger.LogError($"Employees controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных) с пагинацией
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /employees/?skip={5}&amp;take={10}
		/// </remarks>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список записей из базы данных</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpGet("/employees/")]
		public IActionResult GetAll(int skip, int take)
		{
			logger.LogDebug($"Employees controller: list employees for range {skip}, {take}.");
			try
			{
				var data = model.GetAll(skip, take);
				return Ok(data);
			} catch (Exception e)
			{
				logger.LogError($"Employees controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /employees/search?searchTerm={term}&amp;skip={5}&amp;take={10}
		/// </remarks>
		/// <param name="searchTerm">Слово для поиска в именах</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpGet("/employees/search")]
		public IActionResult GetByName(string searchTerm, int skip, int take)
		{
			if (searchTerm != null)
			{
				logger.LogDebug($"Employees controller: list employees by name {searchTerm} for range {skip}, {take}.");
				try
				{
					var data = model.GetByName(searchTerm, skip, take);
					return Ok(data);
				} catch (Exception e)
				{
					logger.LogError($"Employees controller: data access exception {e}.");
					return StatusCode(500, "Failed processing request, check server log file.");
				}
			} else return BadRequest("Search term not specified");
		}

		/// <summary>
		/// Добавление новой персоны в коллекцию
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// POST /employees
		/// </remarks>
		/// <param name="item">Информация о человеке</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="409">Если запись с таким ключом уже существует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpPost("/employees")]
		public IActionResult Create([FromBody] PersonDto item)
		{
			logger.LogDebug($"Employees controller: create employee {item.Id}.");
			try
			{
				model.Create(item);
				return Ok($"Employee {item.Id} successfully created.");
			} catch (RecordAlreadyExistsException)
			{
				return Conflict($"Employee {item.Id} already exists.");
			} catch (Exception e)
			{
				logger.LogError($"Employees controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Обновление существующей персоны в коллекции
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// PUT /employees
		/// </remarks>
		/// <param name="item">Информация о человеке</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если обновляемая запись в базе данных отсутствует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpPut("/employees")]
		public IActionResult Update([FromBody] PersonDto item)
		{
			logger.LogDebug($"Employees controller: update employee {item.Id}.");
			try
			{
				model.Update(item);
				return Ok($"Employee {item.Id} successfully updated.");
			} catch (RecordNotFoundException)
			{
				return NotFound($"Employee {item.Id} does not exist in database.");
			} catch (Exception e)
			{
				logger.LogError($"Employees controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Удаление персоны из коллекции
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// DELETE /employees/{id}
		/// </remarks>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если ключ в базе данных отсутствует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpDelete("/employees/{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			logger.LogDebug($"Employees controller: delete employee {id}.");
			try
			{
				model.Delete(id);
				return Ok($"Employee {id} successfully deleted.");
			} catch (RecordNotFoundException)
			{
				return NotFound($"Employee {id} does not exist in database.");
			} catch (Exception e)
			{
				logger.LogError($"Employees controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}
	}
}
