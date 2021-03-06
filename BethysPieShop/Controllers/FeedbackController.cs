﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BethysPieShop.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentPage = "Feedback";
            return View();
        }

        [HttpPost]
        public IActionResult Index(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _feedbackRepository.AddFeedback(feedback);
                return RedirectToAction("FeedbackComplete");
            }
            //Return the user to the form with the same data still in it, if validation fails.
            return View(feedback);
        }

        public IActionResult FeedbackComplete()
        {
            return View();
        }
    }
}