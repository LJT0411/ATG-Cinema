using CinemaApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemaApp.WebAPI.Controllers
{
    public class AdminsController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpGet List Methods

        [Route("api/Admins/GetUsers")]
        public IEnumerable<Users> GetUsers()
        {
            return db.Users.ToList();
        }

        [Route("api/Admins/GetMovies")]
        public IEnumerable<Movies> GetMovies()
        {
            return db.Movies.ToList();
        }

        [Route("api/Admins/GetHalls")]
        public IEnumerable<MovieHall> GetHalls()
        {
            return db.MovieHall.ToList();
        }

        [Route("api/Admins/GetMovieHalls")]
        public IEnumerable<MovieTimes> GetMovieHalls()
        {
            return db.MovieTimes.ToList();
        }

        [Route("api/Admins/GetSeats")]
        public IEnumerable<MovieSeats> GetSeats()
        {
            return db.MovieSeats.ToList();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpGet Get singular Methods
        // For Admin Console

        [Route("api/Admins/GetMovieByID/{id:int}")]
        public Movies GetMovieByID(int id)
        {
            var checkMovie = db.Movies.Where(c => c.MoviesID == id).SingleOrDefault();
            return checkMovie;
        }

        [Route("api/Admins/GetMovieTimesByID/{title}")]
        public Movies GetMovieTitleByTitle(string title)
        {
            var checkMovieTitle = db.Movies.Where(c => c.MovieTitle == title).SingleOrDefault();
            return checkMovieTitle;
        }

        [Route("api/Admins/GetHallByNo/{HallNo}")]
        public MovieHall GetHallByNo(string HallNo)
        {
            var checkHall = db.MovieHall.Where(c => c.MovieHallNo == HallNo).SingleOrDefault();

            return checkHall;
        }

        [Route("api/Admins/GetMovieTimesByID/{id:int}")]
        public MovieTimes GetMovieTimesByID(int id)
        {
            var checkTimes = db.MovieTimes.Where(c => c.MovieTimesID == id).SingleOrDefault();
            return checkTimes;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpPost Methods
        // For Admin Console

        [Route("api/Admins/AddUsers/")]
        [HttpPost]
        public void AddUsers(List<Users> users)
        {
            var FindUser = db.Users.Where(c => c.Username == "tgv" || c.Username == "gsc").ToList();

            if (FindUser.Count() == 0)
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }

        [Route("api/Admins/AddMovies/")]
        [HttpPost]
        public void AddMovies(List<Movies> movies)
        {
            db.Movies.AddRange(movies);
            db.SaveChanges();
        }

        [Route("api/Admins/AddMovieTimes/")]
        [HttpPost]
        public void AddMovieTimes(List<MovieTimes> movieTimes)
        {
            db.MovieTimes.AddRange(movieTimes);
            db.SaveChanges();
        }

        [Route("api/Admins/AddMovieHalls/")]
        [HttpPost]
        public void AddMovieHalls(List<MovieHall> movieHalls)
        {
            db.MovieHall.AddRange(movieHalls);
            db.SaveChanges();
        }

        [Route("api/Admins/AddSeats/")]
        [HttpPost]
        public void AddSeats(List<MovieSeats> movieSeats)
        {
            db.MovieSeats.AddRange(movieSeats);
            db.SaveChanges();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpDelete Method
        // For Admin Console

        [Route("api/Admins/ClearAll")]
        [HttpDelete]
        public void ClearAll()
        {
            db.Users.RemoveRange(db.Users.ToList());
            db.Movies.RemoveRange(db.Movies.ToList());
            db.MovieHall.RemoveRange(db.MovieHall.ToList());
            db.MovieTimes.RemoveRange(db.MovieTimes.ToList());
            db.MovieSeats.RemoveRange(db.MovieSeats.ToList());
            db.SaveChanges();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpGet Methods
        // For Customer MVC

        [Route("api/Admins/GetSeatsByID/{timeID:int}")]
        public IEnumerable<MovieSeats> GetSeatsByID(int timeID)
        {
            var getSeats = db.MovieSeats.Where(c => c.MovieTimesID == timeID).ToList();

            return getSeats;
        }

        [Route("api/Admins/GetSeatByID/{seatID:int}")]
        public MovieSeats GetSeatByID(int seatID)
        {
            var getSeat = db.MovieSeats.Where(c => c.MovieSeatsID == seatID).SingleOrDefault();

            return getSeat;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpPost Methods
        // For Customer MVC

        [Route("api/Admins/Login/")]
        [HttpPost]
        public Users Login(Users users)
        {
            Users checkUser = db.Users.Where(c => c.Username == users.Username && c.Password == users.Password).SingleOrDefault();

            return checkUser;
        }

        [Route("api/Admins/GetUser/")]
        [HttpPost]
        public Users GetUser(Users users)
        {
            Users checkUser = db.Users.Where(c => c.Username == users.Username || c.Email == users.Email).SingleOrDefault();

            return checkUser;
        }

        [Route("api/Admins/SignUp/")]
        [HttpPost]
        public void SignUp(Users users)
        {
            db.Users.Add(users);
            db.SaveChanges();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpPut Methods
        // For Customer MVC

        [Route("api/Admins/UpdateSeatDetail/")]
        [HttpPut]
        public void UpdateSeatDetail(MovieSeats seatDetails)
        {
            db.Entry(seatDetails).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
