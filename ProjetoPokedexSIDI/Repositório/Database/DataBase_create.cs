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

namespace ProjetoPokedexSIDI
{
    class DataBase_create
    {
        public DataBase_create()
        {
            InitializeDB();
        }

        public async static void InitializeDB()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("pokeData.db", CreationCollisionOption.OpenIfExists);
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokeData.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();
                    string initCMD = "CREATE TABLE IF NOT EXISTS pokemonDB" +
                             "(id INTEGER PRIMARY KEY," +
                             "name NVARCHAR(50) NOT NULL, " +
                             "sprite NVARCHAR(150) NOT NULL," +
                             "height INTEGER NOT NULL," +
                             "weight INTEGER NOT NULL," +
                             "base_experience INTEGER NOT NULL," +
                             "hp INTEGER NOT NULL," +
                             "attack INTEGER NOT NULL," +
                             "defense INTEGER NOT NULL," +
                             "special_attack INTEGER NOT NULL," +
                             "special_defense INTEGER NOT NULL," +
                             "speed INTEGER NOT NULL);" +

                              "CREATE TABLE IF NOT EXISTS abilityDB" +
                              "(pokemon_id INTEGER NOT NULL," +
                              "name NVARCHAR(50) NOT NULL, " +
                              "ability_name VARCHAR(50) NOT NULL," +
                              "CONSTRAINT pk_CE primary key(pokemon_id,ability_name));" +



                              "CREATE TABLE IF NOT EXISTS moveDB" +
                              "(pokemon_id INTEGER NOT NULL," +
                              "name NVARCHAR(50) NOT NULL, " +
                              "move_name VARCHAR(50) NOT NULL," +
                              "CONSTRAINT pk_CE primary key(pokemon_id,move_name));" +



                              "CREATE TABLE IF NOT EXISTS typeDB" +
                              "(pokemon_id INTEGER NOT NULL," +
                              "name NVARCHAR(50) NOT NULL, " +
                              "sprite NVARCHAR(150) NOT NULL," +
                              "type_name VARCHAR(50) NOT NULL," +
                              "CONSTRAINT pk_CE primary key(pokemon_id,type_name));";



                    SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);
                    CMDcreateTable.ExecuteReader();
                    con.Close();
                }
                catch(Exception e)
                {
                   Console.WriteLine("Erro: "+e.Message);
                }
                

            }

        }

       
    }
    
}
