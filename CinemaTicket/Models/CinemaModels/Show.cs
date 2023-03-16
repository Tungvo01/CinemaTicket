using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class Show
    {
        [Key]
        public int ShowId { get; set; }
        
        //relationships
        public ICollection<Reservation> Reservations { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("ShowDay")]
        public int ShowDayId { get; set; }
        public ShowDay ShowDay { get; set; }


        [ForeignKey("ShowTime")]
        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; }
    }
}