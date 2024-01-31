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
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepo _reviewerRepo;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerRepo reviewerRepo, IMapper mapper)
        {
            _reviewerRepo = reviewerRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        public async Task<IActionResult> GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDTO>>(await _reviewerRepo.GetReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReview(int reviewerId)
        {
            if (!await _reviewerRepo.ReviewerExist(reviewerId))
                return NotFound();

            var reviewer = _mapper.Map<ReviewerDTO>(_reviewerRepo.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsOfPokemon(int reviewerId)
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(await _reviewerRepo.GetReviewsOfReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreatePokemon([FromBody] ReviewerDTO reviewerDTO)
        {

            if (reviewerDTO == null)
                return BadRequest(ModelState);

            var reviewerList = await _reviewerRepo.GetReviewers();
                                           
            var reviewerExist = reviewerList .FirstOrDefault(p => p.LastName.Trim().ToUpper() == reviewerDTO.LastName.Trim().ToUpper());

            if (reviewerExist != null)
            {
                ModelState.AddModelError("", "Review already exist");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewerDTO);

            if (!await _reviewerRepo.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully Created");

        }


        [HttpPut("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategory(int reviewerId, [FromBody] ReviewerDTO updatedReviewer)
        {
            if (updatedReviewer == null)
                return BadRequest(ModelState);

            if (reviewerId != updatedReviewer.Id)
                return BadRequest(ModelState);

            if (!await _reviewerRepo.ReviewerExist(reviewerId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

            if (!await _reviewerRepo.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }



        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCountry(int reviewerId)
        {
            if (!await _reviewerRepo.ReviewerExist(reviewerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewerRepo.GetReviewer(reviewerId);

            if (!await _reviewerRepo.DeleteReviewer(review))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
