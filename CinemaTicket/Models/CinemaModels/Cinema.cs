using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class Cinema
    {
        [Key]
        public int CinemaId { get; set; }

        public string CinemaName { get; set; }

        public string Description { get; set; }

        //relationships
        public ICollection<Show> Shows { get; set; }
    }
}