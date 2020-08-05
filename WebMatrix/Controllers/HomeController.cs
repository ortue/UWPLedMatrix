using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
        id = random.Next(0, 5);
      }

      if (id != null)
        Util.Autorun = false;

      Task.Run(() => Demo.Go(id));

      //Éteindre le raspberry linux
      if (id == 1000)
      {
        Process.Start(new ProcessStartInfo()
        {
          FileName = "pkill",
          Arguments = "--oldest chromium"
        });

        using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        waitHandle.Wait(TimeSpan.FromSeconds(2));

        Process.Start(new ProcessStartInfo()
        {
          FileName = "sudo",
          Arguments = "shutdown -h now"
        });
      }

      return View();
    }

    public IActionResult Spectrum(int? id)
    {
      //Util.Autorun = false;

      return View();
    }

    public IActionResult Temps(int? id)
    {

      switch (id)
      {
        case 0:
          Util.Autorun = false;
          Classes.Temps.Horloge();
          break;

        case 1:
          Util.Autorun = false;
          Classes.Temps.Meteo();
          break;

        case 2:
          Util.Autorun = false;
          Classes.Temps.Nouvelle();
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