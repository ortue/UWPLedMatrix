using LedLibrary.Classes;
using LedLibrary.Entities;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.CodeAnalysis.Operations;
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
        int r = random.Next(0, 127);
        int g = random.Next(0, 127);
        int b = random.Next(0, 127);

        int y = 0;

        while (y < Util.Context.Pixels.Hauteur - bot[x])
        {
          EffacerDernier(x, y);
          Util.Context.Pixels.GetCoordonnee(x, y).Set(r, g, b);
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
       // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      int i = 0;
      int j = 0;
      int k = 0;
      int l = 0;

      while (Util.TaskWork(task))
      {
        Util.Context.Pixels.GetCoordonnee(Util.Context.Pixels.GetCercleCoord(new Coordonnee { X = 5, Y = 5 }, i, 5)).SetColor(new Couleur { R = 127, B = 5 });

        j = 360 - i;
        Util.Context.Pixels.GetCoordonnee(Util.Context.Pixels.GetCercleCoord(new Coordonnee { X = 14, Y = 5 }, j, 5)).SetColor(new Couleur { R = 127, B = 5 });

        k = (180 + 360) % 360 - i;
        Util.Context.Pixels.GetCoordonnee(Util.Context.Pixels.GetCercleCoord(new Coordonnee { X = 5, Y = 14 }, k, 5)).SetColor(new Couleur { R = 127, B = 5 });

        l = (180 + i) % 360;
        Util.Context.Pixels.GetCoordonnee(Util.Context.Pixels.GetCercleCoord(new Coordonnee { X = 14, Y = 14 }, l, 5)).SetColor(new Couleur { R = 127, B = 5 });

        i += 5;

        if (i > 360)
          i = 0;

        //Background
        //Util.Context.Pixels.BackGround();
        Util.SetLeds();
        Util.Context.Pixels.Reset();

      }
    }
  }
}