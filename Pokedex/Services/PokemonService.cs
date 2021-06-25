using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly string _cave = "cave";
        private readonly IPokemonApiService _pokemonApiService;

        public PokemonService(IPokemonApiService pokemonApiService)
        {
            _pokemonApiService = pokemonApiService;
        }

        public Task<Pokemon> GetPokemon(string pokemonName)
        {
            return _pokemonApiService.GetPokemon(pokemonName);
        }

        public async Task<TranslatedPokemon> GetTranslatedPokemon(string pokemonName)
        {
            var pokemon = await GetPokemon(pokemonName);
            var doesPokemonExist = pokemon == null;
            if (doesPokemonExist)
            {
                return null;
            }

            var isHabitatCave = IsHabitatCave(pokemon);
            var translationType = isHabitatCave ? TranslationType.Yoda : TranslationType.Shakespeare;
            return new TranslatedPokemon(pokemon, translationType);
        }

        private bool IsHabitatCave(Pokemon pokemon)
        {
            return pokemon.Habitat.ToLower() == _cave;
        }
    }

}