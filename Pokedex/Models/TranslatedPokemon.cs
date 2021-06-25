namespace Pokedex.Models
{
    public class TranslatedPokemon
    {
        public TranslatedPokemon(Pokemon pokemon, TranslationType translationType)
        {
            Pokemon = pokemon;
            TranslationType = translationType;
        }

        public Pokemon Pokemon { get; }
        public TranslationType TranslationType { get; }
    }
}