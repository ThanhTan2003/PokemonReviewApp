using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interface
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPoKemonRating(int id);
        bool PokemonExists(int id);
    }
}
