using Library.Entity;

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

    /// <summary>
    /// Contructeur
    /// </summary>
    public PixelList()
    {
      List<int> emplacement = Emplacement();

      for (int position = 0; position < Largeur * Hauteur; position++)
        Add(new Pixel(position, emplacement[position]));
    }

    /// <summary>
    /// Emplacement
    /// </summary>
    /// <returns></returns>
    public List<int> Emplacement()
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
  }
}