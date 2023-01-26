using Library.Collection;
using Library.Entity;
using Library.Util;

namespace BLedMatrix.Shared
{
  public partial class Binaire
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecBinaire);
    }

    /// <summary>
    /// Binaire
    /// </summary>
    private void ExecBinaire()
    {
      int task = TaskGo.StartTask("Binaire");

      while (TaskGo.TaskWork(task))
      {
        SetBinaire();

        Background.Bleu(Pixels);
        Pixels.SendPixels();
        Pixels.Reset();
      }
    }

    /// <summary>
    /// SetBinaire
    /// </summary>
    /// <param name="caracteres"></param>
    public void SetBinaire()
    {
      string heure = Convert.ToString(DateTime.Now.Hour, 2).PadLeft(6, '0');
      string minute = Convert.ToString(DateTime.Now.Minute, 2).PadLeft(6, '0');
      string seconde = Convert.ToString(DateTime.Now.Second, 2).PadLeft(6, '0');

      for (int h = 0; h < 6; h++)
        if (heure[h] == '1')
        {
          Pixels.Get(2, h * 2 + 1).SetColor(Couleurs.Get("Binaire", "HeureCouleur", Couleur.Get(31, 64, 127)));
          Pixels.Get(3, h * 2 + 1).SetColor(Couleurs.Get("Binaire", "HeureCouleur", Couleur.Get(31, 64, 127)));
          Pixels.Get(4, h * 2 + 1).SetColor(Couleurs.Get("Binaire", "HeureCouleur", Couleur.Get(31, 64, 127)));
        }
        else
        {
          Pixels.Get(2, h * 2 + 1).SetColor(Couleurs.Get("Binaire", "HeureAltCouleur", Couleur.Noir));
          Pixels.Get(3, h * 2 + 1).SetColor(Couleurs.Get("Binaire", "HeureAltCouleur", Couleur.Noir));
          Pixels.Get(4, h * 2 + 1).SetColor(Couleurs.Get("Binaire", "HeureAltCouleur", Couleur.Noir));
        }

      for (int m = 0; m < 6; m++)
        if (minute[m] == '1')
        {
          Pixels.Get(9, m * 2 + 1).SetColor(Couleurs.Get("Binaire", "MinuteCouleur", Couleur.Get(31, 64, 127)));
          Pixels.Get(10, m * 2 + 1).SetColor(Couleurs.Get("Binaire", "MinuteCouleur", Couleur.Get(31, 64, 127)));
          Pixels.Get(11, m * 2 + 1).SetColor(Couleurs.Get("Binaire", "MinuteCouleur", Couleur.Get(31, 64, 127)));
        }
        else
        {
          Pixels.Get(9, m * 2 + 1).SetColor(Couleurs.Get("Binaire", "MinuteAltCouleur", Couleur.Noir));
          Pixels.Get(10, m * 2 + 1).SetColor(Couleurs.Get("Binaire", "MinuteAltCouleur", Couleur.Noir));
          Pixels.Get(11, m * 2 + 1).SetColor(Couleurs.Get("Binaire", "MinuteAltCouleur", Couleur.Noir));
        }

      for (int s = 0; s < 6; s++)
        if (seconde[s] == '1')
        {
          Pixels.Get(16, s * 2 + 1).SetColor(Couleurs.Get("Binaire", "SecondeCouleur", Couleur.Get(31, 64, 127)));
          Pixels.Get(17, s * 2 + 1).SetColor(Couleurs.Get("Binaire", "SecondeCouleur", Couleur.Get(31, 64, 127)));
          Pixels.Get(18, s * 2 + 1).SetColor(Couleurs.Get("Binaire", "SecondeCouleur", Couleur.Get(31, 64, 127)));
        }
        else
        {
          Pixels.Get(16, s * 2 + 1).SetColor(Couleurs.Get("Binaire", "SecondeAltCouleur", Couleur.Noir));
          Pixels.Get(17, s * 2 + 1).SetColor(Couleurs.Get("Binaire", "SecondeAltCouleur", Couleur.Noir));
          Pixels.Get(18, s * 2 + 1).SetColor(Couleurs.Get("Binaire", "SecondeAltCouleur", Couleur.Noir));
        }

      Pixels.Set(CaractereList.Print(CaractereList.Heure, 2, 14, Couleurs.Get("Binaire", "HeureTexteCouleur", Couleur.Get(31, 64, 127))));
    }
  }
}