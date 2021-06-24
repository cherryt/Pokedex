using FluentAssertions;
using NUnit.Framework;
using Pokedex.Services;

namespace PokedexTests.ServicesTests
{
    public class PokemonServiceTests
    {
        [Test]
        public void WhenAPokemonDoesNotExist_ShouldNull()
        {
            var pokemonService = new PokemonService();

            var pokemon = pokemonService.GetPokemon("NotAPokemon");
            pokemon.Should().BeNull();
        }

        [Test]
        public void WhenAPokemonExists_ShouldReturnAPokemon()
        {
            var pokemonService = new PokemonService();

            var pokemon = pokemonService.GetPokemon("Bulbasaur");
            
            pokemon.Name.Should().Be("Bulbasaur");
            pokemon.Description.Should().NotBeEmpty();
            pokemon.Habitat.Should().NotBeEmpty();
            pokemon.IsLegendary.Should().Be(false);
        }
    }
}