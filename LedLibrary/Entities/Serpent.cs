namespace LedLibrary.Entities
{
  public class Serpent
  {
    public int X { get; set; }
    public int Y { get; set; }
    public bool Tete { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    //public Serpent()
    //{

    //}

    public Serpent(Serpent serpent)
    {
      if (serpent != null)
      {
        X = serpent.X;
        Y = serpent.Y;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Serpent(int x, int y)
    {
      X = x;
      Y = y;
    }

    /// <summary>
    /// SetSerpent
    /// </summary>
    /// <param name="tmpSerpent"></param>
    public void SetSerpent(Serpent tmpSerpent)
    {
      if (tmpSerpent != null)
      {
        X = tmpSerpent.X;
        Y = tmpSerpent.Y;
      }
    }
  }
}