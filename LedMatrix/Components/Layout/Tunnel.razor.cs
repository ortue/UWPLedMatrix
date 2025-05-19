using Library.Collection;
using Library.Entity;

namespace LedMatrix.Components.Layout
{
  public partial class Tunnel
  {
    private static bool Boot { get; set; } = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      if (Boot)
      {
        await Task.Run(ExecTunnel);

        //await Task.Run(Apa102MinimalTest.Main);

      }

      Boot = false;
    }

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
      CercleList cercles = new(4, 4);
      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        foreach (var cercle in cercles)
          for (int degree = 0; degree < 360; degree += 3)
            if (Pixel.Cercle(degree, cercle.Rayon, cercle.X, cercle.Y) is Pixel coord)
              Pixels.Get(coord).SetColor(cercle.Couleur);

        cercles.SetRayon(0.2, i++ % 2000 > 1000);

        Pixels.SendPixels();

        waitHandle.Wait(TimeSpan.FromMilliseconds(1));
      }
    }
  }
}