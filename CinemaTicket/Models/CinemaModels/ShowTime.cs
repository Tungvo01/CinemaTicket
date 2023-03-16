﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models.CinemaModels
{
    public class ShowTime
    {
        [Key]
        public int ShowTimeId { get; set; }

        public string Time { get; set; }

        //relationships
        public ICollection<Show> Shows { get; set; }

    }
}