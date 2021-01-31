using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Threading;
using WebMatrix.Context;

namespace WebMatrix.Classes
{
  public class Jeu
  {
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
        using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);

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
          waitHandle.Wait(TimeSpan.FromMilliseconds(1500));

        //Mure du haut et du bas
        pong.Horizontal(Util.Context.Pixels.Hauteur);

        //Position de la balle
        pong.X += pong.XX;
        pong.Y += pong.YY;

        //Pointage
        Util.Context.Pixels.Print(pong.ScoreP1.ToString("00"), 1, 2, scoreColor);
        Util.Context.Pixels.Print(pong.ScoreP2.ToString("00"), 12, 2, scoreColor);

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
          waitHandle.Wait(TimeSpan.FromMilliseconds(pong.Vitesse));
      }
    }

    /// <summary>
    /// Serpent
    /// </summary>
    public static void Serpent()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      int cycle = 0;
      Random r = new Random();
      JeuSerpent jeuSerpent = new JeuSerpent(Util.Context.Pixels.Largeur, Util.Context.Pixels.Hauteur);

      while (Util.TaskWork(task))
      {
        //Pointage
        Util.Context.Pixels.Print(jeuSerpent.Score.ToString(), 2, 13, Couleur.Get(127, 127, 127));

        //La balle       
        Util.Context.Pixels.GetCoordonnee(jeuSerpent.X, jeuSerpent.Y).Set(r.Next(127), r.Next(127), r.Next(127));

        //Bordure
        for (int i = 0; i < Util.Context.Pixels.Largeur; i++)
        {
          Util.Context.Pixels.GetCoordonnee(i, 0).Set(64, 64, 127);
          Util.Context.Pixels.GetCoordonnee(i, 19).Set(64, 64, 127);
        }

        for (int i = 0; i < Util.Context.Pixels.Hauteur; i++)
        {
          Util.Context.Pixels.GetCoordonnee(0, i).Set(64, 64, 127);
          Util.Context.Pixels.GetCoordonnee(19, i).Set(64, 64, 127);
        }

        //Mouvement
        bool mort = jeuSerpent.Mouvement(cycle++);
        int degrade = 1;

        //Serpent
        foreach (Serpent serpent in jeuSerpent.Serpents)
          if (CouleurSerpent(degrade++) is Couleur couleurSerpent)
            Util.Context.Pixels.GetCoordonnee(serpent.X, serpent.Y).SetColor(couleurSerpent);

        //Le serpent mange une balle
        bool manger = jeuSerpent.Manger();

        //Background
        Util.Context.Pixels.BackGround(3);
        Util.SetLeds();
        Util.Context.Pixels.Reset();

        using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(jeuSerpent.Vitesse));

        if (manger || mort)
          waitHandle.Wait(TimeSpan.FromMilliseconds(1000));
      }
    }

    /// <summary>
    /// Couleur Serpent
    /// </summary>
    /// <param name="degrade"></param>
    /// <returns></returns>
    public static Couleur CouleurSerpent(int degrade)
    {
      if (degrade < 9)
        return Couleur.Get(31 / degrade, 127 / degrade, 31 / degrade);

      return Couleur.Get(31 / 9, 127 / 9, 31 / 9);
    }

    /// <summary>
    /// Tetris
    /// </summary>
    public static void Tetris()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      int cycle = 0;
      Tetris tetris = new Tetris();

      while (Util.TaskWork(task))
      {
        //Pointage
        Util.Context.Pixels.Print("0", 14, 8, Couleur.Get(127, 127, 127));

        //Bordure
        for (int i = 1; i < 11; i++)
          Util.Context.Pixels.GetCoordonnee(i, 19).Set(64, 64, 127);

        for (int i = 0; i < Util.Context.Pixels.Hauteur; i++)
        {
          Util.Context.Pixels.GetCoordonnee(1, i).Set(64, 64, 127);
          Util.Context.Pixels.GetCoordonnee(11, i).Set(64, 64, 127);
        }

        for (int i = 13; i < 19; i++)
          Util.Context.Pixels.GetCoordonnee(i, 6).Set(64, 64, 127);


        //Next
        tetris.Next();


        Util.Context.Pixels.GetCoordonnee(15, 1).Set(127, 0, 127);
        Util.Context.Pixels.GetCoordonnee(15, 2).Set(127, 0, 127);
        Util.Context.Pixels.GetCoordonnee(16, 2).Set(127, 0, 127);
        Util.Context.Pixels.GetCoordonnee(16, 3).Set(127, 0, 127);



        //Piece
        Util.Context.Pixels.GetCoordonnee(6, 1).Set(127, 127, 0);
        Util.Context.Pixels.GetCoordonnee(6, 2).Set(127, 127, 0);
        Util.Context.Pixels.GetCoordonnee(7, 2).Set(127, 127, 0);
        Util.Context.Pixels.GetCoordonnee(6, 3).Set(127, 127, 0);


        //Background
        Util.Context.Pixels.BackGround(1);
        Util.SetLeds();
        Util.Context.Pixels.Reset();

        using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(10));

//        if (manger || mort)
//          waitHandle.Wait(TimeSpan.FromMilliseconds(1000));
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