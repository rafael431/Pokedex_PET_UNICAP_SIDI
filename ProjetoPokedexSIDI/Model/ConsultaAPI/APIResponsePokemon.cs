using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.ConsultaAPI
{
    public class APIResponsePokemon
    {
        [JsonProperty("abilities")]
        public List<AbilityElement> abilities { get; set; }

        [JsonProperty("base_experience")]
        public int base_experience { get; set; }

        [JsonProperty("height")]
        public int height { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("moves")]
        public List<Move> moves { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("sprites")]
        public Sprites sprites { get; set; }

        [JsonProperty("stats")]
        public List<Stat> stats { get; set; }

        [JsonProperty("types")]
        public List<TypeElement> types { get; set; }

        [JsonProperty("weight")]
        public int weight { get; set; }

    }

    public partial class AbilityElement
    {
        [JsonProperty("ability")]
        public MoveClass ability { get; set; }

    }

    public partial class MoveClass
    {
        [JsonProperty("name")]
        public string name { get; set; }

    }

    public partial class Move
    {
        [JsonProperty("move")]
        public MoveClass move { get; set; }

    }

    public partial class Sprites
    {
        [JsonProperty("other")]
        public Other other { get; set; }

        [JsonProperty("front_default")]
        public String front_default { get; set; }

    }
    public partial class Other
    {
        [JsonProperty("dream_world")]
        public DreamWorld dream_world { get; set; }

        [JsonProperty("official-artwork")]
        public OfficialArtwork official_artwork { get; set; }

        [JsonProperty("home")]
        public home home { get; set; }
    }

    public partial class home
    {
        [JsonProperty("front_default")]
        public String front_default { get; set; }

        [JsonProperty("front_female")]
        public object FrontFemale { get; set; }

        [JsonProperty("front_shiny")]
        public Uri FrontShiny { get; set; }

        [JsonProperty("front_shiny_female")]
        public object FrontShinyFemale { get; set; }
    }


    public partial class DreamWorld
    {
        [JsonProperty("front_default")]
        public string front_default { get; set; }

        [JsonProperty("front_female")]
        public object front_female { get; set; }
    }
    public partial class OfficialArtwork
    {
        [JsonProperty("front_default")]
        public string front_default { get; set; }
    }

    public partial class Stat
    {
        [JsonProperty("base_stat")]
        public int base_stat { get; set; }

        [JsonProperty("stat")]
        public MoveClass stat { get; set; }
    }

    public partial class TypeElement
    {
        [JsonProperty("type")]
        public MoveClass type { get; set; }
    }

}
