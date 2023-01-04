using Library.Collection;
using Library.Entity;

namespace Library.Util
{
  public class Background
  {
    /// <summary>
    /// Bleu
    /// </summary>
    /// <param name="pixels"></param>
    /// <param name="reverse"></param>
    public static void Bleu(PixelList pixels, bool reverse = false)
    {
      List<int> b = new()
      {
        2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2,
        2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2
      };

      int sec = DateTime.Now.Millisecond / 50;
      int multi = 1;

      if (reverse)
        multi = 3;

      foreach (Pixel pixel in pixels.Where(p => p.Couleur.IsNoir != reverse))
        pixel.SetColor(Couleur.Get(0, 0, b[sec + pixel.Y] * multi));
    }

    /// <summary>
    /// Plasma
    /// </summary>
    /// <param name="pixels"></param>
    /// <param name="cycle"></param>
    /// <param name="reverse"></param>
    /// <returns></returns>
    public static double Plasma(PixelList pixels, double cycle = 0, bool reverse = false)
    {
      int alpha = 32;

      if (reverse)
        alpha = 127;

      Plasma plasma = new(pixels);
      return plasma.SetPlasma(alpha, cycle, reverse);
    }

    /// <summary>
    /// Grichage
    /// </summary>
    /// <param name="pixels"></param>
    /// <param name="reverse"></param>
    public static void Grichage(PixelList pixels, bool reverse = false)
    {
      int max = 20;

      if (reverse)
        max = 127;

      Random r = new();

      foreach (Pixel pixel in pixels.Where(p => p.Couleur.IsNoir != reverse))
        pixel.SetColor(new Couleur { B = (byte)r.Next(5, max) });
    }
  }
}