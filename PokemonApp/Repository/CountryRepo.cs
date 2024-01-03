using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class CountryRepo : ICountryRepo
    {
        private readonly DataContext _context;
        public CountryRepo(DataContext dataContext) 
        {
            _context = dataContext;
        }
        public bool CountryExists(int countId)
        {
            return _context.Countries.Any(c => c.Id == countId);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);

            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int countId)
        {
            return _context.Countries.FirstOrDefault(c => c.Id == countId);
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();   
        }

        public IEnumerable<Owner> GetCountryOwners(int countId) // this part you do not finish by yourself
        {
            return _context.Owners.Where(c => c.Country.Id == countId).ToList();
        }

        public bool Save()
        {   
            int save = _context.SaveChanges();

            return save > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
           _context.Update(country);
           return Save();
        }
    }
}
