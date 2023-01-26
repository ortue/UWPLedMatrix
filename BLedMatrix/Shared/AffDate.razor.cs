using Library.Collection;
using Library.Entity;
using Library.Util;

namespace BLedMatrix.Shared
{
  public partial class AffDate
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecAffDate);
    }

    /// <summary>
    /// AffDate
    /// </summary>
    private void ExecAffDate()
    {
      int task = TaskGo.StartTask("AffDate");

      while (TaskGo.TaskWork(task))
      {
        Pixels.Set(CaractereList.Print(DateTime.Now.ToString("yyyy"), 4, 1, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
        Pixels.Set(CaractereList.Print(DateTime.Now.ToString("MM-dd"), 1, 7, Couleurs.Get("AffDate", "DateCouleur", Couleur.Bleu)));
        Pixels.Set(CaractereList.Print(CaractereList.Heure, 2, 13, Couleurs.Get("AffDate", "HeureCouleur", Couleur.Bleu)));

        Background.Bleu(Pixels);
        Pixels.SendPixels();
        Pixels.Reset();
      }
    }
  }
}