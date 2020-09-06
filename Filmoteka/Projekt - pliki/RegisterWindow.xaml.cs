using MahApps.Metro.Controls;
using System.Windows;
using WPF_Projekt_Filmy.Addons;
using WPF_Projekt_Filmy.Model;

namespace WPF_Projekt_Filmy
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : MetroWindow
    {
        private RegisterViewModel _registerViewModel;
        private FilmyEntities _dataEntities;
        private LoginWindow _loginWindow;
        public RegisterWindow(FilmyEntities dataEntities, LoginWindow loginWindow)
        {
            InitializeComponent();
            _registerViewModel = new RegisterViewModel();
            DataContext = _registerViewModel;
            _dataEntities = dataEntities;
            _loginWindow = loginWindow;
        }
        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            QueryForDatabase.AddUser(_registerViewModel, _dataEntities, this, _loginWindow);
        }
        
    }
}
