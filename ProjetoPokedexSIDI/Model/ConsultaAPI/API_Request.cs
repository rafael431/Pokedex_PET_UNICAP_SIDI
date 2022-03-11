using ProjetoPokedexSIDI.Model.ConsultaAPI;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPokedexSIDI.ConsultaAPI
{
    class API_Request
    {
        public API_Request()
        {

        }

        public async Task<APIResponsePokemon> RespostaAPI(string info) // nome ou id
        {
            try
            {
                var pokeClient = RestService.For<IAPIService>("https://pokeapi.co/api/v2");

                Debug.WriteLine("Consultando...");

                APIResponsePokemon pokemon = await pokeClient.GetPokemonAsync(info);

                //Debug.WriteLine(pokemon.name);
                //Debug.WriteLine(pokemon.id);

                return pokemon;

                //DataBase.insertPokemon(pokemon.id.ToString(), pokemon.name);
                //Debug.WriteLine("Terminei de inserir");


            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.Message);

            }

            return null;
        }

        public async Task<APIResponseType> RespostaAPITipo(string info) // nome ou id
        {
            try
            {
                var pokeClient = RestService.For<IAPIServiceType>("https://pokeapi.co/api/v2");

                Debug.WriteLine("Consultando...");

                APIResponseType pokemonPerType = await pokeClient.GetPokemonAsync(info);

                Debug.WriteLine("Entrei aqui no tipooooo");
                Debug.WriteLine(pokemonPerType.pokemon.Count);

                return pokemonPerType;

                


            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.Message);

            }

            return null;
        }


    }
}
