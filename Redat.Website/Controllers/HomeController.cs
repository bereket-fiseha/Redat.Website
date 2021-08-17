using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Redat.Website.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Redat.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult RequestMedicalService() {

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
       public IActionResult ChargeCustomer(string email,string stripeToken) {
            StripeConfiguration.ApiKey="sk_test_51JHqpuCUcptI1p6qyMiffBfLs5viql6q9HxRXqDidXX180GXllWRBpXD08Fq1IB9qjKXOv87SnGnBaSquTAHMBut00QJh7rNfw";
            var customer = new CustomerService().Create(

           new CustomerCreateOptions {
                Email = email,
                Source=stripeToken
            });
            var charge = new ChargeService().Create(new ChargeCreateOptions { 
            Amount=200,
            Description="payment",
            Customer=customer.Id,
            Currency="usd"
            });
            if (charge.Status == "succeeded")
            {
                TempData["status"] = "success";
            }
            else
                TempData["status"] = "error";
           return  RedirectToAction("Index");

        
       }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
