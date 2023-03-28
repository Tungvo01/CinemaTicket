using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaTicket.Identity
{
    public class AppUser
    {
        public DateTime? Birthday { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}