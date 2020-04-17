using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaApp.WebAPI.Models
{
    public class Movies
    {
        [Key]
        public int MoviesID { get; set; }

        public string MovieTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd dd MMM yyyy}")]
        public DateTime MovieReleaseTime { get; set; }

        public string Duration { get; set; }

        public MAvail MovieAvailable { get; set; }

        // FK
        public virtual ICollection<MovieTimes> MovieTimes { get; set; }
    }

    public enum MAvail
    {
        [Display(Name = "Now Showing")]
        NowShowing,
        [Display(Name = "Coming Soon")]
        ComingSoon
    }
}