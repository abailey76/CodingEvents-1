using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodingEvents.Controllers
{
    public class RSVPController : Controller
    {
        static private Dictionary<string, string> Attendees = new Dictionary<string, string>();

        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.events = Attendees;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("/RSVP/Add")]
        public IActionResult RSVP(string name, string response)
        {
            Attendees.Add(name, response);
            return Redirect("/RSVP");
        }
    }
}