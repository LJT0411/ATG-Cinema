using CinemaApp.DomainModelEntity;
using CinemaApp.DomainModelEntity.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Persistence.Repositories
{
    public class CinemaAppRepository : ICinemaApp
    {
        private AppDbContext db = new AppDbContext();

        // Get all data inside a list without using linq

        public IEnumerable<Users> GetUsers()
        {
            return db.Users.ToList();
        }

        public IEnumerable<Movies> GetMovies()
        {
            return db.Movies.ToList();
        }

        public IEnumerable<MovieHall> GetHalls()
        {
            return db.MovieHall.ToList();
        }

        public IEnumerable<MovieTimes> GetMovieHalls()
        {
            return db.MovieTimes.ToList();
        }

        public IEnumerable<MovieSeats> GetSeats()
        {
            return db.MovieSeats.ToList();
        }

        // HttpGet Get Singular Data

        public Movies GetMovieByID(int id)
        {
            var checkMovie = db.Movies.Where(c => c.MoviesID == id).SingleOrDefault();
            return checkMovie;
        }

        public Movies GetMovieTitleByTitle(string title)
        {
            var checkMovieTitle = db.Movies.Where(c => c.MovieTitle == title).SingleOrDefault();
            return checkMovieTitle;
        }

        public MovieHall GetHallByNo(string HallNo)
        {
            var checkHall = db.MovieHall.Where(c => c.MovieHallNo == HallNo).SingleOrDefault();
            return checkHall;
        }

        public MovieTimes GetMovieTimesByID(int id)
        {
            var checkTimes = db.MovieTimes.Where(c => c.MovieTimesID == id).SingleOrDefault();
            return checkTimes;
        }

        // HttpPost Add Data (Admin)

        public void AddUsers(List<Users> users)
        {
            var FindUser = db.Users.Where(c => c.Username == "tgv" || c.Username == "gsc").ToList();

            if (FindUser.Count() == 0)
            {
                db.Users.AddRange(users);
                Save();
            }
        }

        public void AddMovies(List<Movies> movies)
        {
            db.Movies.AddRange(movies);
            Save();
        }

        public void AddMovieTimes(List<MovieTimes> movieTimes)
        {
            db.MovieTimes.AddRange(movieTimes);
            Save();
        }

        public void AddMovieHalls(List<MovieHall> movieHalls)
        {
            db.MovieHall.AddRange(movieHalls);
            Save();
        }

        public void AddSeats(List<MovieSeats> movieSeats)
        {
            db.MovieSeats.AddRange(movieSeats);
            Save();
        }

        public void ClearAll()
        {
            db.Users.RemoveRange(db.Users.ToList());
            db.Movies.RemoveRange(db.Movies.ToList());
            db.MovieHall.RemoveRange(db.MovieHall.ToList());
            db.MovieTimes.RemoveRange(db.MovieTimes.ToList());
            db.MovieSeats.RemoveRange(db.MovieSeats.ToList());
            Save();
        }

        // HttpGet Find Data To List (Customer)

        public IEnumerable<MovieSeats> GetSeatsByTimeID(int timeID)
        {
            var getSeats = db.MovieSeats.Where(c => c.MovieTimesID == timeID).ToList();
            return getSeats;
        }

        public IEnumerable<MovieSeats> GetSeatByUserID(int userID)
        {
            var getSeat = db.MovieSeats.Where(c => c.UsersID == userID).ToList();
            return getSeat;
        }

        public MovieSeats GetSeatBySeatID(int seatID)
        {
            var getSeat = db.MovieSeats.Where(c => c.MovieSeatsID == seatID).SingleOrDefault();
            return getSeat;
        }

        // HttpPost (Customer)

        public Users Login(Users users)
        {
            Users checkUser = db.Users.Where(c => c.Username == users.Username && c.Password == users.Password).SingleOrDefault();
            return checkUser;
        }

        public Users GetUser(Users users)
        {
            Users checkUser = db.Users.Where(c => c.Username == users.Username || c.Email == users.Email).SingleOrDefault();
            return checkUser;
        }

        public void SignUp(Users users)
        {
            db.Users.Add(users);
            Save();
        }

        // HttpPut (Customer)

        public void UpdateSeatDetail(MovieSeats seatDetails)
        {
            db.Entry(seatDetails).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
