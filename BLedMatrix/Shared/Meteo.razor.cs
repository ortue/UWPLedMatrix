using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class Meteo
  {
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
      //ImageClassList meteoImgs = new("Images/Meteo");

      DateTime update = DateTime.Now.AddMinutes(-10);
      int task = TaskGo.StartTask();

      while (TaskGo.TaskWork(task))
      {
        //if (Util.Meteo is current meteo)
        //meteoImgs.SetPixel(meteo.weather.icon, Util.Context.Pixels);

        //Util.Context.Pixels.SetMeteo(Util.Meteo, Heure);

        Pixels.SendPixels();
        Pixels.Reset();

        if (update.AddMinutes(5) < DateTime.Now)
        {
          update = DateTime.Now;
          //Util.GetMeteoAsync();
        }
      }
    }

    /// <summary>
    /// SetMeteo
    /// </summary>
    /// <param name="temp"></param>
    public void SetMeteo()
    {
      Couleur couleur = new() { R = 64, G = 0, B = 0 };

      //if (meteo != null)
      //{
      //  string leading = "";

      //  if (meteo.temperature.value.ToString("0").Length < 2)
      //    leading = "  ";

      //  Print(leading + meteo.temperature.value.ToString("0") + "°C", 1, 1, couleur);
      //  Print("H " + meteo.humidity.value.ToString() + "%", 2, 7, couleur);
      //}

      //Print(heure, 2, 13, couleur);
      //BackGround();
    }
  }
}