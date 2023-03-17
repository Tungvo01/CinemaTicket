using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class MovieDetail
    {
        [Key]
        public int MovieDetailId { get; set; }
        public int MovieId { get; set; }
        public int CelebrityId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Celebrity Celebrity { get; set; }
    }
}