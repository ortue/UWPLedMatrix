using LedLibrary.Classes;
using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Collections.Generic;
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
    public static void Go(int? demo)
    {
      switch (demo)
      {
        case 0:
          Demo1();
          break;

        case 1:
          Demo2();
          break;

        case 2:
          Demo3();
          break;

        case 3:
          Demo4();
          break;

        case 4:
          Demo5();
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

        Random random = new Random();
        int x = random.Next(0, Util.Context.Pixels.Largeur);
        int y = 0;

        Couleur couleur = Couleur.Rnd();

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

          using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            waitHandle.Wait(TimeSpan.FromMilliseconds(temp));

          y++;

          if (y >= Util.Context.Pixels.Hauteur - bot[x])
          {
            if (x > 0 && y < Util.Context.Pixels.Hauteur - bot[x - 1])
              EffacerDernier(x--, y);

            if (x < Util.Context.Pixels.Largeur - 1 && y < Util.Context.Pixels.Hauteur - bot[x + 1])
              EffacerDernier(x++, y);

            using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
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
    /// Pong
    /// </summary>
    public static void Demo2()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      Pong pong = new Pong();
      Couleur scoreColor = new Couleur { R = 127, G = 127, B = 127 };

      while (Util.TaskWork(task))
      {
        //Effacer la balle apres
        Util.Context.Pixels.GetCoordonnee(pong.X, pong.Y).SetColor();

        //Pointiller du milieux
        for (int i = 1; i < Util.Context.Pixels.Hauteur - 1; i += 2)
        {
          Util.Context.Pixels.GetCoordonnee(9, i).Set(16, 16, 32);
          Util.Context.Pixels.GetCoordonnee(10, i).Set(16, 16, 32);
        }

        //Mure des palettes, si y a un but pause 1.5 secondes
        if (pong.Palette(Util.Context.Pixels.Largeur))
          using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            waitHandle.Wait(TimeSpan.FromMilliseconds(1500));

        //Mure du haut et du bas
        pong.Horizontal(Util.Context.Pixels.Hauteur);

        //Position de la balle
        pong.X += pong.XX;
        pong.Y += pong.YY;

        //Pointage
        Util.Context.Pixels.Print(pong.ScorePad, 2, pong.ScoreP1.ToString(), scoreColor);
        Util.Context.Pixels.Print(12, 2, pong.ScoreP2.ToString(), scoreColor);

        //La balle
        Util.Context.Pixels.GetCoordonnee(pong.X, pong.Y).Set(16, 16, 127);

        //Position des palettes
        pong.PositionPalette();

        //Dessiner les palettes
        for (int i = -3; i < 3; i++)
        {
          Couleur paddle = new Couleur();

          if (i >= -2 && i < 3)
            paddle = new Couleur { R = 64, G = 127, B = 64 };

          int p1Int = (int)Math.Round(pong.Pad1, 0);
          int p2Int = (int)Math.Round(pong.Pad2, 0);

          //Pour effacer la palette quand on la bouge, sans effacer le score
          if (!(Util.Context.Pixels.GetCoordonnee(1, p1Int + i).Couleur.Egal(scoreColor) && paddle.IsNoir))
            Util.Context.Pixels.GetCoordonnee(1, p1Int + i).SetColor(paddle);

          //Pour effacer la palette quand on la bouge, sans effacer le score
          if (!(Util.Context.Pixels.GetCoordonnee(18, p2Int + i).Couleur.Egal(scoreColor) && paddle.IsNoir))
            Util.Context.Pixels.GetCoordonnee(18, p2Int + i).SetColor(paddle);
        }

        //Bordure
        for (int i = 0; i < Util.Context.Pixels.Largeur; i++)
        {
          Util.Context.Pixels.GetCoordonnee(i, 0).Set(64, 64, 127);
          Util.Context.Pixels.GetCoordonnee(i, 19).Set(64, 64, 127);
        }

        //Background
        Util.Context.Pixels.BackGround();
        Util.SetLeds();
        Util.Context.Pixels.Reset();

        if (pong.Vitesse > 0)
          using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            waitHandle.Wait(TimeSpan.FromMilliseconds(pong.Vitesse));
      }
    }

    /// <summary>
    /// Cercle
    /// </summary>
    public static void Demo3()
    {
      Util.Setup();
      int task = Util.StartTask();
      Random random = new Random();
      DateTime temp = DateTime.Now;
      CercleList cercles = new CercleList(4, 1, 90);

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
      }
    }

    /// <summary>
    /// EGC
    /// </summary>
    public static void Demo4()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      EGCList egcs = new EGCList(60, Util.Context.Largeur, Util.Context.Hauteur);

      while (Util.TaskWork(task))
      {
        Coeur(egcs.Next(9));

        foreach (EGC egc in egcs)
          Util.Context.Pixels.GetCoordonnee(egc.X, egc.Y).SetColor(egc.Couleur);

        Util.SetLeds();
        Util.Context.Pixels.Reset();

        using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(50));
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

    public static void Demo5()
    {
      // Initialize the led strip
      int i = 0;
      Util.Setup();
      int task = Util.StartTask();
      Random random = new Random();
      CercleList cercles = new CercleList(3, 5, 120);

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


        //Util.Context.Pixels.GetCoordonnee(14, 14).SetColor(Couleur.Get(0, 0, 127));
        //Util.Context.Pixels.GetCoordonnee(15, 14).SetColor(Couleur.Get(0, 0, 127));
        //Util.Context.Pixels.GetCoordonnee(14, 15).SetColor(Couleur.Get(0, 0, 127));
        //Util.Context.Pixels.GetCoordonnee(15, 15).SetColor(Couleur.Get(0, 0, 127));

        //Util.Context.Pixels.GetCoordonnee(10, 10).SetColor(Couleur.Get(127, 0, 0));
        //Util.Context.Pixels.GetCoordonnee(10, 11).SetColor(Couleur.Get(127, 0, 0));
        //Util.Context.Pixels.GetCoordonnee(9, 12).SetColor(Couleur.Get(127, 0, 0));
        //Util.Context.Pixels.GetCoordonnee(10, 12).SetColor(Couleur.Get(127, 0, 0));

        //Util.Context.Pixels.GetCoordonnee(13, 5).SetColor(Couleur.Get(0, 127, 0));
        //Util.Context.Pixels.GetCoordonnee(13, 6).SetColor(Couleur.Get(0, 127, 0));
        //Util.Context.Pixels.GetCoordonnee(12, 6).SetColor(Couleur.Get(0, 127, 0));
        //Util.Context.Pixels.GetCoordonnee(13, 7).SetColor(Couleur.Get(0, 127, 0));


        //Util.Context.Pixels.GetCoordonnee(5, 5).SetColor(Couleur.Get(127, 127, 0));
        //Util.Context.Pixels.GetCoordonnee(5, 6).SetColor(Couleur.Get(127, 127, 0));
        //Util.Context.Pixels.GetCoordonnee(6, 6).SetColor(Couleur.Get(127, 127, 0));
        //Util.Context.Pixels.GetCoordonnee(6, 7).SetColor(Couleur.Get(127, 127, 0));
      }
    }

    /// <summary>
    /// Cercle
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    public static Coordonnee Cercle(int degree, double rayon)
    {
      Coordonnee coord = new Coordonnee(Util.Context.Largeur, Util.Context.Hauteur);

      if (degree >= 0 && degree <= 180)
        coord.X = 10 + (int)(rayon * Math.Sin(Math.PI * degree / 180));
      else
        coord.X = 10 - (int)(rayon * -Math.Sin(Math.PI * degree / 180)) - 1;

      coord.Y = 10 - (int)(rayon * Math.Cos(Math.PI * degree / 180) + 0.5);

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
  }
}