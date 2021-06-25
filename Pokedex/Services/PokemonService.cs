using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private const string Cave = "cave";
        private readonly IPokemonApiService _pokemonApiService;
        private readonly ITranslationApiService _fakeTranslationApiService;

        public PokemonService(IPokemonApiService pokemonApiService, ITranslationApiService fakeTranslationApiService)
        {
            _pokemonApiService = pokemonApiService;
            _fakeTranslationApiService = fakeTranslationApiService;
        }

        public Task<Pokemon> GetPokemon(string pokemonName)
        {
            return _pokemonApiService.GetPokemon(pokemonName);
        }

        public async Task<TranslatedPokemon> GetTranslatedPokemon(string pokemonName)
        {
            var pokemon = await GetPokemon(pokemonName);
            var doesPokemonExist = pokemon != null;
            if (!doesPokemonExist) return null;
            var translationType = GetTranslationType(pokemon);
            return await GetTranslatedPokemon(pokemon, translationType);
        }

        private TranslationType GetTranslationType(Pokemon pokemon)
        {
            var pokemonHabitat = pokemon.Habitat;
            if (string.IsNullOrWhiteSpace(pokemonHabitat))
            {
                return TranslationType.NoTranslation;
            }

            var isHabitatCave = pokemonHabitat.ToLower() == Cave;
            return isHabitatCave ? TranslationType.Yoda : TranslationType.Shakespeare;
        }

        private async Task<TranslatedPokemon> GetTranslatedPokemon(Pokemon pokemon, TranslationType translationType)
        {
            var translatedDescription = await _fakeTranslationApiService.Translate(translationType, pokemon.Description);
            pokemon.Description = translatedDescription;
            return new TranslatedPokemon(pokemon, translationType);
        }
    }
}