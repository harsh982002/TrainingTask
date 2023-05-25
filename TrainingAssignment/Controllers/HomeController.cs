using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using TrainingAssignment.Entities.Models;
using TrainingAssignment.Entities.ViewModels;
using TrainingAssignment.Helpers;
using TrainingAssignment.Models;
using TrainingAssignment.Repository.Interface;

namespace TrainingAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAllRepository _allRepository;
        private readonly INotyfService _notyf;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IAllRepository allRepository, INotyfService notyf, IConfiguration configuration)
        {
            _logger = logger;
            _allRepository = allRepository;
            _notyf = notyf;
            _configuration = configuration;
        }

        [Authorization]
        [Route("/Home/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login");
        }

        [Authorization]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Authorization]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string? emailId)
        {
            if (!string.IsNullOrEmpty(emailId))
            {
                UserLoginModel model = new UserLoginModel();
                model.Email = emailId;
                return View(model);
            }
            var token = HttpContext.Request.Cookies["JwtToken"]?.ToString();
            if (token != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _allRepository.accountRepository.Login(model);
                    if (user is null)
                    {
                        return RedirectToAction("Registration", new { emailId = model.Email });
                    }
                    else if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password) is false)
                    {
                        ViewBag.Message = String.Format("Wrong Credentials!");
                        return View();
                    }
                    else
                    {
                        bool verify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                        if (verify)
                        {
                            var jwtSettings = _configuration.GetSection(nameof(JwtViewModel)).Get<JwtViewModel>();
                            var token = JwtHelper.GenerateToken(jwtSettings, user);
                            if (token is not null)
                            {
                                HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.Now.AddMinutes(1) });
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                return RedirectToAction("Login");
                            }
                        }
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [AllowAnonymous]
        public IActionResult Registration(string? emailId)
        {
            UserRegisterModel model = new UserRegisterModel();
            if (!string.IsNullOrEmpty(emailId))
            {
                model.Email = emailId;
                model.countries = _allRepository.listRepository.GetInitialDetail();
                return View(model);
            }
            model.countries = _allRepository.listRepository.GetInitialDetail();
            return View(model);
        }

        [Route("/Home/getstates")]
        [HttpPost]
        public JsonResult GetStates(long country)
        {
            var States = _allRepository.listRepository.GetStates(country);
            return Json(States);
        }

        [Route("/Home/city")]
        [HttpPost]
        public JsonResult GetCity(long states)
        {
            var City = _allRepository.listRepository.GetCity(states);
            return Json(City);
        }

        [AllowAnonymous]
        [Route("/Home/Registration")]
        [HttpPost]
        public IActionResult Registration(UserRegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_allRepository.accountRepository.IsValidUserEmail(model))
                    {
                        User registration = _allRepository.accountRepository.Registration(model);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        _notyf.Custom("Already registered, please login directly!", 5, "whitesmoke", "fa fa-gear");
                        return RedirectToAction("Login", new { emailId = model.Email });
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UnAuthorize()
        {
            return View();
        }
    }
}