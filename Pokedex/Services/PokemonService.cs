using Pokedex.Models;

namespace Pokedex.Services
{
    public class PokemonService
    {
        public Pokemon GetPokemon(string pokemonName)
        {
            if (pokemonName == "Bulbasaur")
                return new Pokemon
                {
                    Name = "Bulbasaur",
                    Description = "green and blue",
                    Habitat = "not rare",
                    IsLegendary = false,
                };
            return null;
        }
    }
}