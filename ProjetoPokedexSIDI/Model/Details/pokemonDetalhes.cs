using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.Model
{
    internal class pokemonDetalhes
    {
        public int id { get; set; }
        public string name { get; set; }

        public string sprite { get; set; }
        public int height { get; set; }

        public int weight { get; set; }
        public int base_exp { get; set; }

        public int hp { get; set; }

        public int attack { get; set; }

        public int defense { get; set; }

        public int special_attack { get; set; }

        public int special_defense { get; set; }

        public int speed { get; set; }

       


        public pokemonDetalhes(int id_Pokemon, string name_Pokemon, string sprite_pokemon, int height_pokemon, int weight_pokemon, int baseexp_pokemon, int hp_pokemon, int attack_pokemon, int defense_pokemon, int special_attack_pokemon, int special_defense_pokemon, int speed_pokemon)
        {
            id = id_Pokemon;
            name = name_Pokemon;
            sprite = sprite_pokemon;
            height = height_pokemon;
            weight = weight_pokemon;
            base_exp = baseexp_pokemon;
            hp = hp_pokemon;
            attack = attack_pokemon;
            defense = defense_pokemon;
            special_attack = special_attack_pokemon;
            special_defense = special_defense_pokemon;
            speed = speed_pokemon;
        }


    }
}
