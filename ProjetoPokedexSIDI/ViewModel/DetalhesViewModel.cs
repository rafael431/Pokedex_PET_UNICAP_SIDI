using ProjetoPokedexSIDI.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ProjetoPokedexSIDI.ConsultaAPI;
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
using ProjetoPokedexSIDI.Model.Details;

namespace ProjetoPokedexSIDI.ViewModel
{
    internal class DetalhesViewModel : ObservableObject
    {
        private Complete_pokemon selectedPokemon_;
        ObservableCollection<Complete_pokemon> pokemonList_ = new ObservableCollection<Complete_pokemon>();
        Type_Table type_table = new Type_Table();
        Moves_table move_table = new Moves_table();
        Ability_table ability_table = new Ability_table();
        ObservableCollection<MoveDetalhes> moves_detalhes_list_ = new ObservableCollection<MoveDetalhes>(); 

        ObservableCollection<TypeDetalhes> type_Detalhes_list_ = new ObservableCollection<TypeDetalhes>();
        ObservableCollection<string> pokemon_types_;


        public DetalhesViewModel()
        {
            
        }

        public Complete_pokemon SelectedPokemon
        {
            get => selectedPokemon_;
            set => SetProperty(ref selectedPokemon_, value);
        }


        public ObservableCollection<Complete_pokemon> Pokemons
        {
            get => pokemonList_;
            set => SetProperty(ref pokemonList_, value);
        }

        public ObservableCollection<MoveDetalhes> Moves_Detalhes_list
        {
            get => moves_detalhes_list_;
            set => SetProperty(ref moves_detalhes_list_, value);
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


        


    }
}
