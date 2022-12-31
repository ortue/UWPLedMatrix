using Library.Entity;
using Nfw.Linux.Joystick.Simple;
using Nfw.Linux.Joystick.Xpad;

namespace BLedMatrix.Shared
{
  public partial class Manette
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecManette);
    }

    /// <summary>
    /// Manette
    /// </summary>
    private void ExecManette()
    {
      int task = TaskGo.StartTask();
      using ManualResetEventSlim waitHandle = new(false);

      using Joystick joystick = new("/dev/input/js0");
      var manette = new Library.Util.Manette();
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / (decimal)64000);
      joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      //32767
      decimal x = 0;
      decimal y = 0;

      while (TaskGo.TaskWork(task))
      {
        x += manette.AxisAX;
        y += manette.AxisAY;

        if (x > 19)
          x = 19;

        if (y > 19)
          y = 19;

        if (x < 0)
          x = 0;

        if (y < 0)
          y = 0;

        Pixels.Get(x, y).SetColor(Couleur.Rouge);
        
        if(manette.BtnA)
        Pixels.Get(0, 19).SetColor(Couleur.Rouge);

        if (manette.BtnB)
          Pixels.Get(1, 19).SetColor(Couleur.Rouge);

        if (manette.BtnX)
          Pixels.Get(2, 19).SetColor(Couleur.Rouge);

        if (manette.BtnY)
          Pixels.Get(3, 19).SetColor(Couleur.Rouge);

        if (manette.BtnL)
          Pixels.Get(4, 19).SetColor(Couleur.Rouge);

        if (manette.BtnR)
          Pixels.Get(5, 19).SetColor(Couleur.Rouge);

        if (manette.BtnZL)
          Pixels.Get(6, 19).SetColor(Couleur.Rouge);

        if (manette.BtnZR)
          Pixels.Get(7, 19).SetColor(Couleur.Rouge);

        if (manette.BtnMoins)
          Pixels.Get(8, 19).SetColor(Couleur.Rouge);

        if (manette.BtnPlus)
          Pixels.Get(9, 19).SetColor(Couleur.Rouge);

        if (manette.BtnAxisA)
          Pixels.Get(10, 19).SetColor(Couleur.Rouge);

        if (manette.BtnAxisB)
          Pixels.Get(11, 19).SetColor(Couleur.Rouge);

        if (manette.BtnO)
          Pixels.Get(12, 19).SetColor(Couleur.Rouge);

        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(10));
      }
    }
  }
}