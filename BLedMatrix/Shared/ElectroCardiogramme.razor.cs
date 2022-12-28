using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class ElectroCardiogramme
  {
    private void Set()
    {
      int task = TaskGo.StartTask();

      Random random = new();
      DateTime temp = DateTime.Now;
      //CercleList cercles = new(4, 1, 90);

      while (TaskGo.TaskWork(task))
      {
        //if (temp.AddMinutes(1) < DateTime.Now)
        //{
        //  temp = DateTime.Now;
        //  int r = random.Next(2, 12);
        //  cercles = new CercleList(r, 1, 360 / r);
        //}

        //cercles.Variation();

        //foreach (Cercle cercle in cercles)
        //  Pixels.Get(Util.Context.Pixels.GetCercleCoord(cercle.Centre, cercle.DegreeInter, cercle.Rayon)).SetColor(cercle.Couleur);

        //cercles.SetDegree(5);


        Pixels.Get(1, 1).SetColor(Couleur.Rouge);

        Pixels.SendPixels();
        Pixels.Reset();

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(2));
      }
    }
  }
}