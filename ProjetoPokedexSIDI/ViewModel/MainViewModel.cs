using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ProjetoPokedexSIDI.ConsultaAPI;
using ProjetoPokedexSIDI.Model;
using ProjetoPokedexSIDI.Model.ConsultaAPI;
using ProjetoPokedexSIDI.Model.Details;
using ProjetoPokedexSIDI.Repositório;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace ProjetoPokedexSIDI.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        //Variables
        #region Variables
        API_Request API = new API_Request();
        int i = 0;
        string info;
        APIResponsePokemon result;

        Pokemon_table poketable = new Pokemon_table();
        Ability_table abilitytable = new Ability_table();
        Moves_table movesTable = new Moves_table();
        Type_Table typeTable = new Type_Table();

        private ObservableCollection<pokemonDetalhes> pokemons_ = new ObservableCollection<pokemonDetalhes>();
        private ObservableCollection<TypeDetalhes> types_ = new ObservableCollection<TypeDetalhes>();
        ObservableCollection<TypeDetalhes> type_Detalhes_list_ = new ObservableCollection<TypeDetalhes>();
        ObservableCollection<string> pokemon_types_ = new ObservableCollection<string>();
        ObservableCollection<string> pokemon_moves_ = new ObservableCollection<string>();
        ObservableCollection<string> pokemon_abilities_ = new ObservableCollection<string>();

        private Visibility IsVisible_ = Visibility.Collapsed;

        private Complete_pokemon pokeComplete_ = new Complete_pokemon();
        private int counter_ = 10;
        private int textIdBox_;
        private string textIdBoxStr_;
        private string textNameBox_;
        private string textTypeBox_;
        private string textIdStatus_;
        private string textNameStatus_;
        private string textTypeStatus_;
        private string TextTeste_;
        private string ErrorMessage_;
        private pokemonDetalhes SelectedPokemon_;
        #endregion

        //ICommands
        #region ICommands
        public ICommand ClickCommandId
        {
            get;
            private set;
        }
        public ICommand ClickCommandName
        {
            get;
            private set;
        }

        public ICommand ClickCommandAll
        {
            get;
            private set;
        }

        public ICommand ClickCommandType
        {
            get;
            private set;
        }

        public ICommand PokemonClicked
        {
            get;
            private set;
        }

        public ICommand ClickCommandGetMore
        {
            get;
            private set;
        }
        public ICommand ClickCommandTypeGetPokemon
        {
            get;
            private set;

        }
        #endregion  // ICommands

        //Observables
        #region Observables
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

        public ObservableCollection<TypeDetalhes> Type_Detalhes_list
        {
            get => type_Detalhes_list_;
            set => SetProperty(ref type_Detalhes_list_, value);
        }
        public ObservableCollection<string> Pokemon_types
        {
            get => pokemon_types_;
            set => SetProperty(ref pokemon_types_, value);
        }

        public ObservableCollection<string> Pokemon_moves
        {
            get => pokemon_moves_;
            set => SetProperty(ref pokemon_moves_, value);
        }

        public ObservableCollection<string> Pokemon_abilities
        {
            get => pokemon_abilities_;
            set => SetProperty(ref pokemon_abilities_, value);
        }

        public Complete_pokemon CompletePokemon
        {
            get => pokeComplete_;
            set => SetProperty(ref pokeComplete_, value);
        }

        public Visibility IsVisible
        {
            get => IsVisible_;
            set => SetProperty(ref IsVisible_, value);
        }

        public int Counter
        {
            get => counter_;
            set => SetProperty(ref counter_, value);
        }

        public string TextIdBoxStr
        {
            get => textIdBoxStr_;
            set => SetProperty(ref textIdBoxStr_, value);
        }

        public int TextIdBox
        {
            get => textIdBox_;
            set => SetProperty(ref textIdBox_, value);
        }
        public string textIdStatus
        {
            get => textIdStatus_;
            set => SetProperty(ref textIdStatus_, value);
        }

        public string ErrorMessage
        {
            get => ErrorMessage_;
            set => SetProperty(ref ErrorMessage_, value);
        }
        public string TextNameBox
        {
            get => textNameBox_;
            set => SetProperty(ref textNameBox_, value);
        }
        public string textNameStatus
        {
            get => textNameStatus_;
            set => SetProperty(ref textNameStatus_, value);
        }
        public string TextTypeBox
        {
            get => textTypeBox_;
            set => SetProperty(ref textTypeBox_, value);
        }

        public string TextTypeStatus
        {
            get => textTypeStatus_;
            set => SetProperty(ref textTypeStatus_, value);
        }

        public string TextTeste
        {
            get => TextTeste_;
            set => SetProperty(ref TextTeste_, value);
        }
        public pokemonDetalhes SelectedPokemon
        {
            get => SelectedPokemon_;
            set => SetProperty(ref SelectedPokemon_, value);
        }
        #endregion

        //Constructor
        #region Constructor
        public MainViewModel() //CONSTRUTOR
        {

            getAllPokemons();
            ClickCommandId = new RelayCommand(getPokemonId);
            ClickCommandName = new RelayCommand(getPokemonName);
            ClickCommandAll = new RelayCommand(getAllPokemonsClick);
            ClickCommandType = new RelayCommand(getPokemonType);
            ClickCommandGetMore = new RelayCommand(getMorePokemonType);
            ClickCommandTypeGetPokemon = new RelayCommand<TypeDetalhes>(getPokemonNameParameter);
            PokemonClicked = new RelayCommand<pokemonDetalhes>(getAtributes);
        }
        #endregion


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

        private void getAtributes(pokemonDetalhes e)
        {
            SelectedPokemon = e;
            TextTeste = SelectedPokemon.name;
            createCompletedPokemon(e);
            ErrorMessage = "";
        }

        private void createPokemonDetalhes(TypeDetalhes e)
        {
            pokemonDetalhes newPokemon;
            TextNameBox = e.pokemonName;
            getPokemonName();

        }


        public void createCompletedPokemon(pokemonDetalhes pokemon_selected)
        {
            List<TypeDetalhes> typesList = new List<TypeDetalhes>();
            ObservableCollection<MoveDetalhes> moveList = new ObservableCollection<MoveDetalhes>();
            List<AbilityDetalhes> abilitiesList = new List<AbilityDetalhes>();


            CompletePokemon.id = pokemon_selected.id;
            CompletePokemon.name = pokemon_selected.name;
            CompletePokemon.sprite = pokemon_selected.sprite;
            CompletePokemon.height = pokemon_selected.height;
            CompletePokemon.weight = pokemon_selected.weight;
            CompletePokemon.base_exp = pokemon_selected.base_exp;
            CompletePokemon.hp = pokemon_selected.hp;
            CompletePokemon.attack = pokemon_selected.attack;
            CompletePokemon.defense = pokemon_selected.defense;
            CompletePokemon.special_attack = pokemon_selected.special_attack;
            CompletePokemon.special_defense = pokemon_selected.special_defense;
            CompletePokemon.speed = pokemon_selected.speed;
            int i = 0;

            //Type_Detalhes_list = typeTable.GetTypesbyName(pokemon_selected.name);
            typesList = typeTable.GetTypesbyName(pokemon_selected.name);
            moveList = movesTable.GetMovesbyName(pokemon_selected.name);
            abilitiesList = abilitytable.GetAbilitiesbyName(pokemon_selected.name);



            foreach (var typeDetalhes in typesList)
            {
                Pokemon_types.Add(typesList[i].type_name); //tenho uma lista com o nome dos tipos do pokemon
                CompletePokemon.types.Add(typesList[i].type_name);
                Debug.WriteLine("AAAAAAAAAAAAAAA");
                Debug.WriteLine(typesList[i].type_name);
                i++;
            }
            i = 0;

            CompletePokemon.types = Pokemon_types;

            foreach (var moveDetalhes in moveList)
            {
                pokemon_moves_.Add(moveList[i].name); //tenho uma lista com o nome dos tipos do pokemon
                CompletePokemon.moves.Add(moveList[i]);
                Debug.WriteLine("BBBBBBBBBBBBBBBBBB");
                Debug.WriteLine(moveList[i].name);
                i++;
            }
            i = 0;

            //CompletePokemon.moves = pokemon_moves_;

            foreach (var abilityDetalhes in abilitiesList)
            {
                Pokemon_abilities.Add(abilitiesList[i].name); //tenho uma lista com o nome dos tipos do pokemon
                CompletePokemon.abilities.Add(abilityDetalhes.name);
                Debug.WriteLine("CCCCCCCCCCCCCCCCCCC");
                Debug.WriteLine(abilitiesList[i].name);
                i++;
            }
            i = 0;

            CompletePokemon.abilities = Pokemon_abilities;

        }



        public void getAllPokemonsClick()
        {
            Pokemons = getAllPokemons();

        }


        public void getPokemonNameParameter(TypeDetalhes pokemon)
        {
            ObservableCollection<pokemonDetalhes> list = new ObservableCollection<pokemonDetalhes>();

            if (pokemon.pokemonName.Equals(""))
            {
                return;
            }
            else
            {
                string name = pokemon.pokemonName;
                name = name.ToLower();
                list = poketable.GetPokemonsbyName(name);
                SelectedPokemon = list[0];
                createCompletedPokemon(list[0]);
            }
        }



        public async void getPokemonName()
        {
            Types.Clear();
            //pokemonDetalhes pokemon = null;

            if (string.IsNullOrEmpty(TextNameBox))
            {
                Pokemons = getAllPokemons();
            }
            else if (IsNumeric(TextNameBox))
            {
                textNameStatus = "Por favor, digite somente letras";
            }
            else
            {
                TextNameBox = TextNameBox.ToLower();
                int pokemonCount = poketable.GetPokemonsCountName(TextNameBox); // SELECT COUNT POR NOME

                if (pokemonCount > 0)  //SE TIVER O POKEMON NA BASE
                {
                    textNameStatus = "";
                    Pokemons = poketable.GetPokemonsbyName(TextNameBox);
                    ClearTextBox();
                    //pokemon = pokemons_[0];
                    //return pokemon;
                }
                else // CASO NÃO TENHA O POKEMON NA BASE => BUSCA NA API E INSERE
                {
                    VisibleGo();

                    await Task.Run(() =>
                    {
                        result = Task.Run(async () => await API.RespostaAPI(TextNameBox)).Result;
                    });

                    if (result == null)
                    {
                        textNameStatus = "O pokemon informado não existe!";
                        VisibleGo();
                        ClearTextBox();
                    }
                    else if (result.id > 251)
                    {
                        textNameStatus = "Pokemon não é da 1ª nem 2ª geração!";
                        VisibleGo();
                        ClearTextBox();
                    }
                    else
                    {
                        textNameStatus = "";
                        poketable.insertPokemon(result);

                        foreach (var ability in result.abilities)
                        {

                            abilitytable.insertAbility(result.id, result.name, result.abilities[i].ability.name);
                            //Debug.WriteLine(result.abilities[i].ability.name);
                            i++;
                        }
                        i = 0;

                        foreach (var move in result.moves)
                        {
                            movesTable.insertMove(result.id, result.name, result.moves[i].move.name);
                            //Debug.WriteLine(result.moves[i].move.name);
                            i++;
                        }
                        i = 0;

                        foreach (var type in result.types)
                        {
                            typeTable.insertType(result.id, result.name, result.sprites.front_default, result.types[i].type.name);
                            //Debug.WriteLine(result.types[i].type.name);
                            i++;
                        }
                        i = 0;

                        Pokemons = poketable.GetPokemonsbyName(TextNameBox);
                        VisibleGo();
                        ClearTextBox();
                    }

                }



            }
        }


        public async void getPokemonId()
        {
            Types.Clear();

            if (!Int32.TryParse(TextIdBoxStr, out textIdBox_))
            {
                textIdStatus = "Digite uma valor numerico como ID";
                ClearTextBox();
            }
            else
            {
                Int32.TryParse(TextIdBoxStr, out textIdBox_);

                if (TextIdBox <= 0 || TextIdBox > 251)
                {
                    textIdStatus = "Por favor, digite um valor de 1 a 251.";
                    ClearTextBox();
                }

                else
                {
                    textIdStatus = "";
                    List<pokemonDetalhes> pokemonsList = new List<pokemonDetalhes>();

                    int pokemonCount = poketable.GetPokemonsCountId(TextIdBox); // SELECT COUNT POR ID

                    if (pokemonCount > 0)  //SE TIVER O POKEMON NA BASE
                    {
                        Debug.WriteLine("ENTREI NO IF");
                        Pokemons = poketable.GetPokemonsbyID(TextIdBox);
                        ClearTextBox();

                    }
                    else // CASO NÃO TENHA O POKEMON NA BASE => BUSCA NA API E INSERES
                    {
                        Debug.WriteLine("ENTREI NO ELSE");
                        string idString = TextIdBox.ToString();
                        VisibleGo();

                        await Task.Run(() =>
                        {
                            result = Task.Run(async () => await API.RespostaAPI(idString)).Result;
                        });

                        poketable.insertPokemon(result);

                        foreach (var ability in result.abilities)
                        {

                            abilitytable.insertAbility(result.id, result.name, result.abilities[i].ability.name);
                            //Debug.WriteLine(result.abilities[i].ability.name);
                            i++;
                        }
                        i = 0;

                        foreach (var move in result.moves)
                        {
                            movesTable.insertMove(result.id, result.name, result.moves[i].move.name);
                            //Debug.WriteLine(result.moves[i].move.name);
                            i++;
                        }
                        i = 0;

                        foreach (var type in result.types)
                        {
                            typeTable.insertType(result.id, result.name, result.sprites.front_default, result.types[i].type.name);
                            //Debug.WriteLine(result.types[i].type.name);
                            i++;
                        }
                        i = 0;

                        Pokemons = poketable.GetPokemonsbyID(TextIdBox);
                        VisibleGo();
                        ClearTextBox();
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

        public ObservableCollection<pokemonDetalhes> getAllPokemons()
        {
            Types.Clear();
            Pokemons = poketable.GetPokemons();
            return pokemons_;


        }

        public List<TypeDetalhes> getTypebyId(int id)
        {
            List<TypeDetalhes> pokemonsList = new List<TypeDetalhes>(); // MUDAR

            pokemonsList = typeTable.GetTypesbyId(id);
            return pokemonsList;

        }

        public async void getPokemonType()
        {

            if (string.IsNullOrEmpty(TextTypeBox))
            {
                Pokemons = getAllPokemons();
            }
            else if(IsNumeric(TextTypeBox)){
                TextTypeStatus = "Por favor, digite somente letras";
            }
            else
            {
                VisibleGo();
                APIResponseType resultType = null;
                int i = 0;
                TextTypeBox = TextTypeBox.ToLower();
                Pokemons.Clear();
                //aqui teste solicitação type api

                await Task.Run(() =>
                {
                    resultType = Task.Run(async () => await API.RespostaAPITipo(TextTypeBox)).Result;

                });

                if (resultType == null)
                {
                    TextTypeStatus = "O tipo informado não existe!";
                    VisibleGo();
                }

                else
                {
                    TextTypeStatus = "";

                    while (i <= Counter)
                    { //counter = 10
                        insertPokemonByType(resultType.pokemon[i].pokemon.name);
                        i++;
                    }

                    Types = typeTable.GetTypesbyType(TextTypeBox);


                    VisibleGo();
                }

            }
        }

        public void getMorePokemonType()
        {
            Counter = Counter + 10;
            getPokemonType();
            Counter = 10;
        }



        public void insertPokemonByType(string pokemonName)
        {
            if (TextTypeBox.Equals(""))
            {
                Pokemons = getAllPokemons();
            }
            else
            {
                int pokemonCount = poketable.GetPokemonsCountName(pokemonName); // SELECT COUNT POR NOME

                if (pokemonCount > 0)  //SE TIVER O POKEMON NA BASE
                {
                    return;
                }
                else // CASO NÃO TENHA O POKEMON NA BASE => BUSCA NA API E INSERE
                {
                    result = Task.Run(async () => await API.RespostaAPI(pokemonName)).Result;

                    if (result == null)
                    {
                        return;
                    }
                    if (result.id > 251)
                    {
                        return;
                    }
                    else
                    {
                        poketable.insertPokemon(result);

                        foreach (var ability in result.abilities)
                        {

                            abilitytable.insertAbility(result.id, result.name, result.abilities[i].ability.name);
                            //Debug.WriteLine(result.abilities[i].ability.name);
                            i++;
                        }
                        i = 0;

                        foreach (var move in result.moves)
                        {
                            movesTable.insertMove(result.id, result.name, result.moves[i].move.name);
                            //Debug.WriteLine(result.moves[i].move.name);
                            i++;
                        }
                        i = 0;

                        foreach (var type in result.types)
                        {
                            typeTable.insertType(result.id, result.name, result.sprites.front_default, result.types[i].type.name);
                            //Debug.WriteLine(result.types[i].type.name);
                            i++;
                        }
                        i = 0;
                    }
                }
            }
        }
    }
}
