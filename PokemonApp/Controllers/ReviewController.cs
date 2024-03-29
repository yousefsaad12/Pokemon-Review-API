﻿using AutoMapper;
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
        public async Task<IActionResult>  GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(await _reviewRepo.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public async Task<IActionResult>  GetReview(int reviewId)
        {
            if (! await _reviewRepo.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDTO>(await _reviewRepo.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult>  GetReviewsOfPokemon(int pokemonId)
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(await _reviewRepo.GetReviewsOfPokemon(pokemonId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<IActionResult>  CreatePokemon([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDTO reviewDTO)
        {

            if (reviewDTO == null)
                return BadRequest(ModelState);

            var reviewList = await _reviewRepo.GetReviews();
                                           
            var reviewExist = reviewList.FirstOrDefault(p => p.Title.Trim().ToUpper() == reviewDTO.Title.Trim().ToUpper());

            if (reviewExist != null)
            {
                ModelState.AddModelError("", "Review already exist");
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewDTO);

            reviewMap.Pokemon = await _pokemonRepo.GetPokemon(pokemonId);
            reviewMap.Reviewer = await _reviewerRepo.GetReviewer(reviewerId);

            if (!await _reviewRepo.CreateReview(reviewMap))
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
        public async Task<IActionResult>  UpdateCategory(int reviewId, [FromBody] ReviewDTO updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (!await _reviewRepo.ReviewExists(reviewId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!await _reviewRepo.UpdateReview(reviewMap))
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
        public async Task<IActionResult>  DeleteCountry(int reviewId)
        {
            if (!await _reviewRepo.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewRepo.GetReview(reviewId);

            if (!await _reviewRepo.DeleteReview(review))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}

