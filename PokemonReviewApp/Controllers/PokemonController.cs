using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    //Chỉ định các yêu cầu tới controller này được xử lý qua đường dẫn /api/Pokemon,
    [Route("api/[controller]")]

    // Attribute chỉ định rằng controller này sẽ được ASP.NET Core coi là một API controller
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        // Attribute routing chỉ định rằng phương thức GetPokemons sẽ xử lý các yêu cầu HTTP GET.
        [HttpGet]

        // attribute routing chỉ định loại dữ liệu được trả về và mã phản hồi HTTP. Trong trường hợp này,
        // nó chỉ định rằng phương thức này sẽ trả về một danh sách các Pokémon với mã phản hồi 200 (OK).
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            // Kiểm tra xem ModelState có hợp lệ không
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Trả về một phản hồi HTTP OK (mã 200) với danh sách các Pokémon
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type =typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int id)
        {
            if(!_pokemonRepository.PokemonExists(id))
                return NotFound();

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200,Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int id) 
        {
            if(! _pokemonRepository.PokemonExists(id))
                return NotFound();
            var rating = _pokemonRepository.GetPoKemonRating(id);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(rating);
        }
    }
}
