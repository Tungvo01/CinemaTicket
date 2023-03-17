using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class Celebrity
    {
        [Key]
        public int CelebrityId { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }

        public string Weight { get; set; }

        public string UrlAvatar { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public ICollection<MovieDetail> MovieDetails { get; set; }



    }
}