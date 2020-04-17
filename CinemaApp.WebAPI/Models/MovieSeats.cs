using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaApp.WebAPI.Models
{
    public class MovieSeats
    {
        [Key]
        public int MovieSeatsID { get; set; }

        public string SeatNo { get; set; }

        public SAvail SeatAvail { get; set; }

        // FK
        public int? UsersID { get; set; }

        public virtual Users Users { get; set; }

        public int? MovieTimesID { get; set; }

        public virtual MovieTimes MovieTimes { get; set; }

    }
    public enum SAvail
    {
        E, T
    }
}