using Library.Entity;
using Nfw.Linux.Joystick.Simple;

namespace BlazorAppMatrix.Components.Layout
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
      var manette = new Library.Util.Manette(10, 10);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 64000d);
      joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      //32767


      while (TaskGo.TaskWork(task))
      {
        manette.NextAxisA();

        Pixels.Get(manette.Pixel).SetColor(Couleur.Rouge);

        if (manette.BtnA)
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