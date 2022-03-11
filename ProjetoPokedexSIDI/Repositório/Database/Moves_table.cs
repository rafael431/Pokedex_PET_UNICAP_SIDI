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
    internal class Moves_table
    {
        public Moves_table()
        {

        }

        public List<MoveDetalhes> GetMoves()
        {
            List<MoveDetalhes> moveList = new List<MoveDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM moveDB ORDER BY pokemon_id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        moveList.Add(new MoveDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return moveList;
        }

        public List<MoveDetalhes> GetMovesbyId(int id)
        {
            List<MoveDetalhes> moveList = new List<MoveDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM moveDB WHERE pokemon_id = @id";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@id", id);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        moveList.Add(new MoveDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return moveList;
        }

        public ObservableCollection<MoveDetalhes> GetMovesbyName(string name)
        {
            ObservableCollection<MoveDetalhes> moveList = new ObservableCollection<MoveDetalhes>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();

                    String selectCmd = "SELECT * FROM moveDB WHERE name = @name";
                    SqliteCommand CMD_Select = new SqliteCommand(selectCmd, con);
                    CMD_Select.Parameters.AddWithValue("@name", name);

                    SqliteDataReader reader = CMD_Select.ExecuteReader();

                    while (reader.Read())
                    {
                        moveList.Add(new MoveDetalhes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }

            }

            return moveList;
        }


        public void insertMove(int id, string name, string move)
        {
            if (!move.Equals("") && !id.Equals("") && !name.Equals(""))
            {

                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {

                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;

                        CMD_Insert.CommandText = "INSERT INTO moveDB VALUES (@id, @name, @move)";
                        CMD_Insert.Parameters.AddWithValue("@id", id);
                        CMD_Insert.Parameters.AddWithValue("@name", name);
                        CMD_Insert.Parameters.AddWithValue("@move", move);

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
