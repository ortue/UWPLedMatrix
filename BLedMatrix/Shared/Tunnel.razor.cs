using Library.Collection;
using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class Tunnel
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecTunnel);
    }

    /// <summary>
    /// Tunnel
    /// </summary>
    private void ExecTunnel()
    {
      int i = 1;
      int task = TaskGo.StartTask();
      CercleList cercles = new(3, 5);
      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        foreach (var cercle in cercles)
          for (int degree = 0; degree < 360; degree += 5)
            if (Pixel.Cercle(degree, cercle.Rayon, cercle.X, cercle.Y) is Pixel coord)
              Pixels.Get(coord).SetColor(cercle.Couleur);

        cercles.SetRayon(0.3, i++ % 2000 > 1000);
        Pixels.SendPixels();

        waitHandle.Wait(TimeSpan.FromMilliseconds(10));
      }
    }
  }
}