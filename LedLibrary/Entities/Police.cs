namespace LedLibrary.Entities
{
  public class Police
  {
    public int X { get; set; }
    public int Y { get; set; }
    public bool Point { get; set; }

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