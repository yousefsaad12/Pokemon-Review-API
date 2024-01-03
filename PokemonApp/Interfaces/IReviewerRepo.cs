using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewerRepo
    {
        bool ReviewerExist(int  id);
        ICollection<Reviewer> GetReviewers();
        ICollection<Review> GetReviewsOfReviewer(int id);
        Reviewer GetReviewer(int id);
        bool CreateReviewer(Reviewer reviewer);

        bool DeleteReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool Save();
    }
}
