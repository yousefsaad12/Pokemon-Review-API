using Microsoft.EntityFrameworkCore;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class ReviewerRepo : IReviewerRepo
    {
        private readonly DataContext _context;
        public ReviewerRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReviewer(Reviewer reviewer)
        {
            await _context.AddAsync(reviewer);

            return await Save();
        }

        public async Task<bool> DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);

            return await Save();
        }

        public async Task<Reviewer?> GetReviewer(int id)
        {
            return await _context.Reviewers.Include(e => e.Reviews).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ICollection<Reviewer>> GetReviewers()
        {
            return await _context.Reviewers.ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsOfReviewer(int reviewerId)
        {
            return await _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToListAsync();
        }

        public async Task<bool> ReviewerExist(int id)
        {
            return await _context.Reviewers.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return await Save();
        }
    }
}
