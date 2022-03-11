using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using ProjetoPokedexSIDI.ConsultaAPI;
using ProjetoPokedexSIDI.Repositório;
using ProjetoPokedexSIDI.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjetoPokedexSIDI.Model.ConsultaAPI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjetoPokedexSIDI.ViewModel
{
    internal class RegistViewModel : ObservableObject
    {
        
        #region consulta da API

        API_Request API = new API_Request();
        int i = 0;
        string info;
        APIResponsePokemon result;

        #endregion

        #region Consulta das Tabelas

        Pokemon_table poketable = new Pokemon_table();
        Type_Table typeTable = new Type_Table();

        #endregion

        #region Criar Observaveis

        private ObservableCollection<pokemonDetalhes> pokemons_ = new ObservableCollection<pokemonDetalhes>();
        private ObservableCollection<TypeDetalhes> types_ = new ObservableCollection<TypeDetalhes>();
        private pokemonDetalhes selectedPokemon_;
        private Visibility IsVisible_ = Visibility.Collapsed;

        #endregion

        #region Aributos da classe

        private int pokeID;
        private String pokeIdText;
        private String pokeName;
        private String pokeType;
        private String status;

        #endregion

        #region Observaveis

        public ObservableCollection<pokemonDetalhes> Pokemons
        {
            get => pokemons_;
            set => SetProperty(ref pokemons_, value);
        }

        public ObservableCollection<TypeDetalhes> Types
        {
            get => types_;
            set => SetProperty(ref types_, value);
        }

        public pokemonDetalhes SelectedPokemon
        {
            get => selectedPokemon_;
            set => SetProperty(ref selectedPokemon_, value);
        }

        public ObservableCollection<pokemonDetalhes> getAllPokemons()
        {
            Types.Clear();
            Pokemons = poketable.GetPokemons();

            //Debug.WriteLine(pokemons_[0].name);
            //Debug.WriteLine(pokemons_[1].name);
            //Debug.WriteLine(pokemonsList[0].sprite);

            return pokemons_;

        }

        public Visibility IsVisible
        {
            get => IsVisible_;
            set => SetProperty(ref IsVisible_, value);
        }

        #endregion

        #region Construtor da ViewModel

        public RegistViewModel()
        {
            ClickCommandPokemon = new RelayCommand(CreatePokemon);
        }

        #endregion

        // ------- Visibilidade --------- //

        private void VisibleGo()
        {

            if (IsVisible == Visibility.Visible)
            {
                IsVisible = Visibility.Collapsed;
            }
            else
            {
                IsVisible = Visibility.Visible;
            }

        }






        // -------- Binding dos TextBox -------- //

        public string PokeStatus
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public int TextIdBox
        {
            get => pokeID;
            set => SetProperty(ref pokeID, value);
        }

        public string TextIdBoxStr
        {
            get => pokeIdText;
            set => SetProperty(ref pokeIdText, value);
        }

        public string TextNameBox
        {
            get => pokeName;
            set => SetProperty(ref pokeName, value);
        }

        public string TextTypeBox
        {
            get => pokeType;
            set => SetProperty(ref pokeType, value);
        }





        // -------- Comando dos Buttons -------- //

        public ICommand ClickCommandPokemon
        {
            get;
            private set;
        }

        // -------- Criador de Pokemon -------- //

        public async void CreatePokemon()
        {
            if (!IsNumeric(pokeIdText)){
                PokeStatus = "Digite uma valor numerico como ID";
                ClearTextBox();
            }
            else
            {
                Int32.TryParse(TextIdBoxStr, out pokeID);

                if (TextIdBox >= 0 && TextIdBox <= 251)
                {
                    PokeStatus = "Digite uma ID acima de 251";
                    ClearTextBox();
                }
                else if (string.IsNullOrEmpty(TextNameBox))
                {
                    PokeStatus = "Nome invalido!";
                    ClearTextBox();
                }
                else if (string.IsNullOrEmpty(TextTypeBox))
                {
                    PokeStatus = "Tipo invalido!";
                    ClearTextBox();
                }
                else
                {
                    VisibleGo();
                    APIResponsePokemon result = null;
                    APIResponseType resultType = null;

                    TextTypeBox = TextTypeBox.ToLower();

                    await Task.Run(() =>
                    {
                        resultType = Task.Run(async () => await API.RespostaAPITipo(TextTypeBox)).Result;
                    });

                    await Task.Run(() =>
                    {
                        result = Task.Run(async () => await API.RespostaAPI(TextNameBox)).Result;
                    });

                    if (result != null)
                    {
                        PokeStatus = result.name + " é um pokemon já existente!";
                        ClearTextBox();
                        VisibleGo();
                    }
                    else if (resultType == null)
                    {
                        PokeStatus = "Tipo inexistente!";
                        ClearTextBox();
                        VisibleGo();
                    }

                    else
                    {

                        TextNameBox = TextNameBox.ToLower();
                        int pokemonCountId = poketable.GetPokemonsCountId(TextIdBox);
                        int pokemonCountName = poketable.GetPokemonsCountName(TextNameBox);

                        if (pokemonCountId <= 0) // nao ha pokemon no banco
                        {

                            // cria pokemon e insere no banco
                            //string sprite = "Assets/RegistImages/pokeSprite.jpg";
                            string sprite = "https://static.quizur.com/i/b/57c1c26fc0b812.5998420157c1c26fb156c9.51498011.png";
                            int height = 1;
                            int weight = 30;
                            int baseExp = 180;
                            int hp = 40;
                            int attack = 50;
                            int defense = 40;
                            int specialAttack = 60;
                            int specialDefense = 50;
                            int speed = 55;

                            pokemonDetalhes newPokemon = new pokemonDetalhes(TextIdBox, TextNameBox, sprite, height, weight, baseExp, hp, attack, defense, specialAttack, specialDefense, speed);

                            poketable.insertCreatedPokemon(newPokemon);
                            typeTable.insertType(TextIdBox, TextNameBox, sprite, TextTypeBox);

                            Pokemons = poketable.GetPokemonsbyID(TextIdBox);
                            Types = typeTable.GetTypesbyType(TextTypeBox);

                            PokeStatus = "Pokemon criado!";
                            ClearTextBox();
                            VisibleGo();

                        }
                        else if (pokemonCountName > 0)
                        {
                            PokeStatus = "Nome já existente no banco!";
                            ClearTextBox();
                            VisibleGo();
                        }
                        else
                        {
                            PokeStatus = "ID já existente no banco!";
                            ClearTextBox();
                            VisibleGo();
                        }

                    }
                }
            }
        }


        public void ClearTextBox()
        {
            TextIdBoxStr = "";
            TextNameBox = "";
            TextTypeBox = "";
        }

        public static bool IsNumeric(string s)
        {
            return int.TryParse(s, out int n);
        }

    }


       

    
}
