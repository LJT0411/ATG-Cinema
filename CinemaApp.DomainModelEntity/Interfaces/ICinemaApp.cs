using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DomainModelEntity.Interfaces
{
    public interface ICinemaApp
    {
        // Get all data inside a list without using linq
        IEnumerable<Users> GetUsers();

        IEnumerable<Movies> GetMovies();

        IEnumerable<MovieHall> GetHalls();

        IEnumerable<MovieTimes> GetMovieHalls();

        IEnumerable<MovieSeats> GetSeats();

        // HttpGet Get Singular Data
        Movies GetMovieByID(int id);

        Movies GetMovieTitleByTitle(string title);

        MovieHall GetHallByNo(string HallNo);

        MovieTimes GetMovieTimesByID(int id);

        // HttpPost Add Data
        void AddUsers(List<Users> users);

        void AddMovies(List<Movies> movies);

        void AddMovieTimes(List<MovieTimes> movieTimes);

        void AddMovieHalls(List<MovieHall> movieHalls);

        void AddSeats(List<MovieSeats> movieSeats);

        void ClearAll();


        // HttpGet Find Data To List

        IEnumerable<MovieSeats> GetSeatsByTimeID(int timeID);

        IEnumerable<MovieSeats> GetSeatByUserID(int userID);

        MovieSeats GetSeatBySeatID(int seatID);

        // HttpPost (Customer)

        Users Login(Users users);

        Users GetUser(Users users);

        void SignUp(Users users);

        void UpdateSeatDetail(MovieSeats seatDetails);

        void Save();
    }
}
