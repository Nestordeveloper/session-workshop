using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using session_workshop.Models;

namespace session_workshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Submission(User survey)
    {
        if (ModelState.IsValid)
        {
            Random random = new Random();
            int minRange = 1;
            int maxRange = 100;
            int randomNumber = random.Next(minRange, maxRange);
            HttpContext.Session.SetInt32("RandomNumber", randomNumber);
            HttpContext.Session.SetString("Name", survey.UserName);
            return RedirectToAction("Dashboard");
        }
        else
        {
            return View("Login", survey);
        }
    }

    [HttpGet]
    [Route("dashboard")]
    public IActionResult Dashboard()
    {
        return View("Dashboard");
    }

    [HttpPost]
    public IActionResult Operacion(string operation)
    {
        int randomNumber = HttpContext.Session.GetInt32("RandomNumber") ?? 0;

        if (operation == "sumar")
        {
            randomNumber += 1;
        }
        else if (operation == "restar")
        {
            randomNumber -= 1;
        }
        else if (operation == "multiplicar")
        {
            randomNumber *= 2;
        }
        else if (operation == "aleatorio")
        {
            Random random = new Random();
            int randomValue = random.Next(1, 101);
            randomNumber += randomValue;
        }

        HttpContext.Session.SetInt32("RandomNumber", randomNumber);

        return RedirectToAction("Dashboard");
    }
    [HttpPost]
    [Route("logout")]
    public IActionResult Logout(string logout)
    {
        if (logout == "logout")
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        return RedirectToAction("Dashboard");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
