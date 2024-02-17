using Library.Collection;
using Library.Entity;
using Nfw.Linux.Joystick.Simple;

namespace BlazorAppMatrix.Components.Layout
{
  public partial class Tunnel
  {
    private static bool Boot { get; set; } = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      if (Boot)
        await Task.Run(ExecTunnel);

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

      using Joystick joystick = new("/dev/input/js0");
      var manette = new Library.Util.Manette(10, 10);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 150000d);
      joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      while (TaskGo.TaskWork(task))
      {
        foreach (var cercle in cercles)
          for (int degree = 0; degree < 360; degree += 3)
            if (Pixel.Cercle(degree, cercle.Rayon, cercle.X, cercle.Y) is Pixel coord)
              Pixels.Get(coord).SetColor(cercle.Couleur);

        if (manette.BtnA)
        {
          manette.NextAxisA();
          cercles.SetRayon(0.2, manette.Pixel);
          //Pixels.Get(manette.Pixel).SetColor(Couleur.Rnd);
        }
        else
          cercles.SetRayon(0.2, i++ % 2000 > 1000);

        Pixels.SendPixels();

        waitHandle.Wait(TimeSpan.FromMilliseconds(2));
      }
    }
  }
}