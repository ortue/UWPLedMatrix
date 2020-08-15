namespace LedLibrary.Entities
{
  public class Police
  {
    public int X { get; set; }
    public int Y { get; set; }
    public bool Point { get; set; }

    /// <summary>
    /// Espace
    /// </summary>
    /// <param name="x"></param>
    //public Police(int x)
    //{
    //  X = x;
    //  Point = false;
    //}

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    /// <param name="i"></param>
    /// <param name="point"></param>
    public Police(int largeur, int i, int position, bool point)
    {
      X = i % largeur + position;
      Y = i / largeur;
      Point = point;
    }
  }
}