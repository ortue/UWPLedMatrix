using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LedLibrary.Classes;
using LedLibrary.Collection;
using LedMatrix.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    public IActionResult Index()
    {
      Util.Setup();
      //Util.StopTask();

      //Task.Run(() =>
      //{
      //  int task = Util.StartTask();

      //  while (Util.TaskWork(task))
      //  {
      //    Util.Context.Pixels.SetHorloge();
      //    Util.SetLeds();
      //    Util.Context.Pixels.Reset();
      //  }
      //});





      Util.StopTask();

      Task.Run(() =>
      {
        DateTime update = DateTime.Now.AddMinutes(-10);
        int task = Util.StartTask();

        while (Util.TaskWork(task))
        {
          //if (Util.Context.Meteo != null)
          //{
          //  ImageClass imageClass = new ImageClass(Util.Context.MeteoImgs.GetName(Util.Context.Meteo.weather.icon).FileName);
          //  imageClass.SetÞixelFrame(0, Util.Context.Pixels, 0, false);
          //}

          Util.Context.Pixels.SetMeteo(Util.Context.Meteo);
          Util.SetLeds();
          Util.Context.Pixels.Reset();

          if (update.AddMinutes(5) < DateTime.Now)
          {
            update = DateTime.Now;
            Util.Context.Meteo = Util.GetMeteo();
          }
        }
      });





      //Task.Run(() =>
      //{
      //  Util.Context.Autorun = true;

      //  int i = 0;

      //  while (Util.Context.Autorun)
      //  {
      //    ShowAnimation(Animations[i++].FileName);

      //    using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
      //      waitHandle.Wait(TimeSpan.FromSeconds(10));

      //    if (Animations.Count <= i)
      //      i = 0;
      //  }
      //});







      return View();
    }

    public IActionResult Privacy()
    {

      // Initialize the led strip
      Util.Setup();

      Task.Run(() => Demo.Go());


      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



    /// <summary>
    /// ShowAnimation
    /// </summary>
    /// <param name="file"></param>
    private void ShowAnimation(string file)
    {
      Task.Run(() =>
      {
        int task = Util.StartTask();
        int frame = 0;

        //Fade Out
        //if (!string.IsNullOrWhiteSpace(LastAutorun))
        //{
        //  ImageClass imageClassLastAutorun = new ImageClass(LastAutorun);

        //  for (int slide = 0; slide < imageClassLastAutorun.Width; slide++)
        //    SetAnimation(imageClassLastAutorun, frame++, slide, true);
        //}

        ImageClass imageClass = new ImageClass(file);

        //Fade In
        //for (int slide = imageClass.Width; slide >= 0; slide--)
        //  SetAnimation(imageClass, frame++, slide);

        LastAutorun = file;

        //Animation
        //while (imageClass.Animation && Util.TaskWork(task))
        //  SetAnimation(imageClass, frame++, 0);
      });
    }


    /// <summary>
    /// SetAnimation
    /// </summary>
    /// <param name="imageClass"></param>
    /// <param name="frame"></param>
    /// <param name="slide"></param>
    /// <param name="fadeOut"></param>
    private void SetAnimation(ImageClass imageClass, int frame, int slide, bool fadeOut = false)
    {
      imageClass.SetÞixelFrame(frame++, Util.Context.Pixels, slide, fadeOut);
      Util.SetLeds();
      Util.Context.Pixels.Reset();

      if (slide == 0)
        using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
          waitHandle.Wait(TimeSpan.FromMilliseconds(60));
    }
  }
}