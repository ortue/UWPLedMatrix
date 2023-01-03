using Library.Collection;
using Library.Entity;

namespace Library.Util
{
  public class Plasma
  {
    private const int Multi = 24;
    public double[,]? PlasmaArray { get; set; } //= new double[PixelList.Largeur * (Multi + 1), PixelList.Hauteur];
    public double HueShift { get; set; }
    public PixelList Pixels { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="pixels"></param>
    public Plasma(PixelList pixels)
    {
      Pixels = pixels;
    }

    /// <summary>
    /// SetPlasma
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="cycle"></param>
    /// <param name="reverse"></param>
    /// <returns></returns>
    public double SetPlasma(int alpha, double cycle = 0, bool reverse = false)
    {
      HueShift = (HueShift + 0.02) % 1;

      if (PlasmaArray == null)
      {
        PlasmaArray = new double[PixelList.Largeur * (Multi + 1), PixelList.Hauteur];

        for (int y = 0; y < PixelList.Hauteur; y++)
          for (int x = 0; x < PixelList.Largeur * (Multi + 1); x++)
          {
            double value = Math.Sin(x / 8.0);
            value += Math.Sin(y / 4.0);
            value += Math.Sin((x + y) / 8.0);
            value += Math.Sin(Math.Sqrt(x * x + y * y) / 4.0);
            value += 4; // shift range from -4 .. 4 to 0 .. 8
            value /= 8; // bring range down to 0 .. 1

            PlasmaArray[x, y] = value;
          }
      }

      cycle += 0.5;
      //cycle += 1;

      int offset = (int)(cycle % (PixelList.Largeur * Multi));

      if ((int)(cycle / (PixelList.Largeur * Multi)) % 2 == 1)
        offset = (int)(PixelList.Largeur * Multi - cycle % (PixelList.Largeur * Multi));

      for (int y = 0; y < PixelList.Hauteur; y++)
        for (int x = 0; x < PixelList.Largeur; x++)
          if (Pixels.Get(x, y) is Pixel pixel)
            if (pixel.Couleur.IsNoir != reverse)
            {
              double hue = HueShift % 1 + PlasmaArray[x + offset, y] % 1;
              Couleur couleur = HSVtoRGB(hue, 1, 1, alpha);
              pixel.SetColor(couleur);
            }

      return cycle;
    }

    /// <summary>
    /// HSVtoRGB
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    private static Couleur HSVtoRGB(double h, int s, int v, int alpha)
    {
      int i = (int)Math.Floor(h * 6);
      double f = h * 6 - i;
      int p = v * (1 - s);
      double q = v * (1 - f * s);
      double t = v * (1 - (1 - f) * s);

      return (i % 6) switch
      {
        0 => Couleur.Get(v * alpha, (int)Math.Round(t * alpha), p * alpha),
        1 => Couleur.Get((int)Math.Round(q * alpha), v * alpha, p * alpha),
        2 => Couleur.Get(p * alpha, v * alpha, (int)Math.Round(t * alpha)),
        3 => Couleur.Get(p * alpha, (int)Math.Round(q * alpha), v * alpha),
        4 => Couleur.Get((int)Math.Round(t * alpha), p * alpha, v * alpha),
        _ => Couleur.Get(v * alpha, p * alpha, (int)Math.Round(q * alpha))
      };
    }
  }
}
