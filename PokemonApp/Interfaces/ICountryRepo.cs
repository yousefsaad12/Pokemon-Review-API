using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICountryRepo
    {
        Task<Country?> GetCountry(int id);
        Task<IEnumerable<Country>> GetCountries();

        Task<bool> CountryExists(int id);
        Task<IEnumerable<Owner>> GetCountryOwners(int id);

        Task<Country> GetCountryByOwner(int id);

        Task<bool> DeleteCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> CreateCountry(Country country);
        Task<bool> Save();
    }
}
