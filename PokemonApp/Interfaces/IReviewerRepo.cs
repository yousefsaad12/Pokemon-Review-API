using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewerRepo
    {
        Task<bool> ReviewerExist(int  id);
        Task<ICollection<Reviewer>> GetReviewers();
        Task<ICollection<Review>> GetReviewsOfReviewer(int id);
        Task<Reviewer?> GetReviewer(int id);
        Task<bool> CreateReviewer(Reviewer reviewer);

        Task<bool> DeleteReviewer(Reviewer reviewer);
        Task<bool> UpdateReviewer(Reviewer reviewer);
        Task<bool> Save();
    }
}
