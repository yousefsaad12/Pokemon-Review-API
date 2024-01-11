using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IPokemonRepo
    {
        Task<ICollection<Pokemon>> GetPokemons();
        Task<Pokemon?> GetPokemon(int id);

        Task<int> GetPokemonRating(int id);

        Task<bool> PokemonExists(int id);

        Task<bool>CreatePokemon(int ownerId, int categoryId,Pokemon pokemon);

        Task<bool> UpdatePokemon(Pokemon pokemon);

        Task<bool> DeletePokemon(Pokemon pokemon);

        Task<bool> Save();
    }
}
