using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
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
      //int bord = 0;

      Task.Run(() =>
      {
        DateTime update = DateTime.Now.AddMinutes(-10);
        int task = Util.StartTask();

        while (Util.TaskWork(task))
        {
          if (Util.Meteo.weather is currentWeather weather)
            meteoImgs.SetPixel(weather.icon, Util.Context.Pixels);

          Util.Context.Pixels.SetMeteo(Util.Meteo);

          //Border(bord++);

          Util.SetLeds();
          Util.Context.Pixels.Reset();

          if (update.AddMinutes(5) < DateTime.Now)
          {
            update = DateTime.Now;
            Util.GetMeteo();
          }
        }
      });
    }

    //private static void Border(int bord)
    //{
    //  Util.Context.Pixels.GetCoordonnee(bord % 20, bord % 20).SetColor(new Couleur { B = 127 });
    //}
  }
}