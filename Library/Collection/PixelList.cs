using Library.Entity;
using Library.Util;

namespace Library.Collection
{
  public class PixelList : List<Pixel>
  {
    public const int Largeur = 20;
    public const int Hauteur = 20;
    public DotStarStrip DotStarStrip { get; set; }
    public IEnumerable<Pixel>? PixelDebug { get; set; }

    public IEnumerable<Pixel> PixelColors
    {
      get { return this.OrderBy(p => p.Numero); }
    }

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
    /// Contructeur
    /// </summary>
    /// <param name="inter"></param>
    public PixelList(bool inter)
    {
      DotStarStrip = new(Largeur * Hauteur, Environment.MachineName != "PC-BENOIT");
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
    /// Remet la couleur noir a tous les pixels de la liste
    /// </summary>
    public void Reset()
    {
      foreach (Pixel pixel in this)
        pixel.Couleur = new Couleur();
    }

    /// <summary>
    /// Get pixel using Coordonnee
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Pixel Get(int x, int y)
    {
      return Find(p => p.X == x && p.Y == y)!;
    }

    /// <summary>
    /// Get pixel using Coordonnee
    /// </summary>
    /// <param name="coordonnee"></param>
    /// <returns></returns>
    public Pixel Get(Pixel coordonnee)
    {
      return Find(p => p.X == coordonnee.X && p.Y == coordonnee.Y)!;
    }

    /// <summary>
    /// Get pixel using Coordonnee
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Pixel Get(double x, double y)
    {
      int xx = (int)Math.Round(x, 0);

      if (xx >= Largeur - 1)
        xx = Largeur - 1;

      if (xx < 0)
        xx = 0;

      int yy = (int)Math.Round(y, 0);

      if (yy >= Hauteur - 1)
        yy = Hauteur - 1;

      if (yy < 0)
        yy = 0;

      return Find(p => p.X == xx && p.Y == yy)!;
    }

    /// <summary>
    /// Envois la liste des pixels aux leds
    /// </summary>
    public void SendPixels()
    {
      DotStarStrip.SendPixels(PixelColors);
      SendPixelsDebug(Environment.MachineName == "PC-BENOIT");
    }

    /// <summary>
    /// SendPixelsDebug
    /// </summary>
    /// <param name="debug"></param>
    private void SendPixelsDebug(bool debug)
    {
      if (!debug)
        return;

      List<Pixel> pixels = new List<Pixel>();

      foreach (Pixel pixel in this)
        pixels.Add(new Pixel(pixel));

      PixelDebug = pixels;
    }

    /// <summary>
    /// GetCerlcleCoord
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    public static Pixel GetCercleCoord(Pixel centre, int degree, double rayon)
    {
      Pixel p = new();

      if (degree >= 0 && degree <= 180)
        p.X = centre.X + (int)(rayon * Math.Sin(Math.PI * degree / 180));
      else
        p.X = centre.X - (int)(rayon * -Math.Sin(Math.PI * degree / 180)) - 1;

      p.Y = centre.Y - (int)(rayon * Math.Cos(Math.PI * degree / 180) + 0.5);

      return p.CheckCoord();
    }

    /// <summary>
    /// GetTempCoord
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    public static Pixel GetTempsCoord(double temp, int rayon)
    {
      Pixel p = new();

      temp *= 6;   //each minute and second make 6 degree

      if (temp >= 0 && temp <= 180)
        p.X = (Largeur / 2) + (int)(rayon * Math.Sin(Math.PI * temp / 180));
      else
        p.X = (Largeur / 2) - (int)(rayon * -Math.Sin(Math.PI * temp / 180)) - 1;

      p.Y = (Hauteur / 2) - (int)(rayon * Math.Cos(Math.PI * temp / 180) + 0.5);

      return p.CheckCoord();
    }

    /// <summary>
    /// GetHeureCoord
    /// </summary>
    /// <param name="heure"></param>
    /// <param name="minute"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    public static Pixel GetHeureCoord(int heure, int minute, int rayon)
    {
      Pixel p = new();

      //each hour makes 30 degree
      //each min makes 0.5 degree
      double val = (heure * 30) + (minute * 0.5);

      if (val >= 0 && val <= 180)
        p.X = (Largeur / 2) + (int)(rayon * Math.Sin(Math.PI * val / 180));
      else
        p.X = (Largeur / 2) - (int)(rayon * -Math.Sin(Math.PI * val / 180)) - 1;

      p.Y = (Hauteur / 2) - (int)(rayon * Math.Cos(Math.PI * val / 180) + 0.5);

      return p.CheckCoord();
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="pixelList"></param>
    public void Set(PixelList pixels)
    {
      foreach (Pixel pixel in pixels)
        Get(pixel).SetColor(pixel.Couleur);
    }
  }
}