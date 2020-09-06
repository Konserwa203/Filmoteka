using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using WPF_Projekt_Filmy.Model;

namespace WPF_Projekt_Filmy.Addons
{
    public static class QueryForDatabase
    {
        public static List<Filmy> GetData(LoginViewModel loginViewModel)
        {
            using (var context = new FilmyEntities())
            {
                return context.Filmy.Where(x => x.IdUzytkownika == loginViewModel.LoggedUser.IdUzytkownika).ToList();
            }
        }
        public static List<Filmy> GetData(LoginViewModel loginViewModel, bool? obejrzany)
        {
            using (var context = new FilmyEntities())
            {
                return context.Filmy.Where(x => x.IdUzytkownika == loginViewModel.LoggedUser.IdUzytkownika && x.Obejrzany == obejrzany).ToList();
            }
        }
        public static void AddData(Filmy film)
        {
            using (var dataEntities = new FilmyEntities())
            {
                dataEntities.Filmy.Add(
                    new Filmy
                    {
                        Tytuł = film.Tytuł,
                        Obejrzany = film.Obejrzany,
                        RokWydania = film.RokWydania,
                        CzasTrwania = film.CzasTrwania,
                        Ograniczenia = film.Ograniczenia,
                        OcenaIMDB = film.OcenaIMDB,
                        LinkDoObrazka = film.LinkDoObrazka,
                        Fabuła = film.Fabuła,
                        IdUzytkownika = film.IdUzytkownika
                    });
                dataEntities.SaveChanges();
            }
        }


        public static void DeleteData(LoginViewModel loginViewModel)
        {
            using (var dataEntities = new FilmyEntities())
            {
                var obj = dataEntities.Filmy.First(x => x.IdFilmu == loginViewModel.ObecnieWybranyFilm.IdFilmu);
                dataEntities.Filmy.Remove(obj);
                dataEntities.SaveChanges();
            }

        }

        public static void UpdateData(LoginViewModel loginViewModel, FilmyEntities dataEntities, MainWindow mainWindow)
        {
            bool? obejrzany = loginViewModel.ObecnieWybranyFilm.Obejrzany;
            loginViewModel.ObecnieWybranyFilm.Obejrzany = mainWindow.CheckboxZmianaObejrzany.IsChecked;
            dataEntities.Filmy.AddOrUpdate(loginViewModel.ObecnieWybranyFilm);
            dataEntities.SaveChanges();
        }

        public static void AddUser( RegisterViewModel registerViewModel, FilmyEntities dataEntities, RegisterWindow registerWindow, LoginWindow loginWindow)
        {
            var user = new Uzytkownik
            {
                Nickname = registerViewModel.RegisterLogin,
                Login = registerViewModel.RegisterLogin,
                Haslo = registerViewModel.RegisterPassword,
            };

            if (registerViewModel.Error == null)
            {
                try
                {
                    dataEntities.Uzytkownik.Add(user);
                    dataEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                registerWindow.Hide();
                loginWindow.Show();
            }
            else
            {
                registerWindow.ShowMessageAsync("", "Login bądź hasło nie zostały wprowadzone poprawnie.");
            }
        }

        public static void UserLogin(LoginViewModel loginViewModel, FilmyEntities dataEntities, LoginWindow loginWindow)
        {
            
            var konto = dataEntities.Uzytkownik.FirstOrDefault(x => x.Login == loginViewModel.Login && x.Haslo == loginViewModel.Password);
            if (konto == null)
            {
                loginWindow.ShowMessageAsync("Nie udało się zalogować", "Spróbuj ponownie!");
            }
            else
            {
                loginViewModel.LoggedUser = konto;
                new MainWindow(loginViewModel, dataEntities).Show();
                loginWindow.Hide();
            }
        }
    }
}
