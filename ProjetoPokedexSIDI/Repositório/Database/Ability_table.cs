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

namespace ProjetoPokedexSIDI.Repositório
{
    internal class Ability_table
    {
        public Ability_table()
        {

        }

        public void insertAbility(int id, string name, string ability)
        {
            if (!ability.Equals("") && !id.Equals("")  && !name.Equals(""))
            {

                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {

                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;

                        CMD_Insert.CommandText = "INSERT INTO abilityDB VALUES (@id, @name, @ability)";
                        CMD_Insert.Parameters.AddWithValue("@id", id);
                        CMD_Insert.Parameters.AddWithValue("@name", name);
                        CMD_Insert.Parameters.AddWithValue("@ability", ability);

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

        public List<AbilityDetalhes> GetAbilities()
        {
            List<AbilityDetalhes> abilityList = new List<AbilityDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM abilityDB ORDER BY pokemon_id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        abilityList.Add(new AbilityDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return abilityList;
        }

        public List<AbilityDetalhes> GetAbilitiesbyId(int id)
        {
            List<AbilityDetalhes> abilityList = new List<AbilityDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM abilityDB WHERE pokemon_id = @id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@id", id);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        abilityList.Add(new AbilityDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return abilityList;
        }

        public List<AbilityDetalhes> GetAbilitiesbyName(string name)
        {
            List<AbilityDetalhes> abilityList = new List<AbilityDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM abilityDB WHERE name = @name";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@name", name);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        abilityList.Add(new AbilityDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return abilityList;
        }


    }
}
