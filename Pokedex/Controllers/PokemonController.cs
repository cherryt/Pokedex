using Microsoft.AspNetCore.Mvc;
using Pokedex.Services;

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
}