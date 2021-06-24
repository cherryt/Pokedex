using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Pokedex.Services;

namespace PokedexTests.ServicesTests
{
    public class PokemonServiceTests
    {
        [Test]
        public async Task WhenAPokemonDoesNotExist_ShouldNull()
        {
            var pokemonService = new PokemonService();

            var pokemon = await pokemonService.GetPokemon("NotAPokemon");
            pokemon.Should().BeNull();
        }

        [TestCase("bulbasaur")]
        [TestCase("Bulbasaur")]
        public async Task WhenAPokemonExists_ShouldReturnAPokemon(string bulbasaur)
        {
            var pokemonService = new PokemonService();

            var pokemon = await pokemonService.GetPokemon(bulbasaur);
            
            pokemon.Name.Should().NotBeEmpty();
            pokemon.Description.Should().NotBeEmpty();
            pokemon.Habitat.Should().NotBeEmpty();
            pokemon.IsLegendary.Should().Be(false);
        }
    }
}