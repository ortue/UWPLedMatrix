using LedLibrary.Collection;
using LedLibrary.Entities;
using Nfw.Linux.Joystick.Simple;
using System;
using System.Linq;
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
      Pong pong = new();
      Couleur scoreColor = new() { R = 127, G = 127, B = 127 };

      while (Util.TaskWork(task))
      {
        using ManualResetEventSlim waitHandle = new(false);

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
          Couleur paddle = new();

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
      Random r = new();
      JeuSerpent jeuSerpent = new(Util.Context.Pixels.Largeur, Util.Context.Pixels.Hauteur);

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

        using ManualResetEventSlim waitHandle = new(false);
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
      int topScore = 0;
      Tetris tetris = new();

      while (Util.TaskWork(task))
      {
        //Pointage
        Util.Context.Pixels.Print(tetris.ScoreUn, 15, 8, Couleur.Get(127, 127, 127));
        Util.Context.Pixels.Print(tetris.ScoreDeux, 15, 14, Couleur.Get(127, 127, 127));

        foreach (TetrisPiece centaine in tetris.Centaines)
          Util.Context.Pixels.GetCoordonnee(centaine.X, centaine.Y).Set(127, 127, 127);

        foreach (TetrisPiece millier in tetris.Milliers)
          Util.Context.Pixels.GetCoordonnee(millier.X, millier.Y).Set(127, 127, 0);

        //Bordure
        for (int i = 1; i < 12; i++)
          Util.Context.Pixels.GetCoordonnee(i, 19).Set(64, 64, 127);

        for (int i = 0; i < Util.Context.Pixels.Hauteur; i++)
        {
          Util.Context.Pixels.GetCoordonnee(1, i).Set(64, 64, 127);
          Util.Context.Pixels.GetCoordonnee(12, i).Set(64, 64, 127);
        }

        for (int i = 13; i < 20; i++)
          Util.Context.Pixels.GetCoordonnee(i, 6).Set(64, 64, 127);

        //Next
        foreach (TetrisPiece next in tetris.Nexts)
          Util.Context.Pixels.GetCoordonnee(next.X + 16, next.Y + 1).SetColor(next.Couleur);

        //Piece tombé
        foreach (TetrisPiece pieceTombe in tetris.PieceTombes)
          if (Util.Context.Pixels.GetCoordonnee(pieceTombe.X, pieceTombe.Y) is Pixel pixel)
            pixel.SetColor(pieceTombe.Couleur);

        //Mouvement
        tetris.Mouvement(cycle++);

        //Piece
        foreach (TetrisPiece piece in tetris.Pieces)
          if (Util.Context.Pixels.GetCoordonnee(piece.X + tetris.X, piece.Y + tetris.Y) is Pixel pixel)
            pixel.SetColor(piece.Couleur);

        //Rendu en bas on travail
        bool nouvellePiece = tetris.Bottom();

        //Enlever les lignes pleine, avec une animation
        if (tetris.EffacerLigne() is TetrisPieceList tetrisPieces)
          TetrisAnimation(Util.Context.Pixels, tetrisPieces);

        //Background
        Util.Context.Pixels.BackGround(1);
        Util.SetLeds();

        if (nouvellePiece)
        {
          if (tetris.Mort)
          {
            if (tetris.Score > topScore)
              topScore = tetris.Score;

            foreach (Pixel pixel in Util.Context.Pixels)
              pixel.Fade(4);

            Util.Context.Pixels.Print("TOP", 4, 2, Couleur.Get(127, 127, 127));
            Util.Context.Pixels.Print(topScore.ToString(), 3, 12, Couleur.Get(127, 127, 127));
            Util.SetLeds();

            using ManualResetEventSlim waitHandle = new(false);
            waitHandle.Wait(TimeSpan.FromMilliseconds(10000));

            tetris = new Tetris();
          }
          else
            tetris.NouvellePiece();
        }

        Util.Context.Pixels.Reset();
      }
    }

    /// <summary>
    /// TetrisAnimation
    /// </summary>
    private static void TetrisAnimation(PixelList pixels, TetrisPieceList tetrisPieces)
    {
      pixels.BackGround(1);

      if (tetrisPieces.Any())
        for (int anime = 0; anime < 10; anime++)
        {
          foreach (TetrisPiece pieceTombe in tetrisPieces)
            if (pixels.GetCoordonnee(pieceTombe.X, pieceTombe.Y) is Pixel pixel)
            {
              if (anime % 2 == 0)
                foreach (TetrisPiece piece in tetrisPieces)
                  piece.Couleur = Couleur.Noir;
              else
                foreach (TetrisPiece piece in tetrisPieces)
                  piece.Couleur = piece.TmpCouleur;

              pixel.SetColor(pieceTombe.Couleur);
            }

          Util.SetLeds();

          using ManualResetEventSlim waitHandle = new(false);
          waitHandle.Wait(TimeSpan.FromMilliseconds(40));
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
      LabyrintheList labyrinthes = SetLabyrinthe();
      decimal cycle = 0;
      using ManualResetEventSlim waitHandle = new(false);

      while (Util.TaskWork(task))
      {
        if (labyrinthes.Complet)
        {
          labyrinthes = SetLabyrinthe();
          waitHandle.Wait(TimeSpan.FromMilliseconds(500));
        }

        foreach (Labyrinthe labyrinthe in labyrinthes)
        {
          if (labyrinthe.Mur && !labyrinthe.Couleur.Egal(new Couleur()))
            labyrinthe.Couleur = Couleur.Get(127 - labyrinthe.X * 6, (labyrinthes.X + labyrinthes.Y) * 3, labyrinthe.Y * 6);

          Util.Context.Pixels.GetCoordonnee(labyrinthe.X, labyrinthe.Y).SetColor(labyrinthe.Couleur);
        }

        cycle = Util.Context.Pixels.BackGround(4, cycle, true);
        Util.Context.Pixels.GetCoordonnee(labyrinthes.X, labyrinthes.Y).SetColor(Couleur.Get(127, 127, 127));

        if (cycle % 2 == 0)
          labyrinthes.Mouvement();

        Util.SetLeds();
        Util.Context.Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(10));
      }
    }

    /// <summary>
    /// SetLabyrinthe
    /// </summary>
    /// <returns></returns>
    private static LabyrintheList SetLabyrinthe()
    {
      Maze maze = new(8, 8);
      LabyrintheList labyrinthes = new(1 + maze.Width * 2, 1 + maze.Height * 2);

      for (int y = 0; y < maze.Height; y++)
        for (int x = 0; x < maze.Width; x++)
        {
          int xx = 1 + x * 2;
          int yy = 1 + y * 2;
          labyrinthes.AddNew(xx, yy, true);

          if (maze[x, y].HasFlag(CellState.Top))
            labyrinthes.AddNew(xx + 1, yy, true);

          if (maze[x, y].HasFlag(CellState.Left))
            labyrinthes.AddNew(xx, yy + 1, true);

          //Ligne du bas
          labyrinthes.AddNew(xx, 1 + maze.Height * 2, true);
          labyrinthes.AddNew(xx + 1, 1 + maze.Height * 2, true);

          //Ligne de droite
          labyrinthes.AddNew(1 + maze.Width * 2, yy, true);
          labyrinthes.AddNew(1 + maze.Width * 2, yy + 1, true);
          labyrinthes.AddNew(1 + maze.Width * 2, yy + 2, true);
        }

      labyrinthes.SetChemin();
      labyrinthes.SetCheminParcouru();

      return labyrinthes;
    }

    /// <summary>
    /// Arkanoid
    /// </summary>
    public static void Arkanoid()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      Arkanoid arkanoid = new(Util.Context.Pixels.Hauteur, Util.Context.Pixels.Largeur);

      while (Util.TaskWork(task))
      {
        using ManualResetEventSlim waitHandle = new(false);

        //Effacer la balle apres
        Util.Context.Pixels.GetCoordonnee(arkanoid.X, arkanoid.Y).SetColor();

        //Mure du bas, pause 1.5 secondes
        if (arkanoid.Palette())
          waitHandle.Wait(TimeSpan.FromMilliseconds(1500));

        //Mure du haut et des coté
        arkanoid.Mure();

        if (arkanoid.CheckBrique())
          waitHandle.Wait(TimeSpan.FromMilliseconds(1500));

        //Position de la balle
        arkanoid.X += arkanoid.XX;
        arkanoid.Y += arkanoid.YY;

        //La balle
        Util.Context.Pixels.GetCoordonnee(arkanoid.X, arkanoid.Y).Set(16, 16, 127);

        //Position palette
        arkanoid.PositionPalette();

        //Dessiner la palette
        for (int i = -2; i < 2; i++)
        {
          Couleur paddle = new();

          if (i >= -1 && i < 2)
            paddle = new Couleur { R = 64, G = 127, B = 64 };

          int p1Int = (int)Math.Round(arkanoid.Pad, 0);

          //Pour effacer la palette quand on la bouge, sans effacer le score
          //if (!(Util.Context.Pixels.GetCoordonnee(1, p1Int + i).Couleur.Egal(scoreColor) && paddle.IsNoir))
          Util.Context.Pixels.GetCoordonnee(p1Int + i, 18).SetColor(paddle);
        }

        //Bordure
        for (int x = 0; x < Util.Context.Pixels.Largeur; x++)
          Util.Context.Pixels.GetCoordonnee(x, 0).Set(31, 31, 127);

        for (int y = 0; y < Util.Context.Pixels.Hauteur; y++)
        {
          Util.Context.Pixels.GetCoordonnee(0, y).Set(31, 31, 127);
          Util.Context.Pixels.GetCoordonnee(19, y).Set(31, 31, 127);
        }

        foreach (Brique brique in arkanoid.Briques.Where(b => b.Visible))
        {
          Util.Context.Pixels.GetCoordonnee(brique.X, brique.Y).Couleur = brique.Couleur;
          Util.Context.Pixels.GetCoordonnee(brique.XX, brique.Y).Couleur = brique.Couleur;
        }

        Util.Context.Pixels.BackGround();
        Util.SetLeds();
        Util.Context.Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(arkanoid.Vitesse / 2));
      }
    }

    public static void Manette()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      using ManualResetEventSlim waitHandle = new(false);
      using Joystick joystick = new("/dev/input/js0");

      int x = 0;
      int y = 0;
      int xx = 0;
      int yy = 0;

      while (Util.TaskWork(task))
      {
        joystick.AxisCallback = (j, axis, value) =>
        {
          //Console.WriteLine($"{j.DeviceName} => Axis[{axis}] => {value}");

          int valeur = (int)Math.Round((decimal)((value + 32767) / 3449), 0);

          if (axis == 0)
            x = valeur;

          if (axis == 1)
            y = valeur;

          if (axis == 2)
            xx = valeur;

          if (axis == 3)
            yy = valeur;

          //65534

          //if (axis == 0 || axis == 1)
            Util.Context.Pixels.GetCoordonnee(x, y).SetColor(Couleur.Get(127, 0, 0));

          //if (axis == 2 || axis == 3)
            Util.Context.Pixels.GetCoordonnee(xx, yy).SetColor(Couleur.Get(0, 0, 127));

          Util.SetLeds();
          Util.Context.Pixels.Reset();
          //waitHandle.Wait(TimeSpan.FromMilliseconds(10));
        };
      }
    }
  }
}