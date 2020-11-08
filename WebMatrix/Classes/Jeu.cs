using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Threading;
using WebMatrix.Context;

namespace WebMatrix.Classes
{
  public class Jeu
  {
    public static void Go(int? demo)
    {
      switch (demo)
      {
        case 1:
          Pong();
          break;

        case 3:
          Labyrinthe();
          break;
      }
    }

    /// <summary>
    /// Pong
    /// </summary>
    public static void Pong()
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
    /// Labyrinthe
    /// </summary>
    public static void Labyrinthe()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      Couleur Murs = new Couleur { R = 100, G = 100, B = 127 };

      LabyrintheList labyrinthe = new LabyrintheList(Util.Context.Pixels);

      while (Util.TaskWork(task))
      {



        Util.SetLeds();

        using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(10));
      }
    }
  }
}