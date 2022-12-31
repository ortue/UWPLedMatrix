using Library.Collection;
using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class Oscilloscope
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecOscilloscope);
    }

    /// <summary>
    /// Oscilloscope
    /// </summary>
    private void ExecOscilloscope()
    {
      int task = TaskGo.StartTask();

      int x = 0;
      Random random = new();
      SinusList sinus = new(random.Next(1, 4));

      while (TaskGo.TaskWork(task))
      {
        if (x++ % 10000 == 0)
          sinus = new SinusList(random.Next(1, 4));

        foreach (Sinus sin in sinus)
          Pixels.Get(sin.Coord).SetColor(sin.Couleur);

        sinus.Next();

        Pixels.SendPixels();
        Pixels.Reset();
      }
    }
  }
}