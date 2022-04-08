using LedLibrary.Classes;
using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Linq;
using System.Threading;
using WebMatrix.Context;

namespace WebMatrix.Classes
{
  public class Demo
  {
    /// <summary>
    /// Go
    /// </summary>
    public static void Go(int? demo, Criteria criteria)
    {
      switch (demo)
      {
        case 1:
          Demo1();
          break;

        case 2:
          Demo2();
          break;

        case 3:
          Demo3();
          break;

        case 4:
          Demo4();
          break;

        //case 5:
        //  Demo5(criteria);
        //  break;

        case 5:
          Demo5();
          break;

        case 6:
          Demo6();
          break;

        case 7:
          Demo7();
          break;
      }
    }

    /// <summary>
    /// Demo1
    /// </summary>
    public static void Demo1()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      int[] bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

      while (Util.TaskWork(task))
      {
        if (bot.All(bo => bo > 20))
        {
          Util.Context.Pixels.Reset();
          bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        Random random = new();
        int x = random.Next(0, Util.Context.Pixels.Largeur);
        int y = 0;

        Couleur couleur = Couleur.Rnd;

        while (y < Util.Context.Pixels.Hauteur - bot[x])
        {
          EffacerDernier(x, y);
          Util.Context.Pixels.GetCoordonnee(x, y).SetColor(couleur);
          Util.SetLeds();

          int temp = 100;

          if (y > 2)
            temp = 50;

          if (y > 4)
            temp = 25;

          if (y > 6)
            temp = 1;

          using (ManualResetEventSlim waitHandle = new(false))
            waitHandle.Wait(TimeSpan.FromMilliseconds(temp));

          y++;

          if (y >= Util.Context.Pixels.Hauteur - bot[x])
          {
            if (x > 0 && y < Util.Context.Pixels.Hauteur - bot[x - 1])
              EffacerDernier(x--, y);

            if (x < Util.Context.Pixels.Largeur - 1 && y < Util.Context.Pixels.Hauteur - bot[x + 1])
              EffacerDernier(x++, y);

            using ManualResetEventSlim waitHandle = new(false);
            waitHandle.Wait(TimeSpan.FromMilliseconds(100));
          }
        }

        bot[x]++;
      }
    }

    /// <summary>
    /// EffacerDernier
    /// </summary>
    private static void EffacerDernier(int x, int y)
    {
      if (y > 0)
        Util.Context.Pixels.GetCoordonnee(x, y - 1).SetColor(new Couleur());
    }

    /// <summary>
    /// Cercle
    /// </summary>
    public static void Demo2()
    {
      Util.Setup();
      int task = Util.StartTask();
      Random random = new();
      DateTime temp = DateTime.Now;
      CercleList cercles = new(4, 1, 90);

      while (Util.TaskWork(task))
      {
        if (temp.AddMinutes(1) < DateTime.Now)
        {
          temp = DateTime.Now;
          int r = random.Next(2, 12);
          cercles = new CercleList(r, 1, 360 / r);
        }

        cercles.Variation();

        foreach (Cercle cercle in cercles)
          Util.Context.Pixels.GetCoordonnee(Util.Context.Pixels.GetCercleCoord(cercle.Centre, cercle.DegreeInter, cercle.Rayon)).SetColor(cercle.Couleur);

        cercles.SetDegree(5);

        //Background
        Util.SetLeds();
        Util.Context.Pixels.Reset();

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(2));
      }
    }

    /// <summary>
    /// EGC
    /// </summary>
    public static void Demo3()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      EGCList egcs = new(60, Util.Context.Largeur, Util.Context.Hauteur);

