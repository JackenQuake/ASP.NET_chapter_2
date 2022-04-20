using Lesson4.Models.Data;
using Lesson4.Models.Interfaces;
using Lesson4.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Lesson4.Controllers
{
	/// <summary>
	/// Контроллер для управления информацией о клиентах
	/// </summary>
	[ApiController]
	[Route("/")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersModel model;
		private readonly ILogger<UsersController> logger;

		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public UsersController(IUsersModel model, ILogger<UsersController> logger)
		{
			this.model = model;
			this.logger = logger;
			logger.LogDebug("Users controller: created.");
		}

		/// <summary>
		/// Получает информацию о человеке по идентификатору
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /users/{id}
		/// </remarks>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Информация об указанном человеке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если ключ в базе данных отсутствует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpGet("/users/{id}")]
		public IActionResult GetById([FromRoute] int id)
		{
			logger.LogDebug($"Users controller: get user {id}.");
			try
			{
				var data = model.GetById(id);
				return Ok(data);
			} catch (RecordNotFoundException)
			{
				return NotFound($"User {id} does not exist in database.");
			} catch (Exception e)
			{
				logger.LogError($"Users controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных) с пагинацией
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /users/?skip={5}&amp;take={10}
		/// </remarks>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список записей из базы данных</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpGet("/users/")]
		public IActionResult GetAll(int skip, int take)
		{
			logger.LogDebug($"Users controller: list users for range {skip}, {take}.");
			try
			{
				var data = model.GetAll(skip, take);
				return Ok(data);
			} catch (Exception e)
			{
				logger.LogError($"Users controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /users/search?searchTerm={term}&amp;skip={5}&amp;take={10}
		/// </remarks>
		/// <param name="searchTerm">Слово для поиска в именах</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpGet("/users/search")]
		public IActionResult GetByName(string searchTerm, int skip, int take)
		{
			if (searchTerm != null)
			{
				logger.LogDebug($"Users controller: list users by name {searchTerm} for range {skip}, {take}.");
				try
				{
					var data = model.GetByName(searchTerm, skip, take);
					return Ok(data);
				} catch (Exception e)
				{
					logger.LogError($"Users controller: data access exception {e}.");
					return StatusCode(500, "Failed processing request, check server log file.");
				}
			} else return BadRequest("Search term not specified");
		}

		/// <summary>
		/// Добавление новой персоны в коллекцию
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// POST /users
		/// </remarks>
		/// <param name="item">Информация о человеке</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="409">Если запись с таким ключом уже существует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpPost("/users")]
		public IActionResult Create([FromBody] PersonDto item)
		{
			logger.LogDebug($"Users controller: create user {item.Id}.");
			try
			{
				model.Create(item);
				return Ok($"User {item.Id} successfully created.");
			} catch (RecordAlreadyExistsException)
			{
				return Conflict($"User {item.Id} already exists.");
			} catch (Exception e)
			{
				logger.LogError($"Users controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Обновление существующей персоны в коллекции
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// PUT /users
		/// </remarks>
		/// <param name="item">Информация о человеке</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если обновляемая запись в базе данных отсутствует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpPut("/users")]
		public IActionResult Update([FromBody] PersonDto item)
		{
			logger.LogDebug($"Users controller: update user {item.Id}.");
			try
			{
				model.Update(item);
				return Ok($"User {item.Id} successfully updated.");
			} catch (RecordNotFoundException)
			{
				return NotFound($"User {item.Id} does not exist in database.");
			} catch (Exception e)
			{
				logger.LogError($"Users controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}

		/// <summary>
		/// Удаление персоны из коллекции
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// DELETE /users/{id}
		/// </remarks>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если ключ в базе данных отсутствует</response>
		/// <response code="500">Если произошла другая внутренняя ошибка</response>
		[HttpDelete("/users/{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			logger.LogDebug($"Users controller: delete user {id}.");
			try
			{
				model.Delete(id);
				return Ok($"User {id} successfully deleted.");
			} catch (RecordNotFoundException)
			{
				return NotFound($"User {id} does not exist in database.");
			} catch (Exception e)
			{
				logger.LogError($"Users controller: data access exception {e}.");
				return StatusCode(500, "Failed processing request, check server log file.");
			}
		}
	}
}
