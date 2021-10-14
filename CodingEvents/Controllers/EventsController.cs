using CodingEvents.Data;
using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;

        public EventsController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = context.Events.ToList();
            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();
            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Type = addEventViewModel.Type
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);
            
        }

        public IActionResult Delete()
        {
            ViewBag.events = context.Events.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(int id in eventIds)
            {
                Event theEvent = context.Events.Find(id);
                context.Events.Remove(theEvent);
            }
            context.SaveChanges();
            return Redirect("/Events");
        }

        [HttpGet("/Events/Edit/{eventId?}")]
        public IActionResult Edit(int eventId)
        {
            ViewBag.evt = context.Events.Find(eventId);
            ViewBag.title = "Edit Event " + ViewBag.evt.Name + " (id = " + ViewBag.evt.Id + ")";
            return View();
        }

        [HttpPost("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            ViewBag.evt = context.Events.Find(eventId);
            ViewBag.evt.Name = name;
            ViewBag.evt.Description = description;

            return Redirect("/Events");
        }
    }
}
