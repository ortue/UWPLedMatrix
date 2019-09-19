using Windows.UI;

namespace Library.Entities
{
  public class Couleur
  {
    public int FrameCompteur { get; set; }
    public int Position { get; set; }
    public Color Color { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    public Couleur(int position, Color color)
    {
      Position = position;
      Color = color;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="frameCompteur"></param>
    /// <param name="color"></param>
    public Couleur(int frameCompteur, int position, Color color)
    {
      FrameCompteur = frameCompteur;
      Position = position;
      Color = color;
    }
  }
}