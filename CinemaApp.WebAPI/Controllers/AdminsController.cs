using CinemaApp.DomainModelEntity;
using CinemaApp.Persistence.Repositories;
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
        private CinemaAppRepository db = new CinemaAppRepository();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpGet List Methods

        [Route("api/Admins/GetUsers")]
        public IEnumerable<Users> GetUsers()
        {
            return db.GetUsers();
        }

        [Route("api/Admins/GetMovies")]
        public IEnumerable<Movies> GetMovies()
        {
            return db.GetMovies();
        }

        [Route("api/Admins/GetHalls")]
        public IEnumerable<MovieHall> GetHalls()
        {
            return db.GetHalls();
        }

        [Route("api/Admins/GetMovieHalls")]
        public IEnumerable<MovieTimes> GetMovieHalls()
        {
            return db.GetMovieHalls();
        }

        [Route("api/Admins/GetSeats")]
        public IEnumerable<MovieSeats> GetSeats()
        {
            return db.GetSeats();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpGet Get singular Methods
        // For Admin Console

        [Route("api/Admins/GetMovieByID/{id:int}")]
        public Movies GetMovieByID(int id)
        {
            return db.GetMovieByID(id);
        }

        [Route("api/Admins/GetMovieTimesByID/{title}")]
        public Movies GetMovieTitleByTitle(string title)
        {
            return db.GetMovieTitleByTitle(title);
        }

        [Route("api/Admins/GetHallByNo/{HallNo}")]
        public MovieHall GetHallByNo(string HallNo)
        {
            return db.GetHallByNo(HallNo);
        }

        [Route("api/Admins/GetMovieTimesByID/{id:int}")]
        public MovieTimes GetMovieTimesByID(int id)
        {
            return db.GetMovieTimesByID(id);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpPost Methods
        // For Admin Console

        [Route("api/Admins/AddUsers/")]
        [HttpPost]
        public void AddUsers(List<Users> users)
        {
            db.AddUsers(users);
        }

        [Route("api/Admins/AddMovies/")]
        [HttpPost]
        public void AddMovies(List<Movies> movies)
        {
            db.AddMovies(movies);
        }

        [Route("api/Admins/AddMovieTimes/")]
        [HttpPost]
        public void AddMovieTimes(List<MovieTimes> movieTimes)
        {
            db.AddMovieTimes(movieTimes);
        }

        [Route("api/Admins/AddMovieHalls/")]
        [HttpPost]
        public void AddMovieHalls(List<MovieHall> movieHalls)
        {
            db.AddMovieHalls(movieHalls);
        }

        [Route("api/Admins/AddSeats/")]
        [HttpPost]
        public void AddSeats(List<MovieSeats> movieSeats)
        {
            db.AddSeats(movieSeats);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpDelete Method
        // For Admin Console

        [Route("api/Admins/ClearAll")]
        [HttpDelete]
        public void ClearAll()
        {
            db.ClearAll();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpGet Methods
        // For Customer MVC

        [Route("api/Admins/GetSeatsByTimeID/{timeID:int}")]
        public IEnumerable<MovieSeats> GetSeatsByTimeID(int timeID)
        {
            return db.GetSeatsByTimeID(timeID);
        }

        [Route("api/Admins/GetSeatByUserID/{userID:int}")]
        public IEnumerable<MovieSeats> GetSeatByUserID(int userID)
        {
            return db.GetSeatByUserID(userID);
        }

        [Route("api/Admins/GetSeatBySeatID/{seatID:int}")]
        public MovieSeats GetSeatBySeatID(int seatID)
        {
            return db.GetSeatBySeatID(seatID);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpPost Methods
        // For Customer MVC

        [Route("api/Admins/Login/")]
        [HttpPost]
        public Users Login(Users users)
        {
            return db.Login(users);
        }

        [Route("api/Admins/GetUser/")]
        [HttpPost]
        public Users GetUser(Users users)
        {
            return db.GetUser(users);
        }

        [Route("api/Admins/SignUp/")]
        [HttpPost]
        public void SignUp(Users users)
        {
            db.SignUp(users);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HttpPut Methods
        // For Customer MVC

        [Route("api/Admins/UpdateSeatDetail/")]
        [HttpPut]
        public void UpdateSeatDetail(MovieSeats seatDetails)
        {
            db.UpdateSeatDetail(seatDetails);
        }
    }
}
