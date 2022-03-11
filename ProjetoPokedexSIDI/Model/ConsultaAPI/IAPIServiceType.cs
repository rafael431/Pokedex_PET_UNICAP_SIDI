using ProjetoPokedexSIDI.ConsultaAPI;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.Model.ConsultaAPI
{
    internal interface IAPIServiceType
    {
        [Get("/type/{info}")]
        Task<APIResponseType> GetPokemonAsync(string info);


    }
}
