using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICountryRepo
    {
        public Country GetCountry(int id);
        public IEnumerable<Country> GetCountries();

        bool CountryExists(int id);
        public IEnumerable<Owner> GetCountryOwners(int id);

        public Country GetCountryByOwner(int id);

        bool DeleteCountry(Country country);
        bool UpdateCountry(Country country);
        bool CreateCountry(Country country);
        bool Save();
    }
}
