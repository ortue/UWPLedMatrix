namespace BLedMatrix.Shared
{
  public partial class Plasma
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecPlasma);
    }

    /// <summary>
    /// Plasma
    /// </summary>
    private void ExecPlasma()
    {
      int task = TaskGo.StartTask();
      decimal cycle = 0;
      using ManualResetEventSlim waitHandle = new(false);

      var plasma = new Library.Util.Plasma(Pixels);

      while (TaskGo.TaskWork(task))
      {
        cycle = plasma.SetPlasma(127, cycle);
        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(10));
      }
    }
  }
}