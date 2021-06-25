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
            var fakeTranslationApiService = A.Fake<ITranslationApiService>();
            _pokemonService = new PokemonService(pokemonApiService, fakeTranslationApiService);
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
            var pokemonFromApi = SetUpFakePokemonApiService(fakePokemonApiService, habitat);
            var fakeTranslationApiService = SetUpFakeTranslationApiService(pokemonFromApi);

            var pokemonService = new PokemonService(fakePokemonApiService, fakeTranslationApiService);
            var translatedPokemon = await pokemonService.GetTranslatedPokemon("bulbasaur");

            translatedPokemon.TranslationType.Should().Be(translationType);
            translatedPokemon.Pokemon.Should().Be(pokemonFromApi);
            translatedPokemon.Pokemon.Description.Should().Be("translated description");
            translatedPokemon.Pokemon.Habitat.Should().Be("translated habitat");
        }

        private static Pokemon SetUpFakePokemonApiService(IPokemonApiService fakePokemonApiService, string habitat)
        {
            var pokemonFromApi = new Pokemon
            {
                Name = "bulbasaur",
                Description = "bulbasaur description",
                Habitat = habitat,
                IsLegendary = false,
            };
            A.CallTo(() => fakePokemonApiService.GetPokemon("bulbasaur")).Returns(pokemonFromApi);
            return pokemonFromApi;
        }

        private static ITranslationApiService SetUpFakeTranslationApiService(Pokemon pokemonFromApi)
        {
            var fakeTranslationApiService = A.Fake<ITranslationApiService>();
            A.CallTo(() => fakeTranslationApiService.Translate(A<TranslationType>._, pokemonFromApi.Description))
                .Returns("translated description");
            A.CallTo(() => fakeTranslationApiService.Translate(A<TranslationType>._, pokemonFromApi.Habitat))
                .Returns("translated habitat");
            return fakeTranslationApiService;
        }
    }
}