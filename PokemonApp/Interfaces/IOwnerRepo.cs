using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IOwnerRepo
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int ownerId);

        ICollection<Owner> GetOwnersOfPokemon(int pokeId);

        ICollection<Pokemon> GetPokemonsByOwner(int ownerId);
        bool OwnerExist(int ownerId);

        bool UpdateOwner(Owner owner);

        bool DeleteOwner(Owner owner);

        bool CreateOwner(Owner owner);
        bool Save();


    }
}
