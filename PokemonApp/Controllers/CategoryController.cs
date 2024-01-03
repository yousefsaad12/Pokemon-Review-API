using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
    
            private readonly ICategoryRepo _categoryRepo;
            private readonly IMapper _mapper;
            public CategoryController(ICategoryRepo categoryRepo, IMapper mapper)
            {
                _categoryRepo = categoryRepo;
                _mapper = mapper;
            }

            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
            public IActionResult GetCategoreis()
            {
                var categories = _mapper.Map<List<CategoryDTO>>(_categoryRepo.GetCategories());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(categories);
            }

            [HttpGet("{categoryId}")]
            [ProducesResponseType(200, Type = typeof(Category))]
            [ProducesResponseType(400)]
            public IActionResult GetCategory(int categoryId)
            {
                if (!_categoryRepo.CategoryExists(categoryId))
                    return NotFound();

                var category = _mapper.Map<CategoryDTO>(_categoryRepo.GetCategory(categoryId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(category);
            }

            [HttpGet("pokemon/{categoryId}")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
            [ProducesResponseType(400)]
            public IActionResult GetPokemonsByCategory(int categoryId)
            {
                if (!_categoryRepo.CategoryExists(categoryId))
                    return NotFound();

                var pokemons = _mapper.Map<List<PokemonDTO>>(_categoryRepo.GetPokemonsByCategory(categoryId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(pokemons);
            }

            [HttpPost]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreateCategory([FromBody]CategoryDTO category) 
            {
                if (category == null)
                    return BadRequest(ModelState);

                var categoryExist = _categoryRepo.GetCategories()
                                              .Where(c => c.Name.Trim().ToUpper() == category.Name.Trim().ToUpper())
                                              .FirstOrDefault();

                if (categoryExist != null)
                {
                    ModelState.AddModelError("", "Category already exist");
                    return StatusCode(422 ,ModelState);

                }

                if(!ModelState.IsValid) 
                    return BadRequest(ModelState);

                var categoryMap = _mapper.Map<Category>(category);

                if(!_categoryRepo.CreateCategory(categoryMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }


                return Ok("Successfully Created");

            }



            [HttpPut]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            [ProducesResponseType(404)]
            public IActionResult UpdateCategory([FromQuery] int categoryId, [FromBody] CategoryDTO updatedCategory)
            {
                if (updatedCategory == null)
                    return BadRequest(ModelState);

                if(categoryId != updatedCategory.Id)
                    return BadRequest(ModelState);

                if(!_categoryRepo.CategoryExists(categoryId))
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var categoryMap = _mapper.Map<Category>(updatedCategory);

                if (!_categoryRepo.UpdateCategory(categoryMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return NoContent();

            }


            [HttpDelete("{categoryId}")]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            [ProducesResponseType(404)]
            public IActionResult DeleteCategory(int categoryId) 
            {
                if(!_categoryRepo.CategoryExists(categoryId))
                    return NotFound();

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var category = _categoryRepo.GetCategory(categoryId);

                if(!_categoryRepo.DeleteCategory(category))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting");
                    return StatusCode(500, ModelState);
                }


                return NoContent();
            }


    }
}
