using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Linq;
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
          if (Util.Meteo is current meteo)
            meteoImgs.SetPixel(meteo.weather.icon, Util.Context.Pixels);

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
        int debut = ResetNouvelle();
        int task = Util.StartTask();
        DateTime update = DateTime.Now.AddMinutes(-60);
        CaractereList caracteres = new CaractereList(20);

        while (Util.TaskWork(task))
        {
          //Reset après avoir défiler tout le texte
          if (!string.IsNullOrWhiteSpace(Util.NouvelleStr) && Util.NouvelleStr.Length < debut++)
            debut = ResetNouvelle();

          caracteres.SetText(Util.NouvelleStr);
          Util.Context.Pixels.SetNouvelle(caracteres.GetCaracteres(debut), debut);
          Util.SetLeds();
          Util.Context.Pixels.Reset();

          using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            waitHandle.Wait(TimeSpan.FromMilliseconds(50));

          //Mettre a jour les nouvelle aux heures
          if (update.AddMinutes(60) < DateTime.Now)
          {
            update = DateTime.Now;
            Util.GetNouvelleAsync();
          }
        }
      });
    }

    /// <summary>
    /// ResetNouvelle
    /// </summary>
    private static int ResetNouvelle()
    {
      RadioCanadaIcon("RadioCanada");

      return -20;
    }

    /// <summary>
    /// RadioCanadaIcon
    /// </summary>
    /// <param name="filename"></param>
    private static void RadioCanadaIcon(string filename)
    {
      if (new ImageClassList("Images").SingleOrDefault(a => a.FileNameID == filename) is ImageClass imageClass)
      {
        //Fade In
        for (int slide = imageClass.Width; slide >= 0; slide--)
          SetAnimation(imageClass, 0, slide);

        //Fade Out
        for (int slide = 0; slide < imageClass.Width; slide++)
          SetAnimation(imageClass, 0, slide, true);
      }
    }

    /// <summary>
    /// SetAnimation
    /// </summary>
    /// <param name="imageClass"></param>
    /// <param name="frame"></param>
    /// <param name="slide"></param>
    /// <param name="fadeOut"></param>
    private static void SetAnimation(ImageClass imageClass, int frame, int slide, bool fadeOut = false)
    {
      imageClass.SetÞixelFrame(frame++, Util.Context.Pixels, slide, fadeOut);
      Util.SetLeds();
      Util.Context.Pixels.Reset();
    }
  }
}