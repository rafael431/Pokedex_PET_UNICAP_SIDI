using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.Model.Details
{
    internal class Complete_pokemon
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

        public ObservableCollection<string> types { get; set; }
        public ObservableCollection<string> abilities { get; set; }
        public ObservableCollection<MoveDetalhes> moves { get; set; }


        public Complete_pokemon()
        {
            types = new ObservableCollection<string>();
            abilities = new ObservableCollection<string>();
            moves = new ObservableCollection<MoveDetalhes>();
        }
    }
}
