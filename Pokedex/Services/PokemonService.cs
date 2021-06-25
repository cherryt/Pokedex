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

            var translationType = GetTranslationType(pokemon);
            return new TranslatedPokemon(pokemon, translationType);
        }

        private TranslationType GetTranslationType(Pokemon pokemon)
        {
            var pokemonHabitat = pokemon.Habitat;
            if (string.IsNullOrWhiteSpace(pokemonHabitat))
            {
                return TranslationType.NoTranslation;
            }
            var isHabitatCave = pokemonHabitat.ToLower() == _cave;
            return isHabitatCave ? TranslationType.Yoda : TranslationType.Shakespeare;
        }
    }

}