using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Projekt_Filmy;

public class LoginViewModel : IDataErrorInfo, INotifyPropertyChanged, ICommand
{
    public string this[string columnName]
    {
        get
        {
            if (string.IsNullOrWhiteSpace(columnName) || columnName == nameof(Login))
            {
                if (string.IsNullOrWhiteSpace(Login))
                {
                    return "Login jest wymagany";
                }
                if (Login.Length < 3)
                {
                    return "Login musi posiadać przynajmniej 3 znaki";
                }
            }/* -> Przez to iż zmieniłem TextBox logowania na Passwordbox walidacja nie działa -> bardzo trudno ją zrobić na element PasswordBox :/ 
            if (string.IsNullOrWhiteSpace(columnName) || columnName == nameof(Password))
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    return "Hasło jest wymagane";
                }
                if (Password.Length < 6)
                {
                    return "Hasło musi posiadać przynajmniej 6 znaków";
                }
            }*/
            return null;
        }
    }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Error => this[string.Empty];
    public string Nickname { get; set; }

    public Uzytkownik LoggedUser { get; set; }

    public List<Filmy> FilmyLista { get; set; }

    public Filmy ObecnieWybranyFilm { get; set; }
    public int ObecnieWybranyFilmId { get; set; }

    public string FilmTitle { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        PasswordBox passwordBox = (PasswordBox)parameter;
        Password = passwordBox.Password;

    }
}

