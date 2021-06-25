using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemon(string pokemonName);
        Task<Pokemon> GetTranslatedPokemon(string pokemonName);
    }
}