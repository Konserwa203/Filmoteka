using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;
using WPF_Projekt_Filmy.Addons;
using WPF_Projekt_Filmy.Model;

namespace WPF_Projekt_Filmy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(); // bez tego wystepuje problem niezamykających sie procesów
        }
        public LoginViewModel loginViewModel;
        public FilmyEntities dataEntities;
        public MainWindow(LoginViewModel _loginViewModel, FilmyEntities _dataEntities)
        {
            InitializeComponent();
            this.ShowMessageAsync("Zalogowano pomyślnie!", $"Witaj {_loginViewModel.LoggedUser.Nickname}");
            DataContext = _loginViewModel;
            dataEntities = _dataEntities;
            loginViewModel = _loginViewModel;

            loginViewModel.FilmyLista = QueryForDatabase.GetData(loginViewModel);
        }

        private void ButtonNieobejrzane_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.FilmyLista = QueryForDatabase.GetData(loginViewModel, false);
        }
        private void ButtonObejrzane_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.FilmyLista = QueryForDatabase.GetData(loginViewModel, true);
        }
        private void ButtonWszystkie_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.FilmyLista = QueryForDatabase.GetData(loginViewModel);
        }

        private void OpenFlyout_Click(object sender, RoutedEventArgs e)
        {
            AddFilmFlyout.IsOpen = true;
        }
        private void OpenFlyoutFilmInfo_Click(object sender, RoutedEventArgs e)
        {
            var film = (sender as System.Windows.Controls.Button).DataContext as Filmy;
            loginViewModel.ObecnieWybranyFilm = film;
            FilmInfoFlyout.IsOpen = true;
        }

        private void ButtonObejrzaneChange_Click(object sender, RoutedEventArgs e)
        {
           
            QueryForDatabase.UpdateData(loginViewModel, dataEntities, this);
            loginViewModel.FilmyLista = QueryForDatabase.GetData(loginViewModel); 
            FilmInfoFlyout.IsOpen = false;

        }


        private void ButtonUsunFilm_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                QueryForDatabase.DeleteData(loginViewModel);
            }
            finally
            {
                loginViewModel.FilmyLista = QueryForDatabase.GetData(loginViewModel); // Mogłoby być lepsze 
                FilmInfoFlyout.IsOpen = false;
            }
 
        }


        private void AddFilmToDatabase_Click(object sender, RoutedEventArgs e)
        {

            Filmy nowyFilm = OmdbServiceModel.GetFilm(loginViewModel, this);
            if(nowyFilm != null)
            {
                QueryForDatabase.AddData(nowyFilm);
                loginViewModel.FilmyLista = QueryForDatabase.GetData(loginViewModel);
            }
            AddFilmFlyout.IsOpen = false;
        }

    }
    public static class ExceptionExtensions
    {
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }
    }

    
}
