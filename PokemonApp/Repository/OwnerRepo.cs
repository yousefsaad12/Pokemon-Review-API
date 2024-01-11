using Microsoft.EntityFrameworkCore;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class OwnerRepo : IOwnerRepo
    {   
        private readonly DataContext _context;
        public OwnerRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOwner(Owner owner)
        {
           await _context.AddAsync(owner);

            return await Save();
        }

        public async Task<bool> DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return await Save();
        }

        public async Task<Owner?> GetOwner(int ownerId)
        {
            return await _context.Owners.FirstOrDefaultAsync(o => o.Id == ownerId);
        }

        public async Task<ICollection<Owner>> GetOwners()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<ICollection<Owner>> GetOwnersOfPokemon(int pokeId)
        {
            return await _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToListAsync();
        }

        public async Task<ICollection<Pokemon>> GetPokemonsByOwner(int ownerId)
        {
            return await _context.PokemonOwners.Where(o => o.Owner.Id == ownerId).Select(p => p.Pokemon).ToListAsync();    
        }

        public async Task<bool> OwnerExist(int ownerId)
        {
            return await _context.Owners.AnyAsync(o => o.Id == ownerId);
        }

        public async Task<bool> Save()
        {   
            int save =  await _context.SaveChangesAsync();

            return save > 0 ? true : false;
        }

        public async Task<bool> UpdateOwner(Owner owner)
        {
            _context.Update(owner);

            return await Save();
        }
    }
}
