using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        //relationships
        public ICollection<Show> Shows { get; set; }

        public ICollection<News> News { get; set; }


    }
}