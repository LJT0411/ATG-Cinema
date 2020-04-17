using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaApp.DomainModelEntity
{
    public class MovieHall
    {
        [Key]
        public int MovieHallID { get; set; }

        public string MovieHallNo { get; set; }

        public int Rows { get; set; }

        public int Column { get; set; }

        public int TotalSeats => Rows * Column;
    }
}