using Library.Collection;
using Library.Entity;
using Library.Util;

namespace BlazorAppMatrix.Components.Layout
{
  public partial class Arkanoid
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecArkanoid);
    }

    /// <summary>
    /// Arkanoid
    /// </summary>
    private void ExecArkanoid()
    {
      int task = TaskGo.StartTask();
      Library.Entity.Arkanoid arkanoid = new();
      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        //Effacer la balle apres
        Pixels.Get(arkanoid.X, arkanoid.Y).SetColor();

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
        Pixels.Get(arkanoid.X, arkanoid.Y).SetColor(16, 16, 127);

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
          Pixels.Get(p1Int + i, 18).SetColor(paddle);
        }

        //Bordure
        for (int x = 0; x < PixelList.Largeur; x++)
          Pixels.Get(x, 0).SetColor(31, 31, 127);

        for (int y = 0; y < PixelList.Hauteur; y++)
        {
          Pixels.Get(0, y).SetColor(31, 31, 127);
          Pixels.Get(19, y).SetColor(31, 31, 127);
        }

        foreach (Brique brique in arkanoid.Briques.Where(b => b.Visible))
        {
          Pixels.Get(brique.X, brique.Y).SetColor(brique.Couleur);
          Pixels.Get(brique.XX, brique.Y).SetColor(brique.Couleur);
        }

        Background.Bleu(Pixels);
        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(arkanoid.Vitesse / 2));
      }
    }
  }
}