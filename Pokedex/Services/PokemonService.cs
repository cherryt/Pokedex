using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private string _cave = "cave";
        private IPokemonApiService _pokemonApiService;

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
            if (pokemon == null)
            {
                return null;
            }

            var isHabitatCave = IsHabitatCave(pokemon);
            var translationType = isHabitatCave ? TranslationType.Yoda : TranslationType.Shakespeare;
            return new TranslatedPokemon(pokemon)
            {
                TranslationType = translationType,
            };
        }

        private bool IsHabitatCave(Pokemon pokemon)
        {
            return pokemon.Habitat.ToLower() == _cave;
        }
    }

}