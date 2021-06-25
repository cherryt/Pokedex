using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Pokedex.Controllers;
using Pokedex.Models;
using Pokedex.Services;

namespace PokedexTests.ControllersTests
{
    public class PokedexControllerTests
    {
        private PokemonController _controller;

        [SetUp]
        public void Setup()
        {
            var pokemonService = new PokemonService();
            _controller = new PokemonController(pokemonService);
        }

        [Test]
        public async Task GivenPokemonDoesNotExist_WhenGetPokemon_ShouldReturnHttpNotFound()
        {
            var result = await _controller.GetPokemon("NotAPokemon");

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GivenPokemonDoesExist_WhenGetPokemon_ShouldReturnSuccessfulResponse()
        {
            const string pokemon = "Bulbasaur";
            var response = await _controller.GetPokemon(pokemon);

            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().BeOfType<Pokemon>();
        }

        [Test]
        public async Task GivenPokemonDoesNotExist_WhenGetTranslatedPokemon_ShouldReturnHttpNotFound()
        {
            var result = await _controller.GetTranslatedPokemon("NotAPokemon");

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GivenPokemonDoesExist_WhenGetTranslatedPokemon_ShouldReturnSuccessfulResponse()
        {
            const string pokemon = "Bulbasaur";
            var response = await _controller.GetTranslatedPokemon(pokemon);

            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().BeOfType<Pokemon>();
        }
    }
}