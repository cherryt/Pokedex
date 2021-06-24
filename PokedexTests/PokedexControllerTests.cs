using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Pokedex.Controllers;

namespace PokedexTests
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
        public void WhenPokemonDoesNotExist_ShouldReturnHttpNotFound()
        {
            var result = _controller.GetPokemon("NotAPokemon");

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void WhenPokemonDoesExist_ShouldReturnSuccessfulResponse()
        {
            const string pokemon = "Bulbasaur";
            var result = _controller.GetPokemon(pokemon);

            result.Should().BeOfType<OkResult>();
        }
    }
}