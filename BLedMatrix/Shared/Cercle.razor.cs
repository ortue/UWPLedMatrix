using Library.Collection;
using Nfw.Linux.Joystick.Simple;

namespace BLedMatrix.Shared
{
  public partial class Cercle
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecCercle);
    }

    /// <summary>
    /// ExecCercle
    /// </summary>
    private void ExecCercle()
    {
      int task = TaskGo.StartTask();

      using ManualResetEventSlim waitHandle = new(false);

      using Joystick joystick = new("/dev/input/js0");
      var manette = new Library.Util.Manette(9, 9);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 150000d);
      joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      double ra = 4;

      Random random = new();
      DateTime temp = DateTime.Now;
      CercleList cercles = new(4, 1, 90);

      while (TaskGo.TaskWork(task))
      {
        if (temp.AddMinutes(1) < DateTime.Now)
        {
          temp = DateTime.Now;
          int r = random.Next(2, 12);
          cercles = new CercleList(r, 1, 360 / r);
        }

        cercles.Variation();

        if (manette.BtnA)
        {
          ra += manette.AxisBX;

          if (ra > 10)
            ra = 10;

          if (ra < 0)
            ra = 0;

          manette.NextAxisA();

          foreach (var cercle in cercles)
            Pixels.Get(PixelList.GetCercleCoord(manette.Pixel, cercle.DegreeInter, Math.Abs((int)Math.Round(ra, 0)))).SetColor(cercle.Couleur);
        }
        else
        {
          foreach (var cercle in cercles)
            Pixels.Get(PixelList.GetCercleCoord(cercle.Centre, cercle.DegreeInter, cercle.Rayon)).SetColor(cercle.Couleur);
        }

        cercles.SetDegree(5);

        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(5));
      }
    }
  }
}