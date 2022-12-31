using Library.Collection;

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

        foreach (var cercle in cercles)
          Pixels.Get(PixelList.GetCercleCoord(cercle.Centre, cercle.DegreeInter, cercle.Rayon)).SetColor(cercle.Couleur);

        cercles.SetDegree(5);

        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(4));
      }
    }
  }
}