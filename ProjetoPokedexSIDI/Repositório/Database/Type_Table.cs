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
using System.Collections.ObjectModel;

namespace ProjetoPokedexSIDI.Repositório
{
    internal class Type_Table
    {
        public Type_Table()
        {
                
        }

        public List<TypeDetalhes> GetTypes()
        {
            List<TypeDetalhes> typeList = new List<TypeDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM typeDB ORDER BY pokemon_id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        typeList.Add(new TypeDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return typeList;
        }


        public List<TypeDetalhes> GetTypesbyId(int id)
        {
            List<TypeDetalhes> typeList = new List<TypeDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM typeDB WHERE pokemon_id = @id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@id", id);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        typeList.Add(new TypeDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return typeList;
        }


        public List<TypeDetalhes> GetTypesbyName(string name)
        {
            List<TypeDetalhes> typeList = new List<TypeDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM typeDB WHERE name = @name";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@name", name);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        typeList.Add(new TypeDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return typeList;
        }

        public ObservableCollection<TypeDetalhes> GetTypesbyType(string pokemon_type)
        {
            ObservableCollection<TypeDetalhes> typeList = new ObservableCollection<TypeDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM typeDB WHERE type_name = @type_name";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@type_name", pokemon_type);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        typeList.Add(new TypeDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return typeList;
        }




        public void insertType(int id, string name, string sprite, string types)
        {
            if (!types.Equals("") && !id.Equals("") && !name.Equals(""))
            {

                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {

                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;

                        CMD_Insert.CommandText = "INSERT INTO typeDB VALUES (@id, @name, @sprite, @type)";
                        CMD_Insert.Parameters.AddWithValue("@id", id);
                        CMD_Insert.Parameters.AddWithValue("@name", name);
                        CMD_Insert.Parameters.AddWithValue("@sprite", sprite);
                        CMD_Insert.Parameters.AddWithValue("@type", types);

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




    }
}
