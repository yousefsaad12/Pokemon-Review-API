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
    public class ReviewController : Controller
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IReviewerRepo _reviewerRepo;
        private readonly IPokemonRepo _pokemonRepo;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepo reviewRepo, IReviewerRepo reviewerRepo, IPokemonRepo pokemonRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _reviewerRepo = reviewerRepo;
            _pokemonRepo = pokemonRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepo.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepo.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDTO>(_reviewRepo.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfPokemon(int pokemonId)
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepo.GetReviewsOfPokemon(pokemonId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreatePokemon([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDTO reviewDTO)
        {

            if (reviewDTO == null)
                return BadRequest(ModelState);

            var reviewExist = _reviewRepo.GetReviews()
                                           .Where(p => p.Title.Trim().ToUpper() == reviewDTO.Title.Trim().ToUpper())
                                           .FirstOrDefault();

            if (reviewExist != null)
            {
                ModelState.AddModelError("", "Review already exist");
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewDTO);

            reviewMap.Pokemon = _pokemonRepo.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewerRepo.GetReviewer(reviewerId);

            if (!_reviewRepo.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully Created");

        }


        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int reviewId, [FromBody] ReviewDTO updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepo.ReviewExists(reviewId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepo.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int reviewId)
        {
            if (!_reviewRepo.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = _reviewRepo.GetReview(reviewId);

            if (!_reviewRepo.DeleteReview(review))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}

