using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CinemaApp.AdminConsole;
using CinemaApp.DomainModelEntity;

namespace CinemapApp_CustomerMVC.Controllers
{
    public class ATGCinemaController : Controller
    {
        private const string controllerName = "Admins";
        static HttpResponseMessage response;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users users)
        {
            // Check User Valid or not

            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/Login", users).Result;
            var checkUser = response.Content.ReadAsAsync<Users>().Result;

            if (checkUser == null)
            {
                ViewbagError("Invalid Username or Password");
                return View();
            }

            // If correct, it will store the ID and Username

            Session["CustomerID"] = checkUser.UsersID;
            Session["Username"] = checkUser.Username;
            return RedirectToAction("Movies");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Users users)
        {
            // Check Username and Email got duplicate or not

            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/GetUser", users).Result;
            var checkUser = response.Content.ReadAsAsync<Users>().Result;

            if (checkUser != null)
            {
                ViewbagError("Username or Email is Duplicated!");
                return View();
            }

            // If no duplicate, it will send to the web api and add the new data

            response = GlobalVariables.WebApiClient.PostAsJsonAsync($"{controllerName}/SignUp", users).Result;
            ViewbagSuccess("Registered Successfully");
            return View();
        }

        public ActionResult Movies()
        {
            // Get all movies from web api and return to the view

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovies").Result;
            var MoviesList = response.Content.ReadAsAsync<IEnumerable<Movies>>().Result;

            return View(MoviesList);
        }

        public ActionResult MovieShowTimes()
        {
            // Get all movies start time from web api and return to the view

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieHalls").Result;
            var MovieTimesList = response.Content.ReadAsAsync<IEnumerable<MovieTimes>>().Result;

            return View(MovieTimesList);
        }

        public ActionResult SelectMovie(int? movieID)
        {
            // Before select a movie, it will check u have sign in or not first

            if (Session["CustomerID"] == null)
            {
                return RedirectToAction("Login");
            }
            // Get all movies start time

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieHalls").Result;
            var MovieTimesList = response.Content.ReadAsAsync<IEnumerable<MovieTimes>>().Result;

            // Find which movie start time is related to the selected movie

            var GetTimeList = MovieTimesList.Where(c => c.MoviesID == movieID).ToList();

            return View(GetTimeList);
        }

        public ActionResult MovieDetails(int? movieID)
        {
            // Check the selected movie details

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetMovieByID/{movieID}").Result;
            var Movie = response.Content.ReadAsAsync<Movies>().Result;

            return View(Movie);
        }

        public ActionResult SelectedTimeSeats(int? timeID)
        {
            // Before select a movie, it will check u have sign in or not first

            if (Session["CustomerID"] == null)
            {
                return RedirectToAction("Login");
            }

            // Get all the seats which are related to the selected time

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetSeatsByTimeID/{timeID}").Result;
            var MovieSeats = response.Content.ReadAsAsync<IEnumerable<MovieSeats>>().Result;

            return View(MovieSeats);
        }

        public ActionResult BuyTicket(int? seatID)
        {
            // Check the selected seat details

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetSeatBySeatID/{seatID}").Result;
            var SeatDetails = response.Content.ReadAsAsync<MovieSeats>().Result;

            // If the selected seat is taken thn will go in this if statement

            if (SeatDetails.SeatAvail == SAvail.T)
            {
                ViewbagError("This seat was taken. Please choose another seat.");
                return View(SeatDetails);
            }
            return View(SeatDetails);
        }

        public ActionResult ConfirmPayment(int? seatID)
        {
            var GetUserID = Convert.ToInt32(Session["CustomerID"]);

            // Once clicked confirm buy now it will update the seat details

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetSeatBySeatID/{seatID}").Result;
            var SeatDetails = response.Content.ReadAsAsync<MovieSeats>().Result;

            SeatDetails.UsersID = GetUserID;
            SeatDetails.SeatAvail = SAvail.T;

            // Here is update the seat

            response = GlobalVariables.WebApiClient.PutAsJsonAsync($"{controllerName}/UpdateSeatDetail", SeatDetails).Result;
            ViewbagSuccess("Purchase Success! Please come again!");
            return View();
        }

        public ActionResult BookingHistory()
        {
            // Before select a movie, it will check u have sign in or not first

            if (Session["CustomerID"] == null)
            {
                return RedirectToAction("Login");
            }
            var GetUserID = Convert.ToInt32(Session["CustomerID"]);

            // Get all history which are bought from the user

            response = GlobalVariables.WebApiClient.GetAsync($"{controllerName}/GetSeatByUserID/{GetUserID}").Result;
            var SeatDetails = response.Content.ReadAsAsync<IEnumerable<MovieSeats>>().Result;
            
            // If the user doesn't have any history, it will go in this if statement

            if (SeatDetails.Count() == 0)
            {
                ViewbagError("You have no history. Please search for movie and buy ticket.");
                return View();
            }
            return View(SeatDetails);
        }

        public ActionResult Logout()
        {
            return View();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Non Actions

        [NonAction]
        public void ViewbagError(string Msg)
        {
            ViewBag.Error = Msg;
        }

        [NonAction]
        public void ViewbagSuccess(string Msg)
        {
            ViewBag.Success = Msg;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
