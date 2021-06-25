using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Services;

namespace Pokedex.Controllers
{
    [ApiController]
    public class PokemonController
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("pokemon/{pokemonName}")]
        public async Task<ActionResult> GetPokemon(string pokemonName)
        {
            var pokemon = await _pokemonService.GetPokemon(pokemonName);
            if(pokemon == null)
                return new NotFoundResult();
            return new OkObjectResult(pokemon);
        }

        [HttpGet("pokemon/translated/{pokemonName}")]
        public async Task<ActionResult> GetTranslatedPokemon(string pokemonName)
        {
            var pokemon = await _pokemonService.GetTranslatedPokemon(pokemonName);
            if(pokemon == null)
                return new NotFoundResult();
            return new OkObjectResult(pokemon);
        }
    }
}