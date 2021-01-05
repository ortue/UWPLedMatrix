using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebMatrix.Context;

namespace WebMatrix.Classes
{
  public class Temps
  {
    public static List<string> Lorembarnaks
    {
      get
      {
        return new List<string>
        {
          "tabarnak", "tabarnouche", "tabarouette", "taboire", "tabarslaque", "tabarnane",
          "câlisse", "câlique", "câline", "câline de bine", "câliboire", "caltor",
          "crisse", "christie", "crime", "bout d'crisse",
          "ostie", "astie", "estique", "ostifie", "esprit",
          "ciboire", "saint-ciboire",
          "torrieux", "torvisse",
          "cimonaque", "saint-cimonaque",
          "baptême", "batince", "batèche",
          "bâtard",
          "calvaire", "calvince", "calvinouche",
          "mosus",
          "maudit", "mautadit", "maudine", "mautadine",
          "sacrament", "sacréfice", "saint-sacrament",
          "viarge", "sainte-viarge", "bout d'viarge",
          "ciarge", "saint-ciarge", "bout d'ciarge",
          "cibouleau",
          "cibole", "cibolac",
          "enfant d'chienne", "chien sale",
          "verrat",
          "marde", "maudite marde", "mangeux d'marde",
          "boswell",
          "sacristi", "sapristi",
          "Jésus de plâtre", "Jésus Marie Joseph", "p'tit Jésus", "doux Jésus",
          "crucifix",
          "patente à gosse", "cochonnerie", "cossin",
          "viande à chien",
          "cul", "saintes fesses",
          "purée",
          "etole",
          "charogne", "charrue",
          "gériboire", "géritole",
          "colon"
        };
      }
    }

    public static string EspaceFin
    {
      get { return "                    "; }
    }

    public static string Heure
    {
      get 
      {
        string leading = "";
        string deuxPoint = " ";
        int hh = DateTime.Now.Hour;

        if (hh == 0)
          hh = 12;

        if (hh > 12)
          hh -= 12;

        if (DateTime.Now.Millisecond < 500)
          deuxPoint = ":";

        if (hh < 10)
          leading = "  ";

        return leading + hh + deuxPoint + DateTime.Now.ToString("mm");
      }
    }

    /// <summary>
    /// Horloge
    /// </summary>
    public static void Horloge()
    {
      Util.Setup();

      Task.Run(() =>
      {
        int task = Util.StartTask();

        CaractereList caracteres = new CaractereList(20);

        while (Util.TaskWork(task))
        {
          caracteres.SetText(Heure);

          Util.Context.Pixels.SetHorloge(caracteres.GetCaracteres());
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

          Util.Context.Pixels.SetMeteo(Util.Meteo, Heure);

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
        int largeur = 0;
        int debut = ResetNouvelle();
        int task = Util.StartTask();
        DateTime update = DateTime.Now.AddMinutes(-60);
        CaractereList caracteres = new CaractereList(20);

        while (Util.TaskWork(task))
        {
          //Reset après avoir défiler tout le texte
          if (!string.IsNullOrWhiteSpace(Util.NouvelleStr) && largeur < debut++)
            debut = ResetNouvelle();

          largeur = caracteres.SetText(Util.NouvelleStr);
          Util.Context.Pixels.SetNouvelle(caracteres.GetCaracteres(debut), Heure);
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
    /// LoremBarnak
    /// </summary>
    public static void LoremBarnak()
    {
      Util.Setup();

      Task.Run(() =>
      {
        int largeur = 0;
        int debut = -20;
        int task = Util.StartTask();
        string loremBarnak = GetLoremBarnak(10);
        CaractereList caracteres = new CaractereList(20);

        while (Util.TaskWork(task))
        {
          //Reset après avoir défiler tout le texte
          if (!string.IsNullOrWhiteSpace(loremBarnak) && largeur < debut++)
          {
            debut = -20;
            loremBarnak = GetLoremBarnak(10);
          }

          largeur = caracteres.SetText(loremBarnak);
          Util.Context.Pixels.SetNouvelle(caracteres.GetCaracteres(debut), Heure);
          Util.SetLeds();
          Util.Context.Pixels.Reset();

          using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
          waitHandle.Wait(TimeSpan.FromMilliseconds(50));
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

    /// <summary>
    /// GetLoremBarnak
    /// </summary>
    /// <param name="taille"></param>
    /// <returns></returns>
    private static string GetLoremBarnak(int taille)
    {
      string loremBarnak = string.Empty;

      List<string> lorembarnaks = Lorembarnaks;

      Random random = new Random();

      for (int i = 0; i < taille; i++)
      {
        int r = random.Next(0, lorembarnaks.Count);

        if (i == 0)
          loremBarnak += lorembarnaks[r] + " ";
        else if (i == taille - 1)
          loremBarnak += Article(lorembarnaks[r]) + "." + EspaceFin;
        else
          loremBarnak += Article(lorembarnaks[r]) + " ";

        lorembarnaks.RemoveAt(r);
      }

      return Util.RemoveDiacritics(loremBarnak.ToUpper());
    }

    /// <summary>
    /// Article
    /// </summary>
    /// <param name="loremBarnak"></param>
    /// <returns></returns>
    private static string Article(string loremBarnak)
    {
      List<char> voyelle = new List<char> { 'A', 'E', 'I', 'O', 'U', 'H', 'Y' };

      if (voyelle.Contains(char.ToUpper(loremBarnak[0])))
        return "D'" + loremBarnak;

      return "DE " + loremBarnak;
    }
  }
}