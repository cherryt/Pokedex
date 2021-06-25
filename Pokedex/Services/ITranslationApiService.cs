using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public interface ITranslationApiService
    {
        Task<string> Translate(TranslationType translationType, string sentence);
    }
}