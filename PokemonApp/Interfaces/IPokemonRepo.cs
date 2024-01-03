using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IPokemonRepo
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);

        int GetPokemonRating(int id);

        bool PokemonExists(int id);

        bool CreatePokemon(int ownerId, int categoryId,Pokemon pokemon);

        bool UpdatePokemon(Pokemon pokemon);

        bool DeletePokemon(Pokemon pokemon);

        bool Save();
    }
}
