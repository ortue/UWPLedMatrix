using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LedLibrary.Collection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMatrix.Classes;
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

    public IActionResult Index(int? id)
    {
      if (id == null && Util.TaskNbr == 0)
      {
        Random random = new Random();
        id = random.Next(0, 2);
      }

      Task.Run(() => Demo.Go(id));

      return View();
    }

    public IActionResult Spectrum(int? id)
    {
      System.Diagnostics.Process.Start(new ProcessStartInfo() { FileName = "sudo", Arguments = "shutdown -h" });


      return View();
    }

    public IActionResult Temps(int? id)
    {
      switch (id)
      {
        case 0:
          Classes.Temps.Horloge();
          break;

        case 1:
          Classes.Temps.Meteo();
          break;
      }

      return View();
    }

    public IActionResult Animation(string id)
    {
      AnimationModel model = new AnimationModel(id);



      return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


  }
}