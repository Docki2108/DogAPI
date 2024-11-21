using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestAPI.DB;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("dogs")]
    public class DogController : ControllerBase
    {
        private readonly DBContext _context;

        public DogController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Показать всех собак",
            Description = "Запрос возвращает список всех собак, сохраненных в базе данных."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllDogs()
        {
            var dogs = _context.Dogs.ToList();
            return Ok(dogs);
        }

        [HttpGet("view/{id}")]
        [SwaggerOperation(
            Summary = "Показать определенную собаку",
            Description = "Запрос возвращает собаку по ID, если она существует."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDog(int id)
        {
            var dog = _context.Dogs.Find(id);
            if (dog == null) return NotFound();
            return Ok(dog);
        }

        [HttpPost("post/")]
        [SwaggerOperation(
            Summary = "Создать собаку",
            Description = "Запрос создает собаку с заданными параметрами."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateDog([FromBody] Dog dog)
        {
            if (dog == null)
            {
                return BadRequest("Объект Dog пустой.");
            }

            _context.Dogs.Add(dog);
            try { _context.SaveChanges(); }
            catch { BadRequest("ID собаки повторяется"); }
            return CreatedAtAction(nameof(GetDog), new { id = dog.ID }, dog);
        }

        [HttpPut("put/{id}")]
        [SwaggerOperation(
            Summary = "Изменить значения собаки",
            Description = "Запрос меняет параметры собаки."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateDog(int id, [FromBody] Dog dog)
        {
            if (id != dog.ID) return BadRequest("Собака по ID не найдена.");

            var existingDog = _context.Dogs.Find(id);
            if (existingDog == null) return NotFound();

            existingDog.Name = dog.Name;
            existingDog.Age = dog.Age;
            existingDog.Weight = dog.Weight;
            existingDog.Speed = dog.Speed;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [SwaggerOperation(
            Summary = "Удалить собаку",
            Description = "Запрос удаляет собаку по ID."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDog(int id)
        {
            var dog = _context.Dogs.Find(id);
            if (dog == null) return NotFound();

            _context.Dogs.Remove(dog);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
