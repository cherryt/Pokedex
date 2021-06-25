namespace Pokedex.Models
{
    public class Pokemon
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Habitat { get; init; }
        public bool IsLegendary { get; init; }
    }

    public class TranslatedPokemon
    {
        public TranslatedPokemon(Pokemon pokemon)
        {
            Pokemon = pokemon;
        }

        public Pokemon Pokemon { get; private set; }
        public TranslationType TranslationType { get; set; }
    }

    public enum TranslationType
    {
        NoTranslation,
        Yoda,
        /*Shakespeare,*/
    }
}