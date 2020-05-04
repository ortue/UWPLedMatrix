using LedMatrix.Context;
using System;
using System.Linq;
using System.Threading;
using Windows.UI;
using Windows.UI.StartScreen;

namespace LedMatrix.Classes
{
  public class Demo
  {
    /// <summary>
    /// Go
    /// </summary>
    public static void Go()
    {
      Random random = new Random();
      int demo = random.Next(0, 2);

      switch (demo)
      {
        case 0:
          Demo1();
          break;

        case 1:
          Demo2();
          break;
      }
    }

    /// <summary>
    /// Demo1
    /// </summary>
    public static void Demo1()
    {
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
          Util.Context.Pixels.Reset();

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

            using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
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
        Util.Context.Pixels.GetCoordonnee(x, y - 1).SetColor(new Color());
    }

    /// <summary>
    /// Demo2
    /// </summary>
    public static void Demo2()
    {
      int task = Util.StartTask();

      decimal x = 10;
      decimal y = 10;

      Random random = new Random();
      decimal xx = random.Next(1, 11) / (decimal)10;
      decimal yy = random.Next(1, 11) / (decimal)10;
      int vitesse = 40;

      decimal p1 = 9;
      decimal p2 = 9;

      while (Util.TaskWork(task))
      {
        //Effacer la balle apres
        Util.Context.Pixels.GetCoordonnee(x, y).SetColor();

        //Pointiller du milieux
        for (int i = 1; i < Util.Context.Pixels.Hauteur - 1; i += 2)
        {
          Util.Context.Pixels.GetCoordonnee(9, i).Set(16, 16, 32);
          Util.Context.Pixels.GetCoordonnee(10, i).Set(16, 16, 32);
        }

        //Mure des palettes
        if (x + xx >= Util.Context.Pixels.Largeur - 3 || x + xx < 2)
        {
          xx -= (xx * 2);

          if (vitesse > 0)
            vitesse--;

          //Quand on pogne le boute de la palette augmenter le yy
          if (x > 10)
          {
            for (int i = -2; i < 3; i++)
              if (y + i == p2)
                yy += yy + (decimal)0.2 * i;
              else if (y == p2)
                yy += yy + (decimal)0.1;
          }
          else
          {
            for (int i = -2; i < 3; i++)
              if (y + i == p1)
                yy += yy + (decimal)0.2 * i;
              else if (y == p1)
                yy += yy + (decimal)0.1;
          }

          if (xx > 2)
            xx = 2;

          if (xx < -2)
            xx = -2;

          if (yy > 2)
            yy = 2;

          if (yy < -2)
            yy = -2;
        }

        //Position de la balle
        x += xx;

        //Mure du haut et du bas
        if (y + yy >= Util.Context.Pixels.Hauteur - 2 || y + yy < 1)
          yy -= (yy * 2);

        //Position de la balle
        y += yy;

        //Pointage
        Util.Context.Pixels.Print(5, 2, "0", new Color { R = 127, G = 127, B = 127 });
        Util.Context.Pixels.Print(12, 2, "0", new Color { R = 127, G = 127, B = 127 });

        //La balle
        Util.Context.Pixels.GetCoordonnee(x, y).Set(0, 0, 127);

        decimal vitessePalette = (decimal)0.5 - (40 - vitesse) / 100;

        //Position des palettes
        if (x > 9 && xx > 0)
        {
          if (p2 < y)
            p2 += vitessePalette;
          else if (p2 > y)
            p2 -= vitessePalette;

          if (p2 < 3)
            p2 = 3;

          if (p2 > 16)
            p2 = 16;
        }
        else if (x < 9 && xx < 0)
        {
          if (p1 < y)
            p1 += vitessePalette;
          else if (p1 > y)
            p1 -= vitessePalette;

          if (p1 < 3)
            p1 = 3;

          if (p1 > 16)
            p1 = 16;
        }

        //Dessiner les palette
        for (int i = -4; i < 4; i++)
        {
          Color paddle = new Color();

          if (i >= -2 && i < 3)
            paddle = new Color { R = 64, G = 127, B = 64 };

          Util.Context.Pixels.GetCoordonnee(1, p1 + i).SetColor(paddle);
          Util.Context.Pixels.GetCoordonnee(18, p2 + i).SetColor(paddle);
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

        if (vitesse > 0)
          using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            waitHandle.Wait(TimeSpan.FromMilliseconds(vitesse));
      }
    }
  }
}