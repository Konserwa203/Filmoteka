using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace WPF_Projekt_Filmy.Model
{
    public class OmdbServiceModel
    {
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Plot { get; set; }
        public string imdbRating { get; set; }
        public string Poster { get; set; }

        public string Error { get; set; }

        public static Filmy GetFilm(LoginViewModel loginViewModel, MainWindow mainWindow)
        {
            try
            {
                var client = new RestSharp.RestClient("http://www.omdbapi.com/");
                var request = new RestRequest()
                    .AddParameter("apikey", "f2923b18")
                    .AddParameter("t", loginViewModel.FilmTitle);
                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var data = JsonConvert.DeserializeObject<OmdbServiceModel>(response.Content);
                    if (!(string.IsNullOrEmpty(data.Error)))
                    {
                        mainWindow.ShowMessageAsync("", $"Nie znaleziono pozycji o tytule {loginViewModel.FilmTitle} ");
                    }
                    else
                    {
                        using (var webClient = new WebClient())
                        {
                            var Film = TakeCareAboutDataFromGetFilm(data, mainWindow, loginViewModel);
                            return Film;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mainWindow.ShowMessageAsync("Coś poszło nie tak!", $"Błąd: {ex.Message} ");
            }

            return null;
        }

        public  static Filmy TakeCareAboutDataFromGetFilm(OmdbServiceModel data, MainWindow mainWindow, LoginViewModel loginViewModel)
        {
            bool obejrzany;
            int yearInt, ratedInt, runTimeInt;
            double imdbRatingDouble;
            if (mainWindow.NewFilmWatchedBox.IsChecked == true)
            {
                obejrzany = true;
            }
            else
            {
                obejrzany = false;
            }

            if (data.Poster == "N/A")
            {
                data.Poster = "https://www.publicdomainpictures.net/pictures/280000/velka/not-found-image-15383864787lu.jpg";
            }


            if (data.Year == "N/A")
            {
                yearInt = 0;
            }
            else if (data.Year.Length < 4 && data.Year.Length > 4)
            {
                yearInt = 0;
            }
            else
            {
                Int32.TryParse(Regex.Match(data.Year, @"\d+").Value, out yearInt);
            }


            if (data.Rated == "R" || data.Rated == "X")
            {
                ratedInt = 18;
            }
            else if (data.Rated == "N/A")
            {
                ratedInt = 0;
            }
            else
            {
                Int32.TryParse(Regex.Match(data.Rated, @"\d+").Value, out ratedInt);

            }

            if (data.Runtime == "N/A")
            {
                runTimeInt = 0;
            }
            else
            {
                Int32.TryParse(Regex.Match(data.Runtime, @"\d+").Value, out runTimeInt);
            }

            if (data.imdbRating == "N/A")
            {
                imdbRatingDouble = 0.0;
            }
            else
            {
                data.imdbRating = data.imdbRating.Replace('.', ',');
                Double.TryParse(Regex.Match(data.imdbRating, "[0-9].[0-9]").Value, out imdbRatingDouble);                                                                           
            }
          
            return new Filmy
            {
                Tytuł = loginViewModel.FilmTitle,
                Obejrzany = obejrzany,
                RokWydania = yearInt,
                CzasTrwania = runTimeInt,
                Ograniczenia = ratedInt,
                OcenaIMDB = imdbRatingDouble,
                LinkDoObrazka = data.Poster,
                Fabuła = data.Plot,
                IdUzytkownika = loginViewModel.LoggedUser.IdUzytkownika
            };


        }

    }
}

