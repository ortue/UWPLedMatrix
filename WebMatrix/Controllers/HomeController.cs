using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LedMatrix.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMatrix.Context;
using WebMatrix.Models;

namespace WebMatrix.Controllers
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

    public IActionResult Privacy()
    {

      try
      {
        // Initialize the led strip
        //Util.Context.PixelStrip.Begin();
        //Task.Run(() => Demo.Go());


        Util.Setup();

        foreach (LedLibrary.Entities.Pixel pixel in Util.Context.Pixels)
          pixel.Couleur = new LedLibrary.Entities.Color { A = 0, R = 32, G = 0, B = 0 };

        Util.SetLeds();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.ToString());
      }


      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
