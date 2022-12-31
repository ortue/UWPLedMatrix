using Library.Collection;
using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class Spiral
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecSpiral);
    }

    /// <summary>
    /// Spiral
    /// </summary>
    private void ExecSpiral()
    {
      // Initialize the led strip
      int i = 1;
      int task = TaskGo.StartTask();
      Random random = new();
      CercleList cercles = new(3, 5, 120);
      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        if (i++ % 250 == 249)
        {
          int r = random.Next(2, 9);
          cercles = new CercleList(r, 5, 360 / r);
        }

        for (double rayon = 1; rayon < 15; rayon += 0.4)
          foreach (var cercle in cercles)
            if (Pixel.Cercle(360 - (cercle.DegreeInter - (int)(rayon * 10)) % 360, rayon) is Pixel coord)
              Pixels.Get(coord).SetColor(cercle.Couleur);

        cercles.SetDegree(9);
        Pixels.SendPixels();

        waitHandle.Wait(TimeSpan.FromMilliseconds(10));
      }
    }
  }
}