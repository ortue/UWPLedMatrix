using Library.Collection;
using Library.Entity;
using Library.Util;
using System.Net.NetworkInformation;

namespace BlazorAppMatrix.Components.Layout
{
  public partial class PingCheck
  {
    public List<long> Moyenne { get; set; } = [3];
    public long Max { get; set; } = 20;
    public int TimeOut { get; set; }
    public bool Inter { get; set; }
    public bool ShowTimeOut { get; set; }
    public long Ping { get; set; } = 0;

    public string PingStr
    {
      get
      {
        if (Ping == 0)
          return "--";

        return Ping.ToString();
      }
    }

    private void Set()
    {
      Task.Run(ExecPing);
    }

    private void ExecPing()
    {
      double cycle = 0;

      int task = TaskGo.StartTask("Ping");

      Ping pingSender = new();
      pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

      while (TaskGo.TaskWork(task))
      {
        if (cycle % 1000 == 0)
          ShowTimeOut = !ShowTimeOut;

        if (!Inter && cycle++ % 100 == 0)
        {
          pingSender.SendAsync("google.com", 10000);

          Inter = true;
        }

        for (int i = 1; i <= 20; i++)
          if (Moyenne.Count > i)
          {
            long point = Moyenne[^i];

            if (Ping > Max)
              Max = Ping;

            long maxY = 20 * point / Max;

            if (maxY == 0)
              maxY = 1;

            for (int y = 1; y <= maxY; y++)
              Pixels.Get(20 - i, 20 - y).SetColor(63 / i, 63 / i, 127 / i);
          }

        Pixels.Set(CaractereList.Print("PING", 1, 1, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));

        if (ShowTimeOut)
          Pixels.Set(CaractereList.Print(Math.Round(TimeOut * 100 / (double)Moyenne.Count, 0).ToString("0%"), 1, 7, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
        else
          Pixels.Set(CaractereList.Print(Moyenne.Average().ToString("0"), 20 - Moyenne.Average().ToString("0").Length * 4, 7, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));

        Pixels.Set(CaractereList.Print(PingStr, 20 - PingStr.Length * 4, 13, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));

        Background.Bleu(Pixels);
        Pixels.SendPixels();
        Pixels.Reset();
      }
    }

    private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
    {
      if (e.Reply is PingReply reply)
      {
        Ping = reply.RoundtripTime;
        Moyenne.Add(Ping);

        if (reply.Status != IPStatus.Success)
          TimeOut++;
      }

      Inter = false;
    }
  }
}