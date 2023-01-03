using Library.Collection;
using Library.Entity;
using Library.Util;
using Nfw.Linux.Joystick.Simple;

namespace BLedMatrix.Shared
{
  public partial class Tetris
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecTetris);
    }

    /// <summary>
    /// Tetris
    /// </summary>
    private void ExecTetris()
    {
      int task = TaskGo.StartTask();
      int cycle = 0;
      int topScore = 0;
      var tetris = new Library.Entity.Tetris();
      
      using ManualResetEventSlim waitHandle = new(false);
      
      using Joystick joystick = new("/dev/input/js0");
      var manette = new Library.Util.Manette(10, 10);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 32767d);
      joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      while (TaskGo.TaskWork(task))
      {
        //Pointage
        Pixels.Set(CaractereList.Print(tetris.ScoreUn, 15, 8, Couleur.Get(127, 127, 127)));
        Pixels.Set(CaractereList.Print(tetris.ScoreDeux, 15, 14, Couleur.Get(127, 127, 127)));

        foreach (TetrisPiece centaine in tetris.Centaines)
          Pixels.Get(centaine.X, centaine.Y).SetColor(127, 127, 127);

        foreach (TetrisPiece millier in tetris.Milliers)
          Pixels.Get(millier.X, millier.Y).SetColor(127, 127, 0);

        //Bordure
        for (int i = 1; i < 12; i++)
          Pixels.Get(i, 19).SetColor(64, 64, 127);

        for (int i = 0; i < PixelList.Hauteur; i++)
        {
          Pixels.Get(1, i).SetColor(64, 64, 127);
          Pixels.Get(12, i).SetColor(64, 64, 127);
        }

        for (int i = 13; i < 20; i++)
          Pixels.Get(i, 6).SetColor(64, 64, 127);

        //Next
        foreach (TetrisPiece next in tetris.Nexts)
          Pixels.Get(next.X + 16, next.Y + 1).SetColor(next.Couleur);

        //Piece tombé
        foreach (TetrisPiece pieceTombe in tetris.PieceTombes)
          if (Pixels.Get(pieceTombe.X, pieceTombe.Y) is Pixel pixel)
            pixel.SetColor(pieceTombe.Couleur);

        //Mouvement
        tetris.Mouvement(cycle++, manette);

        //Piece
        foreach (TetrisPiece piece in tetris.Pieces)
          if (Pixels.Get(piece.X + tetris.X, piece.Y + tetris.Y) is Pixel pixel)
            pixel.SetColor(piece.Couleur);

        //Rendu en bas on travail
        bool nouvellePiece = tetris.Bottom();

        //Enlever les lignes pleine, avec une animation
        if (tetris.EffacerLigne() is TetrisPieceList tetrisPieces)
          TetrisAnimation(Pixels, tetrisPieces);

        Background.Grichage(Pixels);
        Pixels.SendPixels();

        if (nouvellePiece)
        {
          if (tetris.Mort)
          {
            manette.Start = false;

            if (tetris.Score > topScore)
              topScore = tetris.Score;

            foreach (Pixel pixel in Pixels)
              pixel.Fade(4);

            Pixels.Set(CaractereList.Print("TOP", 4, 2, Couleur.Get(127, 127, 127)));
            Pixels.Set(CaractereList.Print(topScore.ToString(), 3, 12, Couleur.Get(127, 127, 127)));

            Pixels.SendPixels();

            waitHandle.Wait(TimeSpan.FromMilliseconds(10000));

            tetris = new Library.Entity.Tetris();
          }
          else
            tetris.NouvellePiece();
        }

        Pixels.Reset();
      }
    }

    /// <summary>
    /// TetrisAnimation
    /// </summary>
    private static void TetrisAnimation(PixelList pixels, TetrisPieceList tetrisPieces)
    {
      using ManualResetEventSlim waitHandle = new(false);

      Background.Grichage(pixels);

      if (tetrisPieces.Any())
        for (int anime = 0; anime < 10; anime++)
        {
          foreach (TetrisPiece pieceTombe in tetrisPieces)
            if (pixels.Get(pieceTombe.X, pieceTombe.Y) is Pixel pixel)
            {
              if (anime % 2 == 0)
                foreach (TetrisPiece piece in tetrisPieces)
                  piece.Couleur = Couleur.Noir;
              else
                foreach (TetrisPiece piece in tetrisPieces)
                  piece.Couleur = piece.TmpCouleur;

              pixel.SetColor(pieceTombe.Couleur);
            }

          pixels.SendPixels();
          waitHandle.Wait(TimeSpan.FromMilliseconds(40));
        }
    }
  }
}