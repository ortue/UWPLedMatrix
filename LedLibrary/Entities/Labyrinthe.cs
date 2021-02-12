namespace LedLibrary.Entities
{
  public class Labyrinthe
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int DX { get; set; }
    public int DY { get; set; }
    public bool Mur { get; set; }
    public bool Chemin { get; set; }
    public Couleur Couleur { get; set; }
    public bool Intersection { get; set; }
    public int CheminParcouru { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Labyrinthe(int x, int y, bool mur)
    {
      X = x;
      Y = y;
      Couleur = new Couleur();
      Mur = mur;
      Chemin = !mur;
    }
  }
}