using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepo _pokemonRepo;
        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;
        public PokemonController(IPokemonRepo pokemonRepo,IMapper mapper, IReviewRepo reviewRepo)
        {
            _pokemonRepo = pokemonRepo;
            _mapper = mapper;
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDTO>>(_pokemonRepo.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof (Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId) 
        {
            if(!_pokemonRepo.PokemonExists(pokeId)) 
                return NotFound();

            var Pokemon = _mapper.Map<PokemonDTO>(_pokemonRepo.GetPokemon(pokeId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonRating(int pokeId) 
        {
            if (!_pokemonRepo.PokemonExists(pokeId))
                return NotFound();

            var Rating = _pokemonRepo.GetPokemonRating(pokeId);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Rating);

        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId ,[FromBody] PokemonDTO pokemonDTO)
        {

            if(pokemonDTO == null)
                return BadRequest(ModelState);

            var pokemonExist = _pokemonRepo.GetPokemons()
                                           .Where(p => p.Name.Trim().ToUpper() == pokemonDTO.Name.Trim().ToUpper())
                                           .FirstOrDefault();

            if(pokemonExist != null)
            {
                ModelState.AddModelError("", "Pokemon already exist");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonDTO);

            if(!_pokemonRepo.CreatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully Created");

        }


        [HttpPut("{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int pokemonId, [FromBody] PokemonDTO updatedPokemon)
        {
            if (updatedPokemon == null)
                return BadRequest(ModelState);

            if (pokemonId != updatedPokemon.Id)
                return BadRequest(ModelState);

            if (!_pokemonRepo.PokemonExists(pokemonId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(updatedPokemon);

            if (!_pokemonRepo.UpdatePokemon(pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }



        [HttpDelete("{pokeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokeId)
        {
            if (!_pokemonRepo.PokemonExists(pokeId))
                return NotFound();

            var reviews = _reviewRepo.GetReviewsOfPokemon(pokeId);
            var pokemon = _pokemonRepo.GetPokemon(pokeId);

            if(!_reviewRepo.DeleteReviews(reviews.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pokemonRepo.DeletePokemon(pokemon))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
