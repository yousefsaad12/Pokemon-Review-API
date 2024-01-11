using PokemonApp.Data;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICategoryRepo
    {   
        Task<IEnumerable<Category>> GetCategories();
        Task<Category?> GetCategory(int id);
        Task<IEnumerable<Pokemon> > GetPokemonsByCategory(int id);
        Task<bool> CategoryExists(int id);
        Task<bool> UpdateCategory(Category category);        
        Task<bool> CreateCategory(Category category);

        Task<bool> DeleteCategory(Category category);
        Task<bool> Save();

    }
}
