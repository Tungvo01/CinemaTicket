using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }


        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }


    }
}