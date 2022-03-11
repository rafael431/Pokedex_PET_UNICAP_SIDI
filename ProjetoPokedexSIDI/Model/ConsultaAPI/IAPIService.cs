using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.ConsultaAPI
{
    public interface IAPIService
    {
        [Get("/pokemon/{info}")]
        Task<APIResponsePokemon> GetPokemonAsync(string info);


    }
}
