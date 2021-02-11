namespace LedLibrary.Entities
{
  public class Labyrinthe
  {
    public int X { get; set; }
    public int Y { get; set; }
    public Couleur Couleur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Labyrinthe(int x, int y)
    {
      X = x;
      Y = y;
      Couleur = new Couleur();
    }
  }
}