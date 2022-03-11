using ProjetoPokedexSIDI.Model;
using ProjetoPokedexSIDI.Model.Details;
using ProjetoPokedexSIDI.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjetoPokedexSIDI.Views
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class PokemonDetails : Page
    {
        private DetalhesViewModel view_model = new DetalhesViewModel ();
        
        public PokemonDetails()
        {
            this.DataContext = view_model;
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            base.OnNavigatedTo(e);
            Complete_pokemon new_pokemon = (Complete_pokemon)e.Parameter;
            view_model.SelectedPokemon = (Complete_pokemon)e.Parameter;
            view_model.Pokemons.Add((Complete_pokemon)e.Parameter);
            view_model.Moves_Detalhes_list = new_pokemon.moves;
            
            

        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
