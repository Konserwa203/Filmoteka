using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using WPF_Projekt_Filmy.Addons;

namespace WPF_Projekt_Filmy
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        // ctrl k c
        // ctrl r r
        // ctr  w w
        
        private FilmyEntities dataEntities = new FilmyEntities();
        private LoginViewModel _loginViewModel;
        public LoginWindow()
        {
            _loginViewModel = new LoginViewModel();
            InitializeComponent();
            DataContext = _loginViewModel;
        }
        
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            QueryForDatabase.UserLogin(_loginViewModel, dataEntities, this);
        }

        private void FormularzRejestracji_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow(dataEntities, this).Show();
            this.Hide();
        }

       
    }

}
