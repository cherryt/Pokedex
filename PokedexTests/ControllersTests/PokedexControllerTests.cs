using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Pokedex.Controllers;
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
        public async Task WhenPokemonDoesNotExist_ShouldReturnHttpNotFound()
        {
            var result = await _controller.GetPokemon("NotAPokemon");

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task WhenPokemonDoesExist_ShouldReturnSuccessfulResponse()
        {
            const string pokemon = "Bulbasaur";
            var result = await _controller.GetPokemon(pokemon);

            result.Should().BeOfType<OkResult>();
        }
    }
}