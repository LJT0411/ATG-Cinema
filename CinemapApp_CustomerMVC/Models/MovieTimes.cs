using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaApp.CustomerMVC.Models
{
    public class MovieTimes
    {
        [Key]
        public int MovieTimesID { get; set; }

        public DateTime MovieTimeStart { get; set; }

        public string MovieHallNo { get; set; }

        // FK
        public int MoviesID { get; set; }

        public virtual Movies Movies { get; set; }

        public virtual ICollection<MovieSeats> MovieSeats { get; set; }

    }
}