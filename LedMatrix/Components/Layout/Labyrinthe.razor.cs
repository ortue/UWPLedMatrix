using Library.Collection;
using Library.Entity;
using Library.Util;

namespace LedMatrix.Components.Layout
{
  public partial class Labyrinthe
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecLabyrinthe);
    }

    /// <summary>
    /// Labyrinthe
    /// </summary>
    private void ExecLabyrinthe()
    {
      int task = TaskGo.StartTask();
      LabyrintheList labyrinthes = SetLabyrinthe();
      double cycle = 0;
      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        if (labyrinthes.Complet)
        {
          labyrinthes = SetLabyrinthe();
          waitHandle.Wait(TimeSpan.FromMilliseconds(500));
        }

        foreach (var labyrinthe in labyrinthes)
        {
          if (labyrinthe.Mur && !labyrinthe.Couleur.Egal(new Couleur()))
            labyrinthe.Couleur = Couleur.Get(127 - labyrinthe.X * 6, (labyrinthes.X + labyrinthes.Y) * 3, labyrinthe.Y * 6);

          Pixels.Get(labyrinthe.X, labyrinthe.Y).SetColor(labyrinthe.Couleur);
        }

        cycle = Background.Plasma(Pixels, cycle, true);
        Pixels.Get(labyrinthes.X, labyrinthes.Y).SetColor(Couleur.Get(127, 127, 127));

        if (cycle % 2 == 0)
          labyrinthes.Mouvement();

        Pixels.SendPixels();
        Pixels.Reset();

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
  }
}