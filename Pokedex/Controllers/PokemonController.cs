using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController
    {
        public ActionResult GetPokemon(string pokemon)
        {
            return new NotFoundResult();
        }
    }
}