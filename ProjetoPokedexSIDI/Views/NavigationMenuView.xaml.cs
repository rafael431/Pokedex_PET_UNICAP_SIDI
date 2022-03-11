using System;
using System.Collections.Generic;
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

namespace ProjetoPokedexSIDI.Views
{
    public sealed partial class NavigationMenuView : Page
    {
        public NavigationMenuView()
        {
            this.InitializeComponent();
            Content.Navigate(typeof(MainPage));
        }

        private void OnPokedexTapped(object sender, TappedRoutedEventArgs e)
        {
            Content.Navigate(typeof(MainPage));
        }

        private void OnRegisterTapped(object sender, TappedRoutedEventArgs e)
        {
            Content.Navigate(typeof(PokeRegist));
        }
    }
}
