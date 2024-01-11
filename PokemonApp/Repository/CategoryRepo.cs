using Microsoft.EntityFrameworkCore;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DataContext _context;

        public CategoryRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CategoryExists(int catId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == catId);
        }

        public async Task<bool> CreateCategory(Category category)
        {

            await _context.AddAsync(category);

            return await Save();
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Remove(category);
            return await Save();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategory(int catId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == catId);
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsByCategory(int id) // this part you do not finish by yourself
        {
            return await _context.PokemonCategories.Where(c => c.CategoryId == id).Select(c => c.Pokemon).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            
            return save > 0 ? true : false;
        }

        public Task<bool> UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
