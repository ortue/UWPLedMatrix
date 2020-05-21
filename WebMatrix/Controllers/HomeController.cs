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
    public string LastAutorun { get; set; }

    public AnimationList Animations
    {
      get { return Util.Context.Animations; }
    }

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

    public IActionResult Animation(int? id)
    {


      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private void ShowAnimation(string file)
    {
      //Task.Run(() =>
      //{
      //  int task = Util.StartTask();
      //  int frame = 0;

        //Fade Out
        //if (!string.IsNullOrWhiteSpace(LastAutorun))
        //{
        //  ImageClass imageClassLastAutorun = new ImageClass(LastAutorun);

        //  for (int slide = 0; slide < imageClassLastAutorun.Width; slide++)
        //    SetAnimation(imageClassLastAutorun, frame++, slide, true);
        //}

        //ImageClass imageClass = new ImageClass(file);

        //Fade In
        //for (int slide = imageClass.Width; slide >= 0; slide--)
        //  SetAnimation(imageClass, frame++, slide);

        //LastAutorun = file;

        //Animation
        //while (imageClass.Animation && Util.TaskWork(task))
        //  SetAnimation(imageClass, frame++, 0);
      //});
    }

    //private void SetAnimation(ImageClass imageClass, int frame, int slide, bool fadeOut = false)
    //{
    //  imageClass.SetÞixelFrame(frame++, Util.Context.Pixels, slide, fadeOut);
    //  Util.SetLeds();
    //  Util.Context.Pixels.Reset();

    //  if (slide == 0)
    //    using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
    //      waitHandle.Wait(TimeSpan.FromMilliseconds(60));
    //}
  }
}