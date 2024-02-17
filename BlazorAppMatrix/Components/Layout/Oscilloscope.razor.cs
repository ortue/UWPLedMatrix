using Library.Collection;
using Library.Entity;
using Nfw.Linux.Joystick.Simple;

namespace BlazorAppMatrix.Components.Layout
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

      using Joystick joystick = new("/dev/input/js0");
      var manette = new Library.Util.Manette(9, 9);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 1000000d);
      joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      while (TaskGo.TaskWork(task))
      {
        manette.NextAxisA(9, 9);

        if (x++ % 10000 == 0)
          sinus = new SinusList(random.Next(1, 4));

        if (manette.BtnA)
        {
          x = 1;
          sinus = new SinusList(1) { Manette = true };
        }

        foreach (Sinus sin in sinus)
        {
          if (sinus.Manette)
            sin.SinusManette(manette);

          Pixels.Get(sin.Coord).SetColor(sin.Couleur);
        }

        sinus.Next();

        //Pixels.Get(manette.Pixel).SetColor(Couleur.RougePale);

        Pixels.SendPixels();
        Pixels.Reset();
      }
    }
  }
}