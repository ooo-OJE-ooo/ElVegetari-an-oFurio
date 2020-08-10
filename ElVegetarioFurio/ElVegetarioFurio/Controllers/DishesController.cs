using ElVegetarioFurio.Models;
using ElVegetarioFurio.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElVegetarioFurio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IDishRepository _repository;

        public DishesController(IDishRepository repository)
        {
            _repository = repository;
        }

        // Methode
        [HttpGet]
        public IEnumerable<Dish> Get()
        {
            return _repository.GetDishes();
        }

        // Einzelnen Datensetz lesen
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dish = _repository.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Dish dish)
        {
            if (!ModelState.IsValid)
            {
                // Status 400 - Bad request
                return BadRequest(ModelState);
            }
            var result = _repository.CreateDish(dish);
            // Status 201 = CreatedAtAction
            // Neues anonymes Objekt anlegen um die ID zu übergeben
            return CreatedAtAction("Get", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Dish dish)
        {
            if(id != dish.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_repository.GetDishById(id) == null)
            {
                return NotFound();
            }
            var result = _repository.UpdateDish(dish);
            return Ok(result);
        }
   
        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            if(_repository.GetDishById(id) == null)
            {
                return NotFound();
            }

            _repository.DeleteDish(id);
            return NoContent();
        }
    }
}
