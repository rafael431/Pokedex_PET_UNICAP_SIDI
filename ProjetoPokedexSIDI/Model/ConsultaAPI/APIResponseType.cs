using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.Model.ConsultaAPI
{
    public class APIResponseType
    {
        [JsonProperty("pokemon")]
        public List<Pokemon> pokemon { get; set; }
        
        public partial class Pokemon
        {
            [JsonProperty("pokemon")]
            public PokemonDetails pokemon { get; set; }

        }

        public partial class PokemonDetails
        {
            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("url")]
            public string url { get; set; }
        }


    }
}
