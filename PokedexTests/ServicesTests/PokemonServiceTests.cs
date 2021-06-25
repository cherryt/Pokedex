using System.Threading.Tasks;
using FakeItEasy;
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

        [TestCase("cave", TranslationType.Yoda)]
        [TestCase("notCave", TranslationType.Shakespeare)]
        [TestCase("", TranslationType.NoTranslation)]
        public async Task WhenGetTranslatedPokemon_ShouldTranslate(
            string habitat, TranslationType translationType)
        {
            var fakePokemonApiService = A.Fake<IPokemonApiService>();
            var pokemonFromApi = new Pokemon
            {
                Name = "bulbasaur",
                Description = "bulbasaur description",
                Habitat = habitat,
                IsLegendary = false,
            };
            A.CallTo(() => fakePokemonApiService.GetPokemon("bulbasaur")).Returns(pokemonFromApi);
            
            var pokemonService = new PokemonService(fakePokemonApiService);
            var translatedPokemon = await pokemonService.GetTranslatedPokemon("bulbasaur");

            translatedPokemon.TranslationType.Should().Be(translationType);
            translatedPokemon.Pokemon.Should().Be(pokemonFromApi);
        }
    }
}