using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewRepo 
    {
        Task<ICollection<Review>> GetReviews();
        Task<Review?> GetReview(int id);
        Task<ICollection<Review>> GetReviewsOfPokemon(int pokemonId);
        Task<bool> ReviewExists(int id);
        Task<bool> CreateReview(Review review);

        Task<bool> DeleteReview(Review review);

        Task<bool> DeleteReviews(List<Review> reviews);
        Task<bool> UpdateReview(Review review);
        Task<bool> Save();
    }
}
