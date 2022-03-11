using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.Model
{
    internal class MoveDetalhes
    {
        public int id { get; set; }
        public string pokemonName{ get; set; }
        public string name { get; set; }
        public MoveDetalhes(int id_pokemon, string pokemon_name, string move_name)
        {
            id = id_pokemon;
            pokemonName = pokemon_name;
            name = move_name;
        }

    }
}
