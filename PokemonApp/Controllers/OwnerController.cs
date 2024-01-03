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
    public class OwnerController : Controller
    {
        private readonly IOwnerRepo _ownerRepo;
        private readonly ICountryRepo _countryRepo;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepo ownerRepo, IMapper mapper, ICountryRepo countryRepo)
        {
            _ownerRepo = ownerRepo;
            _mapper = mapper;
            _countryRepo = countryRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<ICollection<OwnerDTO>>(_ownerRepo.GetOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }


        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepo.OwnerExist(ownerId))
                return NotFound();

            var owner = _mapper.Map<OwnerDTO>(_ownerRepo.GetOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            if (!_ownerRepo.OwnerExist(ownerId))
                return NotFound();

            var pokemons = _mapper.Map<ICollection<PokemonDTO>>(_ownerRepo.GetPokemonsByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromQuery] int countryId ,[FromBody] OwnerDTO owner)
        {
            if (owner == null)
                return BadRequest(ModelState);

            var ownerExist = _ownerRepo.GetOwners()
                                          .Where(c => c.LastName.Trim().ToUpper() == owner.LastName.Trim().ToUpper())
                                          .FirstOrDefault();

            if (ownerExist != null)
            {
                ModelState.AddModelError("", "Country already exist");
                return StatusCode(422, ModelState);

            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(owner);

            ownerMap.Country = _countryRepo.GetCountry(countryId);

            if (!_ownerRepo.CreateOwner(ownerMap))
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
        public IActionResult UpdateCategory([FromQuery] int ownerId, [FromBody] OwnerDTO updatedOwner)
        {
            if (updatedOwner == null)
                return BadRequest(ModelState);

            if (ownerId != updatedOwner.Id)
                return BadRequest(ModelState);

            if (!_ownerRepo.OwnerExist(ownerId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(updatedOwner);

            if (!_ownerRepo.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }



        [HttpDelete("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int ownerId)
        {
            if (!_ownerRepo.OwnerExist(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = _ownerRepo.GetOwner(ownerId);

            if (!_ownerRepo.DeleteOwner(owner))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
