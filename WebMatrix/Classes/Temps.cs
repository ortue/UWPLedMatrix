using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebMatrix.Context;

namespace WebMatrix.Classes
{
  public class Temps
  {
    /// <summary>
    /// Horloge
    /// </summary>
    public static void Horloge()
    {
      Util.Setup();

      Task.Run(() =>
      {
        int task = Util.StartTask();

        while (Util.TaskWork(task))
        {
          Util.Context.Pixels.SetHorloge();
          Util.SetLeds();
          Util.Context.Pixels.Reset();
        }
      });
    }

    /// <summary>
    /// Meteo
    /// </summary>
    public static void Meteo()
    {
      Util.Setup();
      ImageClassList meteoImgs = new ImageClassList("Images/Meteo");

      Task.Run(() =>
      {
        DateTime update = DateTime.Now.AddMinutes(-10);
        int task = Util.StartTask();

        while (Util.TaskWork(task))
        {
          if (Util.Meteo.weather is currentWeather weather)
            meteoImgs.SetPixel(weather.icon, Util.Context.Pixels);

          Util.Context.Pixels.SetMeteo(Util.Meteo);

          Util.SetLeds();
          Util.Context.Pixels.Reset();

          if (update.AddMinutes(5) < DateTime.Now)
          {
            update = DateTime.Now;
            Util.GetMeteoAsync();
          }
        }
      });
    }

    /// <summary>
    /// Nouvelle
    /// </summary>
    public static void Nouvelle()
    {
      Util.Setup();

      Task.Run(() =>
      {
        int debut = -20;
        int task = Util.StartTask();
        DateTime update = DateTime.Now.AddMinutes(-60);
        CaractereList caracteres = new CaractereList(20);

        while (Util.TaskWork(task))
        {
          if (!string.IsNullOrWhiteSpace(Util.NouvelleStr) && Util.NouvelleStr.Length < debut++)
            debut = -20;

          caracteres.SetText(Util.NouvelleStr);
          Util.Context.Pixels.SetNouvelle(caracteres.GetCaracteres(debut));
          Util.SetLeds();
          Util.Context.Pixels.Reset();

          using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            waitHandle.Wait(TimeSpan.FromMilliseconds(100));

          if (update.AddMinutes(60) < DateTime.Now)
          {
            update = DateTime.Now;
            Util.GetNouvelleAsync();
          }
        }
      });
    }
  }
}
