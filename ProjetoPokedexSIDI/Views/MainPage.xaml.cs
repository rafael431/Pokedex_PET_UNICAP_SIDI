using ProjetoPokedexSIDI.ConsultaAPI;
using ProjetoPokedexSIDI.Repositório;
using ProjetoPokedexSIDI.ViewModel;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProjetoPokedexSIDI.Model;
using ProjetoPokedexSIDI.Views;



// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace ProjetoPokedexSIDI
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel view_model = new MainViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = view_model;
                        
        }


        private void NextScreenButtonDetalhes_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(PokemonDetails), view_model.SelectedPokemon);
            if(view_model.SelectedPokemon == null)
            {
                view_model.ErrorMessage = "Selecione um pokemon!";
            }
            else
            {
                this.Frame.Navigate(typeof(PokemonDetails), view_model.CompletePokemon);
            }
            
        }

        private void NextScreenButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(PokeRegist), view_model.SelectedPokemon);
            this.Frame.Navigate(typeof(PokeRegist));
        }

    }
}