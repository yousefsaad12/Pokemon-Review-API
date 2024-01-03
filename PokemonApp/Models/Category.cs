namespace PokemonApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PokemonCategory> PokemonCatgories { get; set; }
    }
}
