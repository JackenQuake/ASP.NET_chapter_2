using Lesson3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Lesson3.Controllers
{
	/// <summary>
	/// Контроллер для управления информацией о людях
	/// </summary>
	[ApiController]
	[Route("/")]
	public class PersonsController : ControllerBase
	{
		private readonly IPersonsModel model;
		private readonly ILogger<PersonsController> logger;

		/// <summary>
		/// Конструктор класса с поддержкой Dependency Injection
		/// </summary>
		public PersonsController(IPersonsModel model, ILogger<PersonsController> logger)
		{
			this.model = model;
			this.logger = logger;
			logger.LogDebug("Persons controller: created.");
		}

		/// <summary>
		/// Получает информацию о человеке по идентификатору
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /persons/{id}
		/// </remarks>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Информация об указанном человеке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если ключ в базе данных отсутствует</response>
		[HttpGet("/persons/{id}")]
		public IActionResult GetById([FromRoute] int id)
		{
			logger.LogDebug($"Persons controller: get person {id}.");
			try
			{
				var data = model.GetById(id);
				return Ok(data);
			}
			catch (ArgumentException)
			{
				return NotFound($"Person {id} does not exist in database.");
			}
		}

		/// <summary>
		/// Получение списка людей (выборки из базы данных) с пагинацией
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /persons/?skip={5}&amp;take={10}
		/// </remarks>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список записей из базы данных</returns>
		/// <response code="200">Если всё хорошо</response>
		[HttpGet("/persons/")]
		public IActionResult GetAll(int skip, int take)
		{
			logger.LogDebug($"Persons controller: list persons for range {skip}, {take}.");
			var data = model.GetAll(skip, take);
			return Ok(data);
		}

		/// <summary>
		/// Поиск человека по имени с пагинацией
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// GET /persons/search?searchTerm={term}&amp;skip={5}&amp;take={10}
		/// </remarks>
		/// <param name="term">Слово для поиска в именах</param>
		/// <param name="skip">Количество записей, которые требуется пропустить в начале (по умолчанию 0)</param>
		/// <param name="take">Количество записей, которые после этого требуется вернуть (по умолчанию все)</param>
		/// <returns>Список людей, чье имя или фамилия содержат указанное слово</returns>
		/// <response code="200">Если всё хорошо</response>
		[HttpGet("/persons/search")]
		public IActionResult GetByName(string searchTerm, int skip, int take)
		{
			if (searchTerm != null)
			{
				logger.LogDebug($"Persons controller: list persons by name {searchTerm} for range {skip}, {take}.");
				var data = model.GetByName(searchTerm, skip, take);
				return Ok(data);
			}
			else return BadRequest("Search term not specified");
		}

		/// <summary>
		/// Добавление новой персоны в коллекцию
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// POST /persons
		/// </remarks>
		/// <param name="item">Информация о человеке</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="409">Если запись с таким ключом уже существует</response>
		[HttpPost("/persons")]
		public IActionResult Create([FromBody] PersonDto item)
		{
			logger.LogDebug($"Persons controller: create person {item.Id}.");
			try
			{
				model.Create(item);
				return Ok($"Person {item.Id} successfully created.");
			}
			catch (ArgumentException)
			{
				return Conflict($"Person {item.Id} already exists.");
			}
		}

		/// <summary>
		/// Обновление существующей персоны в коллекции
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// PUT /persons
		/// </remarks>
		/// <param name="item">Информация о человеке</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если обновляемая запись в базе данных отсутствует</response>
		[HttpPut("/persons")]
		public IActionResult Update([FromBody] PersonDto item)
		{
			logger.LogDebug($"Persons controller: update person {item.Id}.");
			try
			{
				model.Update(item);
				return Ok($"Person {item.Id} successfully updated.");
			}
			catch (ArgumentException)
			{
				return NotFound($"Person {item.Id} does not exist in database.");
			}
		}

		/// <summary>
		/// Удаление персоны из коллекции
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		/// DELETE /persons/{id}
		/// </remarks>
		/// <param name="id">Идентификатор (ключ в базе данных)</param>
		/// <returns>Подтверждение или сообщение об ошибке</returns>
		/// <response code="200">Если всё хорошо</response>
		/// <response code="404">Если ключ в базе данных отсутствует</response>
		[HttpDelete("/persons/{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			logger.LogDebug($"Persons controller: delete person {id}.");
			try
			{
				model.Delete(id);
				return Ok($"Person {id} successfully deleted.");
			}
			catch (ArgumentException)
			{
				return NotFound($"Person {id} does not exist in database.");
			}
		}
	}
}
