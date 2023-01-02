using Library.Collection;
using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class Pong
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecPong);
    }

    /// <summary>
    /// Pong
    /// </summary>
    private void ExecPong()
    {
      int task = TaskGo.StartTask();
      var pong = new Library.Entity.Pong();
      Couleur scoreColor = new() { R = 127, G = 127, B = 127 };
      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        //Effacer la balle apres
        Pixels.Get(pong.X, pong.Y).SetColor();

        //Pointiller du milieux
        for (int i = 1; i < PixelList.Hauteur - 1; i += 2)
        {
          Pixels.Get(9, i).SetColor(16, 16, 32);
          Pixels.Get(10, i).SetColor(16, 16, 32);
        }

        //Mure des palettes, si y a un but pause 1.5 secondes
        if (pong.Palette(PixelList.Largeur))
          waitHandle.Wait(TimeSpan.FromMilliseconds(1500));

        //Mure du haut et du bas
        pong.Horizontal(PixelList.Hauteur);

        //Position de la balle
        pong.X += pong.XX;
        pong.Y += pong.YY;

        //Pointage
        Pixels.Set(CaractereList.Print(pong.ScoreP1.ToString("00"), 1, 2, scoreColor));
        Pixels.Set(CaractereList.Print(pong.ScoreP2.ToString("00"), 12, 2, scoreColor));

        //La balle
        Pixels.Get(pong.X, pong.Y).SetColor(16, 16, 127);

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
          if (!(Pixels.Get(1, p1Int + i).Couleur.Egal(scoreColor) && paddle.IsNoir))
            Pixels.Get(1, p1Int + i).SetColor(paddle);

          //Pour effacer la palette quand on la bouge, sans effacer le score
          if (!(Pixels.Get(18, p2Int + i).Couleur.Egal(scoreColor) && paddle.IsNoir))
            Pixels.Get(18, p2Int + i).SetColor(paddle);
        }

        //Bordure
        for (int i = 0; i < PixelList.Largeur; i++)
        {
          Pixels.Get(i, 0).SetColor(64, 64, 127);
          Pixels.Get(i, 19).SetColor(64, 64, 127);
        }

        //Background
        //Pixels.BackGround();
        Pixels.SendPixels();
        Pixels.Reset();

        if (pong.Vitesse > 0)
          waitHandle.Wait(TimeSpan.FromMilliseconds(pong.Vitesse));
      }
    }
  }
}