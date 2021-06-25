using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using Pokedex.Extensions;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class PokemonApiService : IPokemonApiService
    {
        public async Task<Pokemon> GetPokemon(string pokemonName)
        {
            const string url = "https://pokeapi.co/api/v2/pokemon-species/";
            try
            {
                var result = await GetPokemonFromApi(pokemonName, url);
                return ParseResult(result);
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        private async Task<Dictionary<string, object>> GetPokemonFromApi(string pokemonName, string url)
        {
            return await url.AppendPathSegment(pokemonName.ToLower())
                .GetJsonAsync<Dictionary<string, object>>();
        }

        private static Pokemon ParseResult(IReadOnlyDictionary<string, object> result)
        {
            return new Pokemon
            {
                Name = GetName(result),
                Description = GetDescription(result),
                Habitat = GetHabitat(result),
                IsLegendary = IsLegendary(result) ,
            };
        }

        private static string GetName(IReadOnlyDictionary<string, object> result)
        {
            return result["name"].ToString();
        }

        private static string GetDescription(IReadOnlyDictionary<string, object> result)
        {
            var flavorTextEntries = (JArray) result["flavor_text_entries"];
            var firstFlavorTextEntry = flavorTextEntries[0];
            return firstFlavorTextEntry["flavor_text"].ToString().ReplaceSpecialCharactersWithSpace();
        }

        private static string GetHabitat(IReadOnlyDictionary<string, object> result)
        {
            var habitat = (JObject)result["habitat"];
            return habitat["name"].ToString();
        }

        private static bool IsLegendary(IReadOnlyDictionary<string, object> result)
        {
            return (bool) result["is_legendary"];
        }
    }
}