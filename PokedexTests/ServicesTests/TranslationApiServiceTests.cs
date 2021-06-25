using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using Pokedex.Models;
using Pokedex.Services;

namespace PokedexTests.ServicesTests
{
    public class TranslationApiServiceTests
    {
        private string _sentence;
        private TranslationApiService _service;
        private HttpTest _httpTest;

        [SetUp]
        public void SetUp()
        {
            _sentence = "hello";
            _service = new TranslationApiService();
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }

        [Test]
        public async Task ShouldReturnShakespeareTranslation()
        {
            SetUpHttpResponse(_httpTest);
            var result = await _service.Translate(TranslationType.Shakespeare, _sentence);

            result.Should().Be("hello translation");
            _httpTest.ShouldHaveCalled("https://api.funtranslations.com/translate/shakespeare.json");
        }

        private static void SetUpHttpResponse(HttpTest httpTest)
        {
            var translationFromApi = JsonConvert.SerializeObject(
                new {contents = new {translated = "hello translation"}}
            );
            httpTest.RespondWith(translationFromApi);
        }

        [Test]
        public async Task ShouldReturnYodaTranslation()
        {
            SetUpHttpResponse(_httpTest);
            var result = await _service.Translate(TranslationType.Yoda, _sentence);

            result.Should().Be("hello translation");
            _httpTest.ShouldHaveCalled("https://api.funtranslations.com/translate/yoda.json");
        }

        [Test]
        public async Task WhenNoTranslationType_ShouldReturnStandardTranslation()
        {
            SetUpHttpResponse(_httpTest);
            var result = await _service.Translate(TranslationType.NoTranslation, _sentence);

            result.Should().Be("hello");
        }

        [Test]
        public async Task WhenThereIsAnError_ShouldReturnStandardTranslation()
        {
            _httpTest.RespondWith("bad request", 400);

            var result = await _service.Translate(TranslationType.NoTranslation, _sentence);

            result.Should().Be("hello");
        }
    }
}