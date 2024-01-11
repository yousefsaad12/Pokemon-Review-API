using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IOwnerRepo
    {
        Task<ICollection<Owner>> GetOwners();

        Task<Owner?> GetOwner(int ownerId);

        Task<ICollection<Owner>> GetOwnersOfPokemon(int pokeId);

        Task<ICollection<Pokemon>> GetPokemonsByOwner(int ownerId);
        Task<bool> OwnerExist(int ownerId);

        Task<bool> UpdateOwner(Owner owner);

        Task<bool> DeleteOwner(Owner owner);

        Task<bool> CreateOwner(Owner owner);
        Task<bool> Save();


    }
}
