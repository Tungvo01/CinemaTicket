using CinemaTicket.Models;
using CinemaTicket.Models.CinemaModels;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaTicket.ViewModel
{
    public class HomeViewModel
    {
        private ApplicationDbContext db;
        public HomeViewModel()
        {
            db = new ApplicationDbContext();
        }
        public IEnumerable<MovieDetail> movieDetails { get; set; }
        public IEnumerable<Movie> movies { get; set; }

        public IEnumerable<Celebrity> celebrities { get; set; }

        //public string PageTitle { get; set; }
        //public string PageHeader { get; set; }

        //public List<Celebrity> GetCelebrityInMovie(int id)
        //{
        //    List<Celebrity> result = new List<Celebrity>();
        //    var h = db.MovieDetails.Where(movieid => movieid.MovieId = id).ToList();


        //    return result;
        //}
    }


}