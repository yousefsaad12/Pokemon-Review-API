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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners
                                             .Where(o => o.Id == ownerId)
                                             .FirstOrDefault();

            var category = _context.Categories
                                   .Where(o => o.Id == categoryId)
                                   .FirstOrDefault();

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

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();

        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();   
        }


        public int GetPokemonRating(int id)
        {
           Review review = _context.Reviews.FirstOrDefault(review => review.Pokemon.Id == id);

            if (review.Rating <= 0 || review == null)
                return 0;

           return review.Rating;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
           
           return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}
