using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CountryExists(int countId)
        {
            return await _context.Countries.AnyAsync(c => c.Id == countId);
        }

        public async Task<bool>  CreateCountry(Country country)
        {
            await _context.AddAsync(country);

            return await Save();
        }

        public async Task<bool> DeleteCountry(Country country)
        {
            _context.Remove(country);

            return  await Save();
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await  _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountry(int countId)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == countId);
        }

        public async Task<Country> GetCountryByOwner(int ownerId)
        {
            return await _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefaultAsync();   
        }

        public async Task<IEnumerable<Owner>> GetCountryOwners(int countId) // this part you do not finish by yourself
        {
            return await _context.Owners.Where(c => c.Country.Id == countId).ToListAsync();
        }

        public async Task<bool>  Save()
        {   
            int save = await _context.SaveChangesAsync();

            return save > 0 ? true : false;
        }

        public Task<bool>  UpdateCountry(Country country)
        {
           _context.Update(country);
           return Save();
        }
    }
}
