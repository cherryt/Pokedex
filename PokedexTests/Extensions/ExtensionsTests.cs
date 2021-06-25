using FluentAssertions;
using NUnit.Framework;
using Pokedex.Extensions;

namespace PokedexTests.Extensions
{
    public class ExtensionsTests
    {
        [Test]
        public void ShouldReplaceSpecialCharacters()
        {
            var word = "\n$\u000chello";

            var result = word.ReplaceSpecialCharactersWithSpace();

            result.Should().Be("   hello");
        }
    }
}