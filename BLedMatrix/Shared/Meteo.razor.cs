using Library.Collection;
using Library.Entity;
using Library.Util;
using Microsoft.AspNetCore.Components;

namespace BLedMatrix.Shared
{
  public partial class Meteo
  {
    [Parameter]
    public ImageClassList? Animations { get; set; }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecMeteo);
    }

    /// <summary>
    /// Meteo
    /// </summary>
    private void ExecMeteo()
    {
      Couleur couleur = new() { R = 64, G = 0, B = 0 };
      DateTime update = DateTime.Now.AddMinutes(-10);
      int task = TaskGo.StartTask();

      while (TaskGo.TaskWork(task))
      {
        if (OpenWeather.Meteo is current meteo)
        {
          Animations?.SetPixel(meteo.weather?.icon, Pixels);

          string leading = "";

          if (meteo.temperature?.value.ToString("0").Length < 2)
            leading = "  ";

          Pixels.Set(CaractereList.Print(leading + meteo.temperature?.value.ToString("0") + "°C", 1, 1, couleur));
          Pixels.Set(CaractereList.Print("H " + meteo.humidity?.value.ToString() + "%", 2, 7, couleur));
        }

        Pixels.Set(CaractereList.Print(CaractereList.Heure, 2, 13, couleur));
        Background.Bleu(Pixels);

        Pixels.SendPixels();
        Pixels.Reset();

        if (update.AddMinutes(5) < DateTime.Now)
        {
          update = DateTime.Now;
          OpenWeather.Refresh();
        }
      }
    }
  }
}