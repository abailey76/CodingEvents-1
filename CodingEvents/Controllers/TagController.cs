﻿using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Controllers
{
    public class TagController : Controller
    {
        private EventDbContext context;
        public TagController(EventDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            List<Tag> tags = context.Tags.ToList();
            return View(tags);
        }

        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View(tag);
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            if (ModelState.IsValid)
            {
                context.Tags.Add(tag);
                context.SaveChanges();
                return Redirect("/Tag");
            }
            return View("Add", tag);
        }

        public IActionResult AddEvent(int id)
        {
            Event theEvent = context.Events.Find(id);
            List<Tag> possibleTags = context.Tags.ToList();

            AddEventTagViewModel addEventTagViewModel = new AddEventTagViewModel(theEvent, possibleTags);
            return View(addEventTagViewModel);
        }

        [HttpPost]
        public IActionResult AddEvent(AddEventTagViewModel addEventTagViewModel)
        {
            if (ModelState.IsValid)
            {
                int eventId = addEventTagViewModel.EventId;
                int tagId = addEventTagViewModel.TagId;

                List<EventTag> existingItems = context.EventTags
                    .Where(et => et.EventId == eventId)
                    .Where(et => et.TagId == tagId)
                    .ToList();

                if(existingItems.Count == 0)
                {
                    EventTag eventTag = new EventTag
                    {
                        EventId = eventId,
                        TagId = tagId
                    };

                    context.EventTags.Add(eventTag);
                    context.SaveChanges();
                }
                return Redirect("/Events/Detail/" + eventId);
            }

            return View(addEventTagViewModel);
        }
    }
}