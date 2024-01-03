using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewRepo 
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfPokemon(int pokemonId);
        bool ReviewExists(int id);
        bool CreateReview(Review review);

        bool DeleteReview(Review review);

        bool DeleteReviews(List<Review> reviews);
        bool UpdateReview(Review review);
        bool Save();
    }
}
