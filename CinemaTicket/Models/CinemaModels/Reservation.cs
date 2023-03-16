using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public string CustomerId { get; set; }

        //Relationships
        public ApplicationUser Customer { get; set; }
        [ForeignKey("Seat")]
        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        [ForeignKey("Show")]
        public int ShowId { get; set; }
        public Show Show { get; set; }

    }
}