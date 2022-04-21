using DemoHangFire.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoHangFire.Controllers
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
            HomeViewModel homeViewModel = new();

            homeViewModel.EmailServiceModel = new EmailServiceModel();
            homeViewModel.ErrorViewModel =  null ;
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [Route("AddNewTask")]
        public IActionResult AddNewTask(EmailServiceModel emailServiceModel)
        {
            bool isSuccess=false;
            string message = "Service Scheduled successfully";
            ErrorViewModel errorViewModel = new()
            {
                IsSuccess = isSuccess,
                Message = message
            };
           
            try
            {

                string uniqueId = $"{emailServiceModel.ServiceName}-{Guid.NewGuid().ToString()}";
                RecurringJob.AddOrUpdate(uniqueId, () =>
                    SendEmail(emailServiceModel.ServiceName, emailServiceModel.ConnectionString),
                    Cron.MinuteInterval(emailServiceModel.TimeInterval), TimeZoneInfo.Local, emailServiceModel.ServiceName.ToLower());//At minute TimeInterval of every hour
            }
            catch (Exception ex)
            {
                errorViewModel.Message = "Service Scheduling failed" + " - "+ ex.Message;
            }

            HomeViewModel homeViewModel = new()
            {
                ErrorViewModel = errorViewModel,
                EmailServiceModel = emailServiceModel
            };

            return View(nameof(Index), homeViewModel);
        } 

        public void SendEmail(string svcName, string dbString)
        {
            //left for brevity
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
