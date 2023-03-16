using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }

        public int SeatNo { get; set; }

        public bool Status { get; set; }

        public double Price { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}