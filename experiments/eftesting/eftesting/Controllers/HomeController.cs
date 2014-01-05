using eftesting.DAL;
using eftesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eftesting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            //using (var db = new NexttrackContext())
            //{
            //    var myFirstRoom = new Room
            //    {
            //        Name = "MyFirstRoom",
            //        CreatedDate = DateTime.UtcNow,
            //        LastModifiedDate = DateTime.UtcNow 
            //    };
            //    db.Rooms.Add(myFirstRoom);
            //    db.SaveChanges();
            //}

            return View();
        }
    }
}
