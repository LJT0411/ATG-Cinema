using CinemaApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.AdminConsole
{
    public class AllData
    {
        private const string controllerName = "Admins";
        HttpResponseMessage response;

        public void GenerateUser()
        {
            List<Users> users = new List<Users>()
            {
            new Users(){Username = "gsc",Password="gsc",Email="gsc@outlook.com" },
            new Users(){Username = "tgv",Password="tgv",Email="tgv@outlook.com" }
            };
            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/AddUsers", users).Result;
        }

        public void GenerateMovie()
        {
            List<Movies> movies = new List<Movies>()
            {
            new Movies() { MovieTitle = "Bloodshot", MovieReleaseTime = new DateTime(2020, 03, 12), MovieAvailable = MAvail.NowShowing , Duration = "110 Minutes" },
            new Movies() { MovieTitle = "Captain Marvel", MovieReleaseTime = new DateTime(2020, 03, 07), MovieAvailable = MAvail.NowShowing , Duration = "124 Minutes" },
            new Movies() { MovieTitle = "Wonder Woman 1984", MovieReleaseTime = new DateTime(2020, 08, 13), MovieAvailable = MAvail.ComingSoon , Duration = "" },
            new Movies() { MovieTitle = "Black Widow", MovieReleaseTime = new DateTime(2020, 11, 06), MovieAvailable = MAvail.ComingSoon , Duration = "" }
            };
            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/AddMovies", movies).Result;
        }

        public void GenerateMovieTime()
        {
            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieTimesByID/Bloodshot").Result;
            var getMovieID = response.Content.ReadAsAsync<Movies>().Result;

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieTimesByID/Captain Marvel").Result;
            var getMovieID2 = response.Content.ReadAsAsync<Movies>().Result;

            List<MovieTimes> movieTimes = new List<MovieTimes>()
            {
            new MovieTimes() { MovieTimeStart = new DateTime(2020, 03, 14, 10, 0, 0), MoviesID = getMovieID.MoviesID , MovieHallNo = "One" },
            new MovieTimes() { MovieTimeStart = new DateTime(2020, 03, 14, 14, 30, 0), MoviesID = getMovieID.MoviesID , MovieHallNo = "Two" },
            new MovieTimes() { MovieTimeStart = new DateTime(2020, 03, 14, 18, 10, 0), MoviesID = getMovieID.MoviesID , MovieHallNo = "One" },

            new MovieTimes() { MovieTimeStart = new DateTime(2020, 03, 10, 10, 0, 0), MoviesID = getMovieID2.MoviesID , MovieHallNo = "Two" },
            new MovieTimes() { MovieTimeStart = new DateTime(2020, 03, 10, 14, 30, 0), MoviesID = getMovieID2.MoviesID , MovieHallNo = "One" },
            new MovieTimes() { MovieTimeStart = new DateTime(2020, 03, 10, 18, 10, 0), MoviesID = getMovieID2.MoviesID , MovieHallNo = "One" }
            };
            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/AddMovieTimes", movieTimes).Result;
        }

        public void GenerateMovieHall()
        {
            List<MovieHall> movieHalls = new List<MovieHall>()
            {
            new MovieHall() { MovieHallNo = "One", Rows = 4, Column = 11 },
            new MovieHall() { MovieHallNo = "Two", Rows = 5, Column = 11 }
            };
            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/AddMovieHalls", movieHalls).Result;
        }

        public void GenerateMovieSeats()
        {
            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieHalls").Result;
            var movietimeList = response.Content.ReadAsAsync<IEnumerable<MovieTimes>>().Result;

            List<MovieSeats> ListOfSeat = new List<MovieSeats>();
            Random SeatA = new Random();
            foreach (var item in movietimeList)
            {
                response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetHallByNo/{item.MovieHallNo}").Result;
                var checkHall = response.Content.ReadAsAsync<MovieHall>().Result;

                //Loop Row
                for (int i = 1; i < checkHall.Rows; i++)
                {
                    // Loop the seat
                    for (int x = 1; x < checkHall.Column; x++)
                    {
                        // Random the taken and empty
                        SAvail Avail = (SAvail)SeatA.Next(2);

                        MovieSeats SeatList = new MovieSeats
                        {
                            SeatNo = i + "," + x,
                            SeatAvail = Avail,
                            MovieTimesID = item.MovieTimesID,
                            Amount = 13
                        };
                        ListOfSeat.Add(SeatList);
                    }
                }
            }
            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/AddSeats", ListOfSeat).Result;
        }
    }
}
