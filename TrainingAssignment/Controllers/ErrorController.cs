using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrainingAssignment.Models;

namespace TrainingAssignment.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/HttpError")]
        public IActionResult HttpError()
        {
            return View();
        }

        [Route("/Error/NotImplementedError")]
        public IActionResult NotImplementedError()
        {
            return View();
        }

        [Route("/Error/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
