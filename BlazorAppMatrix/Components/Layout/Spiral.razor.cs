using Library.Collection;
using Library.Entity;

namespace BlazorAppMatrix.Components.Layout
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

      //using Joystick joystick = new("/dev/input/js0");
      //var manette = new Library.Util.Manette();
      //joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / (decimal)150000);
      //joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      //decimal x = 9;
      //decimal y = 9;


      while (TaskGo.TaskWork(task))
      {
        if (i++ % 250 == 249)
        {
          int r = random.Next(2, 9);
          cercles = new CercleList(r, 5, 360 / r);
        }

        //if (manette.BtnA)
        //{
        //  x += manette.AxisAX;
        //  y += manette.AxisAY;

        //  if (x > 19)
        //    x = 19;

        //  if (y > 19)
        //    y = 19;

        //  if (x < 0)
        //    x = 0;

        //  if (y < 0)
        //    y = 0;

        //  cercles = new CercleList(r, 5, 360 / r);
        //}

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