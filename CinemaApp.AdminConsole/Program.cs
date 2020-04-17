using CinemaApp.WebAPI;
using CinemaApp.WebAPI.Models;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaApp.AdminConsole
{
    class Program
    {
        private static AllData data = new AllData();
        private const string controllerName = "Admins";
        static HttpResponseMessage response;

        static void Main(string[] args)
        {
            IEnumerable<Users> userList;
            IEnumerable<Movies> movieList;
            IEnumerable<MovieHall> hallList;
            IEnumerable<MovieTimes> movietimeList;
            IEnumerable<MovieSeats> seatList;

            bool menu = true;
            while (menu)
            {
                Console.WriteLine("1. Clear all data");
                Console.WriteLine("2. Initialize data");
                Console.WriteLine("     2a. Users");
                Console.WriteLine("     2b. Movies");
                Console.WriteLine("     2c. Halls");
                Console.WriteLine("     2d. Movie hall");
                Console.WriteLine("     2e. Movie hall details");
                Console.WriteLine("3. Print data");
                Console.WriteLine("     3a. Print all users");
                Console.WriteLine("     3b. Print all movies");
                Console.WriteLine("     3c. Print all halls");
                Console.WriteLine("     3d. Print all movie halls");
                Console.WriteLine("     3e. Print all movie halls details");
                Console.Write("\nEnter your option : ");
                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        response = GlobalVariables.WebApiClient.DeleteAsync($"{controllerName}/ClearAll").Result;
                        Console.WriteLine("All data removed");
                        ClearMsg();
                        break;

                    case "2":
                        response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetUsers").Result;
                        userList = response.Content.ReadAsAsync<IEnumerable<Users>>().Result;

                        var FindUser = userList.Where(c => c.Username == "tgv" || c.Username == "gsc").ToList();

                        if (FindUser.Count() != 0)
                        {
                            Console.WriteLine("\nAlready have sample data.");
                        }
                        else
                        {
                            data.GenerateUser();
                            data.GenerateMovie();
                            data.GenerateMovieHall();
                            data.GenerateMovieTime();
                            data.GenerateMovieSeats();
                            Console.WriteLine("\nData Added");
                        }
                        ClearMsg();
                        break;

                    case "3a":
                        response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetUsers").Result;
                        userList = response.Content.ReadAsAsync<IEnumerable<Users>>().Result;
                        PrintUsers(userList);
                        ClearMsg();
                        break;

                    case "3b":
                        response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovies").Result;
                        movieList = response.Content.ReadAsAsync<IEnumerable<Movies>>().Result;
                        PrintMovies(movieList);
                        ClearMsg();
                        break;

                    case "3c":
                        response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetHalls").Result;
                        hallList = response.Content.ReadAsAsync<IEnumerable<MovieHall>>().Result;
                        PrintHalls(hallList);
                        ClearMsg();
                        break;

                    case "3d":
                        response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieHalls").Result;
                        movietimeList = response.Content.ReadAsAsync<IEnumerable<MovieTimes>>().Result;
                        PrintMovieHalls(movietimeList);
                        ClearMsg();
                        break;

                    case "3e":
                        response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetSeats").Result;
                        seatList = response.Content.ReadAsAsync<IEnumerable<MovieSeats>>().Result;

                        response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieHalls").Result;
                        movietimeList = response.Content.ReadAsAsync<IEnumerable<MovieTimes>>().Result;
                        PrintMovieSeats(movietimeList,seatList);
                        ClearMsg();
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        ClearMsg();
                        break;
                }
            }
        }

        public static void PrintUsers(IEnumerable<Users> users)
        {
            var table = new ConsoleTable("Id", "Username", "Email");

            if (users.Count() != 0)
            {
                foreach (var item in users)
                {
                    table.AddRow(item.UsersID, item.Username, item.Email);
                }
                table.Write();
            }
            else
            {
                Console.WriteLine("\nDoesn't have any user\n");
            }
        }

        public static void PrintMovies(IEnumerable<Movies> movies)
        {
            var table = new ConsoleTable("Movie Title", "Release Date", "Status");

            if (movies.Count() != 0)
            {
                foreach (var item in movies)
                {
                    table.AddRow(item.MovieTitle,
                                 $"{item.MovieReleaseTime.DayOfWeek} {item.MovieReleaseTime.ToString("dd MMMM yyyy")}",
                                 DisplayOP(item.MovieAvailable));
                }
                table.Write();
            }
            else
            {
                Console.WriteLine("\nDoesn't have any movies\n");
            }
        }

        public static void PrintHalls(IEnumerable<MovieHall> moviehalls)
        {
            var table = new ConsoleTable("Hall ID", "Hall No", "Total Seats");

            if (moviehalls.Count() != 0)
            {
                foreach (var item in moviehalls)
                {
                    table.AddRow(item.MovieHallID,
                                 item.MovieHallNo,
                                 item.TotalSeats);
                }
                table.Write();
            }
            else
            {
                Console.WriteLine("\nDoesn't have any halls\n");
            }
        }

        public static void PrintMovieHalls(IEnumerable<MovieTimes> movietimes)
        {
            var table = new ConsoleTable("ID", "Movie Title", "Start Date", "Hall No");

            if (movietimes.Count() != 0)
            {
                foreach (var item in movietimes)
                {
                    response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieByID/{item.MoviesID}").Result;
                    var checkMovieTitle = response.Content.ReadAsAsync<Movies>().Result;

                    table.AddRow(item.MovieTimesID,
                                 checkMovieTitle.MovieTitle,
                                 item.MovieTimeStart,
                                 item.MovieHallNo);
                }
                table.Write();
            }
            else
            {
                Console.WriteLine("\nDoesn't have any movie halls\n");
            }
        }

        public static void PrintMovieSeats(IEnumerable<MovieTimes> movieTimes,IEnumerable<MovieSeats> seats)
        {
            if (movieTimes.Count() != 0)
            {
                var Rows = new ConsoleTable("T: Taken", "E: Empty");

                Rows.Options.EnableCount = false;
                Rows.Write();

                foreach (var item in movieTimes)
                {
                    response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieByID/{item.MoviesID}").Result;
                    var checkMovieTitle = response.Content.ReadAsAsync<Movies>().Result;

                    Console.WriteLine("Hall No : " + item.MovieHallNo);
                    Console.WriteLine("Movie Title : " + checkMovieTitle.MovieTitle);
                    Console.WriteLine("Movie Start Time : " + item.MovieTimeStart);

                    var checkSeats = seats.Where(c => c.MovieTimesID == item.MovieTimesID).ToList();

                    foreach (var Seat in checkSeats)
                    {
                        Console.Write(Seat.SeatNo + " " + Seat.SeatAvail + "\t");
                        if (Seat.SeatNo.EndsWith("1,10"))
                            Console.WriteLine("\n");
                        if (Seat.SeatNo.EndsWith("2,10"))
                            Console.WriteLine("\n");
                        if (Seat.SeatNo.EndsWith("3,10"))
                            Console.WriteLine("\n");
                        if (Seat.SeatNo.EndsWith("4,10"))
                            Console.WriteLine("\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nDoesn't have any seats\n");
            }
        }

        public static string DisplayOP(MAvail mvail)
        {
            switch (mvail)
            {
                case MAvail.NowShowing:
                    return "Now Showing";
                case MAvail.ComingSoon:
                    return "Coming Soon";
                default:
                    return "";
            }
        }

        public static void ClearMsg()
        {
            Console.ReadLine();
            Console.Clear();
        }
    }
}
