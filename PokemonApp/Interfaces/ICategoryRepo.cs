using PokemonApp.Data;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICategoryRepo
    {   
        public IEnumerable<Category> GetCategories();
        public Category GetCategory(int id);
        public IEnumerable<Pokemon> GetPokemonsByCategory(int id);
        bool CategoryExists(int id);
        bool UpdateCategory(Category category);        
        bool CreateCategory(Category category);

        bool DeleteCategory(Category category);
        bool Save();

    }
}
