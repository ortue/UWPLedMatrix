using Library.Entity;

namespace Library.Collection
{
  public class TetrisHorizontalList : List<TetrisHorizontal>
  {
    public double MaxScore
    {
      get { return this.Max(h => h.Score); }
    }

    public int ScoreX
    {
      get
      {
        if (this.FirstOrDefault(h => h.Score == MaxScore) is TetrisHorizontal horizontal)
          return horizontal.X;

        return 0;
      }
    }

    public int ScoreRotation
    {
      get
      {
        if (this.FirstOrDefault(h => h.Score == MaxScore) is TetrisHorizontal horizontal)
          return horizontal.Rotation;

        return 0;
      }
    }
  }
}
