using LedLibrary.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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

    public IActionResult Index(int? id, Criteria criteria)
    {
      if (id == null && Util.TaskNbr == 0)
      {
        Random random = new Random();
        id = random.Next(1, 9);
      }

      if (id != null)
        Util.Autorun = false;

      Task.Run(() => Demo.Go(id, criteria));

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

      ViewBag.CmbStroboscope = criteria.CmbStroboscope;

      return View();
    }

    public IActionResult Jeu(int? id)
    {
      if (id != null)
        Util.Autorun = false;

      switch (id)
      {
        case 0:
          Task.Run(() => Classes.Jeu.Pong());
          break;

        case 1:
          Task.Run(() => Classes.Jeu.Tetris());
          break;

        case 2:
          Task.Run(() => Classes.Jeu.Serpent());
          break;

        case 3:
          Task.Run(() => Classes.Jeu.Labyrinthe());
          break;

        case 4:
          Task.Run(() => Classes.Jeu.Arkanoid());
          break;
      }

      return View();
    }

    public IActionResult Musique(int? id, Criteria criteria)
    {
      if (id != null)
        Util.Autorun = false;

      switch (id)
      {
        case 0:
          Task.Run(() => Classes.Musique.VuMeter());
          break;

        case 1:
          Task.Run(() => Classes.Musique.Spectrum2(criteria));
          break;

        case 2:
          Task.Run(() => Classes.Musique.Graph1(criteria));
          break;

        case 3:
          Task.Run(() => Classes.Musique.Graph2(criteria));
          break;

        case 4:
          Task.Run(() => Classes.Musique.SpectrumGraph(criteria));
          break;
      }

      ViewBag.CmbAmplitude = criteria.CmbAmplitude;

      return View();
    }

    public IActionResult Temps(int? id)
    {
      if (id != null)
        Util.Autorun = false;

      switch (id)
      {
        case 0:
          Classes.Temps.Horloge();
          break;

        case 1:
          Classes.Temps.Meteo();
          break;

        case 2:
          Classes.Temps.Nouvelle();
          break;

        case 3:
          Classes.Temps.Date();
          break;

        case 4:
          Classes.Temps.Binaire();
          break;

        case 5:
          Classes.Temps.LoremBarnak();
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