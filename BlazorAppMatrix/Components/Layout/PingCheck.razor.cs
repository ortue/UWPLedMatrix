namespace BlazorAppMatrix.Components.Layout
{
  public partial class PingCheck
  {
    private void Set()
    {
      Task.Run(ExecPing);
    }

    private void ExecPing()
    {
      double cycle = 0;
      //Random r = new();
      //int bg = r.Next(1, Util.Context.Pixels.NbrBackground);

      ////Aime pas beaucoup le 3 et le 4 ici
      //if (bg == 3 || bg == 4)
      //  bg = 2;

      //int task = TaskGo.StartTask("Ping");
      ////CaractereList caracteres = new(20);

      //while (TaskGo.TaskWork(task))
      //{
      //  SetHorloge(cycle);
      //  Pixels.SendPixels();
      //  Pixels.Reset();
      //}
    }
  }
}