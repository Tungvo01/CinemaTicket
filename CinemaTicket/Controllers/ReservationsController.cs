using CinemaTicket.Models;
using CinemaTicket.Models.CinemaModels;
using CinemaTicket.ViewModel;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicket.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db;
        public ReservationsController()
        {

            db = new ApplicationDbContext();
        }



        // GET: Reservations
        //public ActionResult Index(int MovieId)
        //{
        //    int MovieIds = int.Parse(Request.Params["MovieId"]);

        //    ViewBag.Reservations = db.Reservations.ToList();
        //    ViewBag.Shows = db.Shows.Where(p => p.MovieId == MovieId).ToList();

        //    ViewBag.ShowDays = new SelectList(db.Shows.Where(p => p.MovieId == MovieId).Include("ShowDays"), "ShowDayId", "ShowDayId");
        //    ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "ShowId");

        //    //day movie
        //    var ShowDays = db.ShowDays
        //        .Join(db.Shows,
        //            sh => sh.ShowDayId,
        //            s => s.ShowDayId,
        //            (sh, s) => new { ShowDays = sh, Shows = s })
        //        .Where(x => x.Shows.ShowId == ShowIds)
        //        .Select(x => x.ShowDays.Day).ToList();
        //    ViewBag.ShowDays = ShowDays;

        //    //show time
        //    var ShowTimes = db.ShowTimes
        //       .Join(db.Shows,
        //           sh => sh.ShowTimeId,
        //           s => s.ShowTimeId,
        //           (sh, s) => new { ShowTimes = sh, Shows = s })
        //       .Where(x => x.Shows.ShowId == ShowIds)
        //       .Select(x => x.ShowTimes.Time).ToList();
        //    ViewBag.ShowTimes = ShowTimes;

        //    //cinema show
        //    var Cinemas = db.Cinemas
        //       .Join(db.Shows,
        //           sh => sh.CinemaId,
        //           s => s.CinemaId,
        //           (sh, s) => new { Cinemas = sh, Shows = s })
        //       .Where(x => x.Shows.ShowId == ShowIds)
        //       .Select(x => x.Cinemas.CinemaName).ToList();

        //    ViewBag.Cimemas = Cinemas;

        //    return View();
        //}
        public ActionResult ChooseShow(int? MovieId, int? ShowId)
        {
            //int MovieIds = int.Parse(Request.Params["MovieId"]);
            //int ShowIds = int.Parse(Request.Params["ShowId"]);

            ViewBag.Movie = db.Movies.Find(MovieId);
            ViewBag.ShowDays = new SelectList(db.Shows.Where(p => p.MovieId == MovieId).Include("ShowDays"), "ShowDayId", "ShowDayId");

            //var reservationIds = (from r in db.Reservations
            //                      join s in db.Shows on r.ShowId equals s.ShowId
            //                      where s.MovieId == MovieId
            //                      select r.ReservationId).ToList();

            var showIds = db.Shows.Where(s => s.MovieId == MovieId).Select(s => s.ShowId);


            ViewBag.Re = showIds;
            //ViewBag.Reservations = new SelectList(reservationIds, "ReservationId", "ReservationId");
            //ViewBag.ShowId = new SelectList(reservationIds, "ShowId", "ShowId");
            //ViewBag.ShowId = new SelectList(db.Shows.Where(p=>p.MovieId == MovieId), "ShowId", "ShowId");


            ViewBag.ReservationId = db.Reservations.Where(c => c.ShowId == ShowId).ToList();
            //day movie
            var ShowDays = db.ShowDays
                .Join(db.Shows,
                    sh => sh.ShowDayId,
                    s => s.ShowDayId,
                    (sh, s) => new { ShowDays = sh, Shows = s })
                .Where(x => x.Shows.ShowId == ShowId)
                .Select(x => x.ShowDays.Day).ToList();
            ViewBag.ShowDays = ShowDays;

            //show time
            var ShowTimes = db.ShowTimes
               .Join(db.Shows,
                   sh => sh.ShowTimeId,
                   s => s.ShowTimeId,
                   (sh, s) => new { ShowTimes = sh, Shows = s })
               .Where(x => x.Shows.ShowId == ShowId)
               .Select(x => x.ShowTimes.Time).ToList();
            ViewBag.ShowTimes = ShowTimes;

            //cinema show
            var Cinemas = db.Cinemas
               .Join(db.Shows,
                   sh => sh.CinemaId,
                   s => s.CinemaId,
                   (sh, s) => new { Cinemas = sh, Shows = s })
               .Where(x => x.Shows.ShowId == ShowId)
               .Select(x => x.Cinemas.CinemaName).ToList();

            ViewBag.Cimemas = Cinemas;


            var seatIds = from r in db.Reservations
                          where r.ShowId == ShowId
                          orderby r.SeatId
                          select r.SeatId;

            ViewBag.SeatIds = seatIds;

            ViewBag.ShowId = ShowId.ToString();

            //ViewBag.statusList = statusList;
            //status = true;
            //db.SaveChanges();



            return View(db.Reservations.Where(c => c.ShowId == ShowId).ToList());
        }


        public ActionResult a(string datveJson)
        {
            datve book = JsonConvert.DeserializeObject<datve>(datveJson);


            return View(book);
        }
        public ActionResult ngon()
        {
            //lughe vao db
            //
            //savechanes
            

            return View();
        }

        public ActionResult kongon()
        {

            return View();
        }
        public ActionResult success(string seats)
        {

            return View();
        }

        public ActionResult PaymentWithPaypal(string datveJson, string price, string seats,string Cancel = null)
        {
            if(datveJson != null)
            {
                string[] arr = seats.Split(' ');
                datve datve = JsonConvert.DeserializeObject<datve>(datveJson);
                Reservation r;
                for (int i = 0; i < arr.Length; i++)
                {
                    r = new Reservation();

                    r.CustomerId = User.Identity.GetUserId();
                    r.SeatId = Int32.Parse(arr[i]);
                    r.ShowId = Int32.Parse(datve.ShowId);
                    db.Reservations.Add(r);
                    db.SaveChanges();
                }
            }
           

            //if (datve is null)
            //{
            //    throw new ArgumentNullException(nameof(datve));
            //}
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Reservations/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, datveJson, price);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session["payment"] = createdPayment.id;
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session["payment"] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("kongon");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("kongon");
            }
            //on successful payment, show success page to user.  
          
           
            return View("ngon");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string datveJson, string price)
        {
            datve datve = JsonConvert.DeserializeObject<datve>(datveJson);
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = datve.MovieName,
                currency = "USD",
                price = price,
                quantity = "1",
                sku = "sku"
            });


            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            //var details = new Details()
            //{
            //    tax = "1",
            //    shipping = "1",
            //    subtotal = "1"
            //};
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = price // Total must be equal to sum of tax, shipping and subtotal.  
                //details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "",
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }


    }
}