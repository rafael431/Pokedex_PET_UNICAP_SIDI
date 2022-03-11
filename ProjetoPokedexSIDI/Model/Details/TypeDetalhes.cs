using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.Model
{
    internal class TypeDetalhes
    {
        public int id { get; set; }
        public string pokemonName{ get; set; }
        public string sprite { get; set; }
        public string type_name { get; set; }
        
        public TypeDetalhes(int id_pokemon, string pokemon_name, string pokemon_sprite, string type)
        {
            id = id_pokemon;
            pokemonName = pokemon_name;
            sprite = pokemon_sprite;
            type_name = type;
        }


    }
}
