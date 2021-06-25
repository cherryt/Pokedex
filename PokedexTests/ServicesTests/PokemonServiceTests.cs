using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Pokedex.Models;
using Pokedex.Services;

namespace PokedexTests.ServicesTests
{
    public class PokemonServiceTests
    {
        private PokemonService _pokemonService;

        [SetUp]
        public void SetUp()
        {
            var pokemonApiService = new PokemonApiService();
            _pokemonService = new PokemonService(pokemonApiService);
        }
        
        [Test]
        public async Task WhenAPokemonDoesNotExist_ShouldReturnNull()
        {
            var pokemon = await _pokemonService.GetPokemon("NotAPokemon");
            pokemon.Should().BeNull();
        }

        [TestCase("bulbasaur")]
        [TestCase("Bulbasaur")]
        public async Task WhenAPokemonExists_ShouldReturnAPokemon(string bulbasaur)
        {
            var pokemon = await _pokemonService.GetPokemon(bulbasaur);
            
            pokemon.Name.Should().NotBeNullOrEmpty();
            pokemon.Description.Should().NotBeNullOrEmpty();
            pokemon.Habitat.Should().NotBeNullOrEmpty();
            pokemon.IsLegendary.Should().BeFalse();
        }

        [Test]
        public async Task GivenPokemonDoesNotExist_WhenGetTranslatedPokemon_ShouldReturnNull()
        {
            var pokemon = await _pokemonService.GetTranslatedPokemon("NotAPokemon");

            pokemon.Should().BeNull();
        }

        [TestCase("bulbasaur")]
        public async Task GivenPokemonDoesNotHaveAHabitat_WhenGetTranslatedPokemon_ShouldNotTranslate(string bulbasaur)
        {
            var pokemon = await _pokemonService.GetTranslatedPokemon(bulbasaur);

            pokemon.Pokemon.Should().NotBeNull();
            pokemon.TranslationType.Should().Be(TranslationType.NoTranslation);
        }

        [TestCase("bulbasaur")]
        public async Task GivenPokemonHasCaveHabitat_WhenGetTranslatedPokemon_ShouldTranslateToYoda(string bulbasaur)
        {
            var pokemon = await _pokemonService.GetTranslatedPokemon(bulbasaur);

            pokemon.TranslationType.Should().Be(TranslationType.Yoda);
            pokemon.Pokemon.Name.Should().Be("yoda name");
            pokemon.Pokemon.Description.Should().Be("yoda description");
            pokemon.Pokemon.Habitat.Should().Be("yoda habitat");
            pokemon.Pokemon.IsLegendary.Should().Be(true);
        }
    }
}