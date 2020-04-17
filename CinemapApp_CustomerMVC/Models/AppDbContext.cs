using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CinemaApp.CustomerMVC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("CinemaApp2020")
        {

        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Movies> Movies { get; set; }

        public DbSet<MovieTimes> MovieTimes { get; set; }

        public DbSet<MovieHall> MovieHall { get; set; }

        public DbSet<MovieSeats> MovieSeats { get; set; }
    }
}