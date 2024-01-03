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
        public bool CategoryExists(int catId)
        {
            return _context.Categories.Any(c => c.Id == catId);
        }

        public bool CreateCategory(Category category)
        {

            _context.Add(category);

            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int catId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == catId);
        }

        public IEnumerable<Pokemon> GetPokemonsByCategory(int id) // this part you do not finish by yourself
        {
            return _context.PokemonCategories.Where(c => c.CategoryId == id).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            
            return save > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
