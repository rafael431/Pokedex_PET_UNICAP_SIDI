using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using ProjetoPokedexSIDI.ConsultaAPI;
using Windows.Storage;
using ProjetoPokedexSIDI.Model;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace ProjetoPokedexSIDI.Repositório
{
    internal class Pokemon_table
    {
        public void insertCreatedPokemon(pokemonDetalhes pokemon)
        {
            if (!pokemon.name.Equals("") && !pokemon.id.Equals("") && !pokemon.base_exp.Equals("") && !pokemon.height.Equals("") && !pokemon.weight.Equals(""))
            {

                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {

                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;

                        CMD_Insert.CommandText = "INSERT INTO pokemonDB VALUES (@id, @name, @sprite, @height, @weight, @base_exp, @hp, @attack, @defense, @special_attack, @special_defense, @speed);"; //insert aqui.
                        CMD_Insert.Parameters.AddWithValue("@id", pokemon.id);
                        CMD_Insert.Parameters.AddWithValue("@name", pokemon.name);
                        CMD_Insert.Parameters.AddWithValue("@sprite", pokemon.sprite);
                        CMD_Insert.Parameters.AddWithValue("@height", pokemon.height);
                        CMD_Insert.Parameters.AddWithValue("@weight", pokemon.weight);
                        CMD_Insert.Parameters.AddWithValue("@base_exp", pokemon.base_exp);
                        CMD_Insert.Parameters.AddWithValue("@hp", pokemon.hp);
                        CMD_Insert.Parameters.AddWithValue("@attack", pokemon.attack);
                        CMD_Insert.Parameters.AddWithValue("@defense", pokemon.defense);
                        CMD_Insert.Parameters.AddWithValue("@special_attack", pokemon.special_attack);
                        CMD_Insert.Parameters.AddWithValue("@special_defense", pokemon.special_defense);
                        CMD_Insert.Parameters.AddWithValue("@speed", pokemon.speed);

                        CMD_Insert.ExecuteReader();

                        con.Close();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro: " + e.Message);
                    }


                }

            }
        }


        public void insertPokemon(APIResponsePokemon pokemon)
        {
            if (!pokemon.name.Equals("") && !pokemon.id.Equals("") && !pokemon.base_experience.Equals("") && !pokemon.height.Equals("") && !pokemon.weight.Equals(""))
            {


                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {

                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;

                        CMD_Insert.CommandText = "INSERT INTO pokemonDB VALUES (@id, @name, @sprite, @height, @weight, @base_exp, @hp, @attack, @defense, @special_attack, @special_defense, @speed);"; //insert aqui.
                        CMD_Insert.Parameters.AddWithValue("@id", pokemon.id);
                        CMD_Insert.Parameters.AddWithValue("@name", pokemon.name);
                        CMD_Insert.Parameters.AddWithValue("@sprite", pokemon.sprites.front_default);
                        CMD_Insert.Parameters.AddWithValue("@height", pokemon.height);
                        CMD_Insert.Parameters.AddWithValue("@weight", pokemon.weight);
                        CMD_Insert.Parameters.AddWithValue("@base_exp", pokemon.base_experience);
                        CMD_Insert.Parameters.AddWithValue("@hp", pokemon.stats[0].base_stat);
                        CMD_Insert.Parameters.AddWithValue("@attack", pokemon.stats[1].base_stat);
                        CMD_Insert.Parameters.AddWithValue("@defense", pokemon.stats[2].base_stat);
                        CMD_Insert.Parameters.AddWithValue("@special_attack", pokemon.stats[3].base_stat);
                        CMD_Insert.Parameters.AddWithValue("@special_defense", pokemon.stats[4].base_stat);
                        CMD_Insert.Parameters.AddWithValue("@speed", pokemon.stats[5].base_stat);

                        CMD_Insert.ExecuteReader();

                        con.Close();


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro: " + e.Message);
                    }


                }

            }

        }
        public ObservableCollection<pokemonDetalhes> GetPokemons()
        {
            ObservableCollection<pokemonDetalhes> pokemonsList = new ObservableCollection<pokemonDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM pokemonDB ORDER BY id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        pokemonsList.Add(new pokemonDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return pokemonsList;
        }

        public ObservableCollection<pokemonDetalhes> GetPokemonsbyID(int id)
        {
            ObservableCollection<pokemonDetalhes> pokemonsList = new ObservableCollection<pokemonDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    Debug.WriteLine("entrei aqui" );

                    String selectCmd = "SELECT * FROM pokemonDB WHERE id = @id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@id", id);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();


                    while (reader.Read())
                    {
                        pokemonsList.Add(new pokemonDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)));
                    }

                    Debug.WriteLine("peguei do banco");
                    

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return pokemonsList;
        }

        public ObservableCollection<pokemonDetalhes> GetPokemonsbyName(String name)
        {
            ObservableCollection<pokemonDetalhes> pokemonsList = new ObservableCollection<pokemonDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    Debug.WriteLine("entrei aqui");

                    String selectCmd = "SELECT * FROM pokemonDB WHERE name = @name";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@name", name);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();


                    while (reader.Read())
                    {
                        pokemonsList.Add(new pokemonDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)));
                    }

                    Debug.WriteLine("peguei do banco");


                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return pokemonsList;
        }

        public int GetPokemonsCountName(String name)
        {
            int countResult = 0;
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    Debug.WriteLine("entrei aqui AAAAAAAAAAAAAAAAA");

                    String selectCmd = "SELECT count(*) FROM pokemonDB WHERE name = @name";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@name", name);

                    countResult = Convert.ToInt32(CMD_Select.ExecuteScalar());

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    //Debug.WriteLine("peguei do banco: ##########" + countResult);

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return countResult;
        }
        public int GetPokemonsCountId(int id)
        {
            int countResult = 0;
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    Debug.WriteLine("entrei aqui AAAAAAAAAAAAAAAAA");

                    String selectCmd = "SELECT count(*) FROM pokemonDB WHERE id = @id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@id", id);

                    countResult = Convert.ToInt32(CMD_Select.ExecuteScalar());

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    Debug.WriteLine("peguei do banco ##########: " + countResult);

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return countResult;
        }



    }
}
