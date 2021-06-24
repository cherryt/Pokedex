using Pokedex.Controllers;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class PokemonService
    {
        public Pokemon GetPokemon(string pokemonName)
        {
            if (pokemonName == "Bulbasaur")
                return new Pokemon();
            return null;
        }
    }
}