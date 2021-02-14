using LedLibrary.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class TetrisHorizontalList : List<TetrisHorizontal>
  {
    public double MaxScore
    {
      get { return this.Max(h => h.Score); }
    }

    public int ScoreX
    {
      get { return this.FirstOrDefault(h => h.Score == MaxScore).X; }
    }

    public int ScoreRotation
    {
      get { return this.FirstOrDefault(h => h.Score == MaxScore).Rotation; }
    }
  }
}