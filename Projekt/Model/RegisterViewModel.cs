using System.ComponentModel;

namespace WPF_Projekt_Filmy.Model
{
    public class RegisterViewModel : IDataErrorInfo
    {

        public string this[string columnName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(columnName) || columnName == nameof(RegisterLogin))
                {
                    if (string.IsNullOrWhiteSpace(RegisterLogin))
                    {
                        return "Login jest wymagany";
                    }
                    if (RegisterLogin.Length < 3)
                    {
                        return "Login musi posiadać przynajmniej 3 znaki";
                    }
                   
                }
                if (string.IsNullOrWhiteSpace(columnName) || columnName == nameof(RegisterPassword))
                {
                    if (string.IsNullOrWhiteSpace(RegisterPassword))
                    {
                        return "Hasło jest wymagane";
                    }
                    if (RegisterPassword.Length < 6)
                    {
                        return "Hasło musi posiadać przynajmniej 6 znaków";
                    }
                }
                
                return null;

            }
        }
        public string Error => this[string.Empty];
        public string RegisterLogin { get; set; }
        public string RegisterPassword { get; set; }
      


    }
}
