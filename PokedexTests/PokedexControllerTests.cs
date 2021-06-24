using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Pokedex.Controllers;

namespace PokedexTests
{
    public class PokedexControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenPokemonDoesNotExist_ShouldReturnHttpNotFound()
        {
            var controller = new PokemonController();

            var result = controller.GetPokemon("NotAPokemon");

            result.Should().BeOfType<NotFoundResult>();

        }
    }
}