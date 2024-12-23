using Library.Collection;
using Library.Entity;

namespace LedMatrix.Components.Layout
{
  public partial class ElectroCardiogramme
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecECG);
    }

    /// <summary>
    /// ElectroCardiogramme
    /// </summary>
    private void ExecECG()
    {
      int task = TaskGo.StartTask();
      ECGList egcs = new(60, PixelList.Largeur, PixelList.Hauteur);
      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        Coeur(egcs.Next(9));

        foreach (ECG egc in egcs)
          Pixels.Get(egc.X, egc.Y).SetColor(egc.Couleur);

        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(80));
      }
    }

    /// <summary>
    /// Coeur
    /// </summary>
    public void Coeur(bool battement)
    {
      if (!battement)
      {
        Pixels.Get(13, 1).SetColor(Couleur.Get(15, 0, 0));
        Pixels.Get(17, 1).SetColor(Couleur.Get(15, 0, 0));

        Pixels.Get(12, 2).SetColor(Couleur.Get(15, 0, 0));
        Pixels.Get(18, 2).SetColor(Couleur.Get(15, 0, 0));

        Pixels.Get(12, 3).SetColor(Couleur.Get(15, 0, 0));
        Pixels.Get(18, 3).SetColor(Couleur.Get(15, 0, 0));

        Pixels.Get(13, 4).SetColor(Couleur.Get(15, 0, 0));
        Pixels.Get(17, 4).SetColor(Couleur.Get(15, 0, 0));

        Pixels.Get(14, 5).SetColor(Couleur.Get(15, 0, 0));
        Pixels.Get(16, 5).SetColor(Couleur.Get(15, 0, 0));

        Pixels.Get(15, 6).SetColor(Couleur.Get(15, 0, 0));
      }

      Pixels.Get(14, 1).SetColor(Couleur.Get(25, 0, 0));
      Pixels.Get(16, 1).SetColor(Couleur.Get(25, 0, 0));

      Pixels.Get(13, 2).SetColor(Couleur.Get(25, 0, 0));
      Pixels.Get(14, 2).SetColor(Couleur.Get(35, 0, 0));
      Pixels.Get(15, 2).SetColor(Couleur.Get(25, 0, 0));
      Pixels.Get(16, 2).SetColor(Couleur.Get(35, 0, 0));
      Pixels.Get(17, 2).SetColor(Couleur.Get(25, 0, 0));

      Pixels.Get(13, 3).SetColor(Couleur.Get(25, 0, 0));
      Pixels.Get(14, 3).SetColor(Couleur.Get(35, 0, 0));
      Pixels.Get(15, 3).SetColor(Couleur.Get(25, 0, 0));
      Pixels.Get(16, 3).SetColor(Couleur.Get(35, 0, 0));
      Pixels.Get(17, 3).SetColor(Couleur.Get(25, 0, 0));

      Pixels.Get(14, 4).SetColor(Couleur.Get(25, 0, 0));
      Pixels.Get(15, 4).SetColor(Couleur.Get(35, 0, 0));
      Pixels.Get(16, 4).SetColor(Couleur.Get(25, 0, 0));

      Pixels.Get(15, 5).SetColor(Couleur.Get(25, 0, 0));
    }
  }
}