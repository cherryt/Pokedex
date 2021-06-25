using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public interface IPokemonApiService
    {
        Task<Pokemon> GetPokemon(string pokemonName);
    }
}