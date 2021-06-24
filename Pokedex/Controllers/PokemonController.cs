using System.Threading.Tasks;
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

        public async Task<ActionResult> GetPokemon(string pokemonName)
        {
            var pokemon = await _pokemonService.GetPokemon(pokemonName);
            if(pokemon == null)
                return new NotFoundResult();
            return new OkResult();
        }
    }
}