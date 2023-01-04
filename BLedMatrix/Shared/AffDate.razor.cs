using Library.Collection;
using Library.Entity;

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
      //decimal cycle = 0;
      //Random r = new();
      //int bg = r.Next(1, Pixels.NbrBackground);
      //bool reverse = r.Next(0, 2) == 1;

      //if (bg == 4)
      //  bg = 1;

      //if (bg == 3 && reverse)
      //  reverse = false;

      int task = TaskGo.StartTask();

      while (TaskGo.TaskWork(task))
      {
        //caracteres.SetText(Heure);

        Pixels.Set(CaractereList.Print(DateTime.Now.ToString("yyyy"), 4, 1, Couleur.Get(0, 0, 127)));
        Pixels.Set(CaractereList.Print(DateTime.Now.ToString("MM-dd"), 1, 7, Couleur.Get(0, 0, 127)));
        Pixels.Set(CaractereList.Print(CaractereList.Heure, 2, 13, Couleur.Get(0, 0, 127)));

          //cycle = Background.Bleu(bg, cycle, reverse);
        Pixels.SendPixels();
        Pixels.Reset();
      }
    }
  }
}