using Library.Collection;
using Library.Entity;
using Library.Util;
using System.Net.NetworkInformation;

namespace BlazorAppMatrix.Components.Layout
{
  public partial class PingCheck
  {
    public List<long> Moyenne { get; set; } = [];
    public long Max { get; set; } = 20;
    public int TimeOut { get; set; }
    public bool Inter { get; set; }
    public bool ShowTimeOut { get; set; }

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
        if (cycle % 10000 == 0)
          ShowTimeOut = !ShowTimeOut;

        if (!Inter)
        {
          pingSender.SendAsync("google.com", 10000);

          Inter = true;
        }



        //cycle = SetPing(cycle);

        Background.Bleu(Pixels);
        Pixels.SendPixels();
      }
    }

    private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
    {
      //Ping pingSender = new();
      //bool showTimeOut = false;

      //if (cycle % 1000 < 500)
      //showTimeOut = !showTimeOut;

      //if (cycle++ % 100 == 0)
      //{
      Pixels.Reset();

      //PingReply reply = pingSender.Send("google.com", 10000);

      if (e.Reply is PingReply reply)
      {
        long ping = reply.RoundtripTime;

        if (reply.Status == IPStatus.Success)
          Moyenne.Add(ping);
        else
          TimeOut++;

        for (int i = 1; i <= 20; i++)
          if (Moyenne.Count > i)
          {
            long point = Moyenne[^i];

            if (ping > Max)
              Max = ping;

            long maxY = 20 * point / Max;

            if (maxY == 0)
              maxY = 1;

            for (int y = 1; y <= maxY; y++)
              Pixels.Get(20 - i, 20 - y).SetColor(63 / i, 63 / i, 127 / i);
          }

        int x = Moyenne.Average().ToString("0").Length * 4;

        Pixels.Set(CaractereList.Print("PING", 1, 1, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));

        if (ShowTimeOut)
          Pixels.Set(CaractereList.Print((TimeOut / (double)Moyenne.Count * 100).ToString("0%"), 1, 7, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
        else
          Pixels.Set(CaractereList.Print(Moyenne.Average().ToString("0"), 20 - x, 7, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));

        x = ping.ToString().Length * 4;

        if (reply.Status == IPStatus.Success)
          Pixels.Set(CaractereList.Print(ping.ToString(), 20 - x, 13, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
        else
          Pixels.Set(CaractereList.Print("--", 14, 13, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
      }

      using ManualResetEventSlim waitHandle = new(false);
      waitHandle.Wait(TimeSpan.FromMilliseconds(100));

      Inter = false;
    }

    private double SetPing(double cycle)
    {
      Ping pingSender = new();
      bool showTimeOut = false;

      if (cycle % 1000 < 500)
        showTimeOut = !showTimeOut;

      if (cycle++ % 100 == 0)
      {
        Pixels.Reset();

        PingReply reply = pingSender.Send("google.com", 10000);
        long ping = reply.RoundtripTime;

        if (reply.Status == IPStatus.Success)
          Moyenne.Add(ping);
        else
          TimeOut++;

        for (int i = 1; i <= 20; i++)
          if (Moyenne.Count > i)
          {
            long point = Moyenne[^i];

            if (ping > Max)
              Max = ping;

            long maxY = 20 * point / Max;

            if (maxY == 0)
              maxY = 1;

            for (int y = 1; y <= maxY; y++)
              Pixels.Get(20 - i, 20 - y).SetColor(63 / i, 63 / i, 127 / i);
          }

        int x = Moyenne.Average().ToString("0").Length * 4;

        Pixels.Set(CaractereList.Print("PING", 1, 1, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));

        if (showTimeOut)
          Pixels.Set(CaractereList.Print((TimeOut / (double)Moyenne.Count * 100).ToString("0%"), 1, 7, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
        else
          Pixels.Set(CaractereList.Print(Moyenne.Average().ToString("0"), 20 - x, 7, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));

        x = ping.ToString().Length * 4;

        if (reply.Status == IPStatus.Success)
          Pixels.Set(CaractereList.Print(ping.ToString(), 20 - x, 13, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
        else
          Pixels.Set(CaractereList.Print("--", 14, 13, Couleurs.Get("AffDate", "AnneeCouleur", Couleur.Bleu)));
      }

      return cycle;
    }
  }
}