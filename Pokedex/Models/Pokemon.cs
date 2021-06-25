namespace Pokedex.Models
{
    public class Pokemon
    {
        public string Name { get; init; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; init; }
    }
}