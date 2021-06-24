using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController
    {
        private readonly PokemonService _pokemonService;

        public PokemonController(PokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        public ActionResult GetPokemon(string pokemonName)
        {
            var pokemon = _pokemonService.GetPokemon(pokemonName);
            if(pokemon == null)
                return new NotFoundResult();
            return new OkResult();
        }
    }

    public class PokemonService
    {
        public Pokemon GetPokemon(string pokemonName)
        {
            if (pokemonName == "Bulbasaur")
                return new Pokemon();
            return null;
        }
    }

    public class Pokemon
    {
    }
}