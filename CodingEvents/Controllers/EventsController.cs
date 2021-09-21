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
        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll());
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
                    ContactEmail = addEventViewModel.ContactEmail
                };

                EventData.Add(newEvent);

                return Redirect("/Events");
            }

            return View(addEventViewModel);
            
        }

        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(int id in eventIds)
            {
                EventData.Remove(id);
            }
            return Redirect("/Events");
        }

        [HttpGet("/Events/Edit/{eventId?}")]
        public IActionResult Edit(int eventId)
        {
            ViewBag.evt = EventData.GetById(eventId);
            ViewBag.title = "Edit Event " + ViewBag.evt.Name + " (id = " + ViewBag.evt.Id + ")";
            return View();
        }

        [HttpPost("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            ViewBag.evt = EventData.GetById(eventId);
            ViewBag.evt.Name = name;
            ViewBag.evt.Description = description;

            return Redirect("/Events");
        }
    }
}
