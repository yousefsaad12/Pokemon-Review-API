using Microsoft.EntityFrameworkCore;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using System.Xml.Linq;

namespace PokemonApp.Repository
{
    public class PokemonRepo : IPokemonRepo
    {

        private readonly DataContext _context;

        public PokemonRepo(DataContext context) 
        {
            _context = context;
        }

        public async Task<bool> CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = await _context.Owners
                                             .Where(o => o.Id == ownerId)
                                             .FirstOrDefaultAsync();

            var category = await _context.Categories
                                   .Where(o => o.Id == categoryId)
                                   .FirstOrDefaultAsync();

            var pokemonOwner = new PokemonOwner() 
            { 
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            await _context.AddAsync(pokemonCategory);

            await _context.AddAsync(pokemon);

            return await Save();

        }

        public async Task<bool> DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            
            return await Save();
        }

        public  async Task<Pokemon?> GetPokemon(int id)
        {
            return await _context.Pokemons.Where(p => p.Id == id).FirstOrDefaultAsync();   
        }


        public async Task<int> GetPokemonRating(int id)
        {
           Review review = await _context.Reviews.FirstOrDefaultAsync(review => review.Pokemon.Id == id);

            if (review.Rating <= 0 || review == null)
                return 0;

           return review.Rating;
        }

        public async Task<ICollection<Pokemon>> GetPokemons()
        {
            return await _context.Pokemons.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<bool> PokemonExists(int id)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> Save()
        {
           var saved = await _context.SaveChangesAsync();
           
           return saved > 0 ? true : false;
        }

        public async Task<bool> UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return await Save();
        }
    }
}
