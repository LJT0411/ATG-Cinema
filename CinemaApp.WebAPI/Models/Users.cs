using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.WebAPI.Models
{
    public class Users
    {
        public int UsersID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<MovieSeats> MovieSeatDetails { get; set; }
    }
}