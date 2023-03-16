using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class ShowDay
    {
        [Key]
        public int ShowDayId { get; set; }

        public DateTime Day { get; set; }

        //relationships
        public ICollection<Show> Shows { get; set; }

    }
}