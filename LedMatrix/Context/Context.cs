using LedMatrix.Classes;
using Library.Classes;
using Library.Collection;

namespace LedMatrix.Context
{
  public class LedMatrixContext
  {
    public PixelList Pixels { get; set; }
    public DotStarStrip PixelStrip { get; set; }
    public AnimationList Animations { get; set; }

    /// <summary>
    /// Nombre de Led de Largeur par defaut
    /// </summary>
    public int Largeur
    {
      get { return 20; }
    }

    /// <summary>
    /// Nombre de Led de Longeur par defaut
    /// </summary>
    public int Hauteur
    {
      get { return 20; }
    }

    /// <summary>
    /// Nombre de Led par defaut
    /// </summary>
    public int NbrLed
    {
      get { return Largeur * Hauteur; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public LedMatrixContext()
    {
      PixelStrip = new DotStarStrip(NbrLed);
      Pixels = new PixelList(Largeur, Hauteur);
      Animations = new AnimationList("Images");
    }
  }
}