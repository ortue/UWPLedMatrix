using Library.Entity;
using Library.Util;

namespace Library.Collection
{
  public class PixelList : List<Pixel>
  {
    public const int Largeur = 20;
    public const int Hauteur = 20;

    public IEnumerable<Pixel> PixelColors
    {
      get { return this.OrderBy(p => p.Numero); }
    }

    public DotStarStrip DotStarStrip { get; set; }

    /// <summary>
    /// Contructeur
    /// </summary>
    public PixelList()
    {
      DotStarStrip = new(Largeur * Hauteur, Environment.MachineName != "PC-BENOIT");

      List<int> emplacement = Emplacement();

      for (int position = 0; position < Largeur * Hauteur; position++)
        Add(new Pixel(position, emplacement[position]));
    }

    /// <summary>
    /// Emplacement
    /// </summary>
    /// <returns></returns>
    public static List<int> Emplacement()
    {
      List<int> emplacement = new();

      for (int j = 0; j < Hauteur; j++)
        for (int i = 1; i <= Largeur; i++)
        {
          if (i % 2 == 0)
            emplacement.Add(Largeur * (i - 1) + 1 + j);
          else
            emplacement.Add(Largeur * i - j);
        }

      return emplacement;
    }

    /// <summary>
    /// Reset
    /// </summary>
    public void Reset()
    {
      foreach (Pixel pixel in this)
        pixel.Couleur = new Couleur();
    }

    /// <summary>
    /// GetCoordonnee
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Pixel Get(int x, int y)
    {
      return Find(p => p.X == x && p.Y == y)!;
    }

    /// <summary>
    /// SendPixels
    /// </summary>
    public void SendPixels()
    {
      DotStarStrip.SendPixels(this);
    }
  }
}