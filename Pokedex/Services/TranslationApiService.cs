using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class TranslationApiService : ITranslationApiService
    {
        private readonly Dictionary<TranslationType, string> _translationsToString = new()
        {
            {TranslationType.Yoda, "yoda"},
            {TranslationType.Shakespeare, "shakespeare"},
        };
        public async Task<string> Translate(TranslationType translationType, string sentence)
        {
            if (translationType == TranslationType.NoTranslation)
            {
                return sentence;
            }
            var url = GetTranslationApiUrl(translationType);
            try
            {
                return await GetTranslationFromApi(url, sentence);
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.Message);
            }
            return sentence;
        }

        private string GetTranslationApiUrl(TranslationType translationType)
        {
            var type = _translationsToString[translationType];
            var url = $"https://api.funtranslations.com/translate/{type}.json";
            return url;
        }

        private static async Task<string> GetTranslationFromApi(string url, string sentence)
        {
            var text = new { text = sentence };
            var result = await url
                .PostJsonAsync(text).ReceiveJson<Dictionary<string, object>>();
            return ParseResult(result);
        }

        private static string ParseResult(IReadOnlyDictionary<string, object> result)
        {
            var contents = (JObject) result["contents"];
            return contents["translated"].ToString();
        }
    }
}