      while (Util.TaskWork(task))
      {
        Coeur(egcs.Next(9));

        foreach (EGC egc in egcs)
          Util.Context.Pixels.GetCoordonnee(egc.X, egc.Y).SetColor(egc.Couleur);

        Util.SetLeds();
        Util.Context.Pixels.Reset();

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(50));
      }
    }

    /// <summary>
    /// Spiral
    /// </summary>
    public static void Demo4()
    {
      // Initialize the led strip
      int i = 1;
      Util.Setup();
      int task = Util.StartTask();
      Random random = new();
      CercleList cercles = new(3, 5, 120);

      while (Util.TaskWork(task))
      {
        if (i++ % 250 == 249)
        {
          int r = random.Next(2, 9);
          cercles = new CercleList(r, 5, 360 / r);
        }

        for (double rayon = 1; rayon < 15; rayon += 0.4)
          foreach (Cercle cercle in cercles)
            if (Cercle(360 - (cercle.DegreeInter - (int)(rayon * 10)) % 360, rayon) is Coordonnee coord)
              Util.Context.Pixels.GetCoordonnee(coord).SetColor(cercle.Couleur);

        cercles.SetDegree(9);
        Util.SetLeds();

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(8));
      }
    }

    /// <summary>
    /// stroboscope
    /// </summary>
    //public static void stroboscope(Criteria criteria)
    //{
    //  int i = 0;
    //  Util.Setup();
    //  int task = Util.StartTask();

    //  while (Util.TaskWork(task))
    //  {
    //    foreach (Pixel pixel in Util.Context.Pixels)
    //      if (i % 2 == 1)
    //        pixel.SetColor();
    //      else
    //        pixel.SetColor(Couleur.Get(100, 100, 127));

    //    i++;
    //    Util.SetLeds();

    //    using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
    //    waitHandle.Wait(TimeSpan.FromMilliseconds(criteria.CmbStroboscope));
    //  }
    //}

    /// <summary>
    /// Tunnel
    /// </summary>
    public static void Demo5()
    {
      // Initialize the led strip
      int i = 1;
      Util.Setup();
      int task = Util.StartTask();
      CercleList cercles = new(3, 5);

      while (Util.TaskWork(task))
      {
        foreach (Cercle cercle in cercles)
          for (int degree = 0; degree < 360; degree += 5)
            if (Cercle(degree, cercle.Rayon, cercle.X, cercle.Y) is Coordonnee coord)
              Util.Context.Pixels.GetCoordonnee(coord).SetColor(cercle.Couleur);

        cercles.SetRayon(0.3, i++ % 2000 > 1000);

        Util.SetLeds();

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(2));
      }
    }

    /// <summary>
    /// Oscilloscope
    /// </summary>
    public static void Demo6()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      int x = 0;
      Random random = new();
      SinusList sinus = new(random.Next(1, 4));

      while (Util.TaskWork(task))
      {
        if (x++ % 10000 == 0)
          sinus = new SinusList(random.Next(1, 4));

        foreach (Sinus sin in sinus)
          Util.Context.Pixels.GetCoordonnee(sin.Coord).SetColor(sin.Couleur);

        sinus.Next();
        Util.SetLeds();

        Util.Context.Pixels.Reset();
      }
    }

    /// <summary>
    /// Coeur
    /// </summary>
    public static void Coeur(bool battement)
    {
      if (!battement)
      {
        Util.Context.Pixels.GetCoordonnee(13, 1).SetColor(Couleur.Get(15, 0, 0));
        Util.Context.Pixels.GetCoordonnee(17, 1).SetColor(Couleur.Get(15, 0, 0));

        Util.Context.Pixels.GetCoordonnee(12, 2).SetColor(Couleur.Get(15, 0, 0));
        Util.Context.Pixels.GetCoordonnee(18, 2).SetColor(Couleur.Get(15, 0, 0));

        Util.Context.Pixels.GetCoordonnee(12, 3).SetColor(Couleur.Get(15, 0, 0));
        Util.Context.Pixels.GetCoordonnee(18, 3).SetColor(Couleur.Get(15, 0, 0));

        Util.Context.Pixels.GetCoordonnee(13, 4).SetColor(Couleur.Get(15, 0, 0));
        Util.Context.Pixels.GetCoordonnee(17, 4).SetColor(Couleur.Get(15, 0, 0));

        Util.Context.Pixels.GetCoordonnee(14, 5).SetColor(Couleur.Get(15, 0, 0));
        Util.Context.Pixels.GetCoordonnee(16, 5).SetColor(Couleur.Get(15, 0, 0));

        Util.Context.Pixels.GetCoordonnee(15, 6).SetColor(Couleur.Get(15, 0, 0));
      }

      Util.Context.Pixels.GetCoordonnee(14, 1).SetColor(Couleur.Get(25, 0, 0));
      Util.Context.Pixels.GetCoordonnee(16, 1).SetColor(Couleur.Get(25, 0, 0));

      Util.Context.Pixels.GetCoordonnee(13, 2).SetColor(Couleur.Get(25, 0, 0));
      Util.Context.Pixels.GetCoordonnee(14, 2).SetColor(Couleur.Get(35, 0, 0));
      Util.Context.Pixels.GetCoordonnee(15, 2).SetColor(Couleur.Get(25, 0, 0));
      Util.Context.Pixels.GetCoordonnee(16, 2).SetColor(Couleur.Get(35, 0, 0));
      Util.Context.Pixels.GetCoordonnee(17, 2).SetColor(Couleur.Get(25, 0, 0));

      Util.Context.Pixels.GetCoordonnee(13, 3).SetColor(Couleur.Get(25, 0, 0));
      Util.Context.Pixels.GetCoordonnee(14, 3).SetColor(Couleur.Get(35, 0, 0));
      Util.Context.Pixels.GetCoordonnee(15, 3).SetColor(Couleur.Get(25, 0, 0));
      Util.Context.Pixels.GetCoordonnee(16, 3).SetColor(Couleur.Get(35, 0, 0));
      Util.Context.Pixels.GetCoordonnee(17, 3).SetColor(Couleur.Get(25, 0, 0));

      Util.Context.Pixels.GetCoordonnee(14, 4).SetColor(Couleur.Get(25, 0, 0));
      Util.Context.Pixels.GetCoordonnee(15, 4).SetColor(Couleur.Get(35, 0, 0));
      Util.Context.Pixels.GetCoordonnee(16, 4).SetColor(Couleur.Get(25, 0, 0));

      Util.Context.Pixels.GetCoordonnee(15, 5).SetColor(Couleur.Get(25, 0, 0));
    }

    /// <summary>
    /// Cercle
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="rayon"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Coordonnee Cercle(int degree, double rayon, int x = 10, int y = 10)
    {
      Coordonnee coord = new(Util.Context.Largeur, Util.Context.Hauteur);

      if (degree >= 0 && degree <= 180)
        coord.X = x + (int)(rayon * Math.Sin(Math.PI * degree / 180));
      else
        coord.X = x - (int)(rayon * -Math.Sin(Math.PI * degree / 180)) - 1;

      coord.Y = y - (int)(rayon * Math.Cos(Math.PI * degree / 180) + 0.5);

      if (coord.X < 0)
        return null;

      if (coord.Y < 0)
        return null;

      if (coord.X > coord.MaxX - 1)
        return null;

      if (coord.Y > coord.MaxY - 1)
        return null;

      return coord;
    }

    /// <summary>
    /// Plasma
    /// </summary>
    public static void Demo7()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      decimal cycle = 0;

      while (Util.TaskWork(task))
      {
        cycle = Util.Context.Pixels.Plasma(127, cycle);
        Util.SetLeds();
        Util.Context.Pixels.Reset();

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(10));
      }
    }

    /// <summary>
    /// Plasma
    /// </summary>
    public static void PlasmaBeta()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      decimal cycle = 0;
      bool inter = false;
      int multi = 49;
      double hueShift = 0;
      double[,] buffer = new double[Util.Context.Largeur * (multi + 1), Util.Context.Hauteur];

      for (int y = 0; y < Util.Context.Hauteur; y++)
        for (int x = 0; x < Util.Context.Largeur * (multi + 1); x++)
        {
          double value = Math.Sin(x / 8.0);
          value += Math.Sin(y / 4.0);
          value += Math.Sin((x + y) / 8.0);
          value += Math.Sin(Math.Sqrt(x * x + y * y) / 4.0);
          value += 4; // shift range from -4 .. 4 to 0 .. 8
          value /= 8; // bring range down to 0 .. 1

          buffer[x, y] = value;
        }

      while (Util.TaskWork(task))
      {
        hueShift = (hueShift + 0.02) % 1;

        cycle += (decimal)0.02;

        if (cycle % Util.Context.Largeur * multi == 0)
        {
          if (inter)
            inter = false;
          else
            inter = true;
        }

        int offset = (int)(cycle % Util.Context.Largeur * multi);

        if (inter)
          offset = (int)(Util.Context.Largeur * multi - cycle % Util.Context.Largeur * multi);

        for (int y = 0; y < Util.Context.Hauteur; y++)
          for (int x = 0; x < Util.Context.Largeur; x++)
          {
            double hue = hueShift + buffer[x + offset, y] % 1;
            Couleur couleur = HSVtoRGB(hue, 1, 1, 10);
            Util.Context.Pixels.GetCoordonnee(x, y).SetColor(couleur);
          }

        Util.SetLeds();
        Util.Context.Pixels.Reset();
      }
    }

    /// <summary>
    /// HSVtoRGB
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    private static Couleur HSVtoRGB(double h, int s, int v, int alpha)
    {
      int i = (int)Math.Floor(h * 6);
      double f = h * 6 - i;
      int p = v * (1 - s);
      double q = v * (1 - f * s);
      double t = v * (1 - (1 - f) * s);

      return (i % 6) switch
      {
        0 => Couleur.Get(v * alpha, (int)Math.Round(t * alpha), p * alpha),
        1 => Couleur.Get((int)Math.Round(q * alpha), v * alpha, p * alpha),
        2 => Couleur.Get(p * alpha, v * alpha, (int)Math.Round(t * alpha)),
        3 => Couleur.Get(p * alpha, (int)Math.Round(q * alpha), v * alpha),
        4 => Couleur.Get((int)Math.Round(t * alpha), p * alpha, v * alpha),
        _ => Couleur.Get(v * alpha, p * alpha, (int)Math.Round(q * alpha))
      };
    }
  }
}