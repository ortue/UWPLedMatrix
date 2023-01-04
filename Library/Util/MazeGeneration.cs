using System.Drawing;

namespace Library.Util
{
  public static class Extensions
  {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
    {
      T[] e = source.ToArray();

      for (var i = e.Length - 1; i >= 0; i--)
      {
        int swapIndex = rng.Next(i + 1);
        yield return e[swapIndex];
        e[swapIndex] = e[i];
      }
    }

    public static CellState OppositeWall(this CellState orig)
    {
      return (CellState)(((int)orig >> 2) | ((int)orig << 2)) & CellState.Initial;
    }

    public static bool HasFlag(this CellState cs, CellState flag)
    {
      return ((int)cs & (int)flag) != 0;
    }
  }

  [Flags]
  public enum CellState
  {
    Top = 1,
    Right = 2,
    Bottom = 4,
    Left = 8,
    Visited = 128,
    Initial = Top | Right | Bottom | Left,
  }

  public struct RemoveWallAction
  {
    public Point Neighbour;
    public CellState Wall;
  }

  public class Maze
  {
    private readonly CellState[,] _cells;
    public readonly int Width;
    public readonly int Height;
    private readonly Random _rng;

    public Maze(int width, int height)
    {
      Width = width;
      Height = height;
      _cells = new CellState[width, height];

      for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
          _cells[x, y] = CellState.Initial;

      _rng = new Random();
      VisitCell(_rng.Next(width), _rng.Next(height));
    }

    public CellState this[int x, int y]
    {
      get { return _cells[x, y]; }
      set { _cells[x, y] = value; }
    }

    public IEnumerable<RemoveWallAction> GetNeighbours(Point p)
    {
      if (p.X > 0)
        yield return new RemoveWallAction
        {
          Neighbour = new Point(p.X - 1, p.Y),
          Wall = CellState.Left
        };

      if (p.Y > 0)
        yield return new RemoveWallAction
        {
          Neighbour = new Point(p.X, p.Y - 1),
          Wall = CellState.Top
        };

      if (p.X < Width - 1)
        yield return new RemoveWallAction
        {
          Neighbour = new Point(p.X + 1, p.Y),
          Wall = CellState.Right
        };

      if (p.Y < Height - 1)
        yield return new RemoveWallAction
        {
          Neighbour = new Point(p.X, p.Y + 1),
          Wall = CellState.Bottom
        };
    }

    public void VisitCell(int x, int y)
    {
      this[x, y] |= CellState.Visited;

      foreach (RemoveWallAction p in GetNeighbours(new Point(x, y)).Shuffle(_rng).Where(z => !(this[z.Neighbour.X, z.Neighbour.Y].HasFlag(CellState.Visited))))
      {
        this[x, y] -= p.Wall;
        this[p.Neighbour.X, p.Neighbour.Y] -= p.Wall.OppositeWall();
        VisitCell(p.Neighbour.X, p.Neighbour.Y);
      }
    }
  }
}