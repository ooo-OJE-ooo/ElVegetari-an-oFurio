using ElVegetarioFurio.Models;
using ElVegetarioFurio.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace ElVegetarioFurio.Controllers 
{ 


        [Route("api/[controller]")]
        public class CategoriesController : Controller
        {
            private readonly ICategoryRepository _repository;
            private readonly string _path;

            public CategoriesController(ICategoryRepository repository, IWebHostEnvironment env)
            {
                _repository = repository;
                _path = Path.Combine(env.ContentRootPath, "data", "images", "categories");
            }

            // Methode
            [HttpGet]
            public IEnumerable<Category> Get()
            {
                return _repository.GetCategories();
            }

            // Einzelnen Datensetz lesen
            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var category = _repository.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }


            [HttpPost]
            public IActionResult Post([FromBody] Category category)
            {
                if (!ModelState.IsValid)
                {
                    // Status 400 - Bad request
                    return BadRequest(ModelState);
                }
                var result = _repository.CreateCategory(category);
                // Status 201 = CreatedAtAction
                // Neues anonymes Objekt anlegen um die ID zu übergeben
                return CreatedAtAction("Get", new { id = result.Id }, result);
            }

            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] Category category)
            {
                if (id != category.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (_repository.GetCategoryById(id) == null)
                {
                    return NotFound();
                }
                var result = _repository.UpdateCategory(category);
                return Ok(result);
            }

            [HttpDelete("{id}")]

            public IActionResult Delete(int id)
            {
                if (_repository.GetCategoryById(id) == null)
                {
                    return NotFound();
                }

                _repository.DeleteCategory(id);
                return NoContent();
            }

            [HttpGet("{id}/image")]

            public IActionResult Image(int id)
            {
                // Die Bilder sind nach den Id der Gerichte benannt
                var file = System.IO.Path.Combine(_path, $"{id}.jpg");
                if (System.IO.File.Exists(file))
                {
                    var bytes = System.IO.File.ReadAllBytes(file);
                    return File(bytes, "image/jpeg");
                }
                return NotFound();
            }
        }
}
