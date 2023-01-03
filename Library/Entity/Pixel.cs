using Library.Collection;

namespace Library.Entity
{
  public class Pixel
  {
    public int X { get; set; }
    public int Y { get; set; }
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public int Numero { get; set; }
    public int Position { get; set; }

    public Couleur Couleur
    {
      get { return Couleur.FromArgb(R, G, B); }
      set
      {
        R = value.R;
        G = value.G;
        B = value.B;
      }
    }

    //public Couleur CouleurDouble
    //{
    //  get { return Couleur.FromArgb((byte)(R * 2), (byte)(G * 2), (byte)(B * 2)); }
    //  set
    //  {
    //    R = value.R;
    //    G = value.G;
    //    B = value.B;
    //  }
    //}

    /// <summary>
    /// Constructeur
    /// </summary>
    public Pixel()
    {

    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="position">Position qu'on affiche une image a partir du coin haut droit vers le coin bas gauche</param>
    /// <param name="numero">Ordre qu'on envoit les pixels au rubans, du coin bas gauche en serpent vers le haut</param>
    public Pixel(int position, int numero)
    {
      Position = position + 1;
      Numero = numero;

      X = position % PixelList.Largeur;
      Y = Convert.ToInt32(Math.Floor((decimal)(position / PixelList.Hauteur)));
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="pixel"></param>
    public Pixel(Pixel pixel)
    {
      Mapping(pixel);
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="position"></param>
    /// <param name="numero"></param>
    /// <param name="couleur"></param>
    public Pixel(int x, int y, Couleur couleur)
    {
      Couleur = couleur;
      X = x;
      Y = y;
    }

    /// <summary>
    /// Mapping
    /// </summary>
    /// <param name="pixel"></param>
    private void Mapping(Pixel pixel)
    {
      Couleur = pixel.Couleur;
      X = pixel.X;
      Y = pixel.Y;
      Numero = pixel.Numero;
      Position = pixel.Position;
    }

    /// <summary>
    /// SetColor
    /// </summary>
    public void SetColor()
    {
      Couleur = Couleur.Noir;
    }

    /// <summary>
    /// SetColor
    /// </summary>
    /// <param name="couleur"></param>
    public void SetColor(Couleur couleur)
    {
      Couleur = couleur;
    }

    /// <summary>
    /// SetColor
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    public void SetColor(int r, int g, int b)
    {
      Couleur = Couleur.Get(r, g, b);
    }

    /// <summary>
    /// CheckCoord
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
    public Pixel CheckCoord()
    {
      if (X < 0)
        X = 0;

      if (Y < 0)
        Y = 0;

      if (X > PixelList.Largeur - 1)
        X = PixelList.Largeur - 1;

      if (Y > PixelList.Hauteur - 1)
        Y = PixelList.Hauteur - 1;

      return this;
    }

    /// <summary>
    /// Cercle
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="rayon"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Pixel? Cercle(int degree, double rayon, int x = 10, int y = 10)
    {
      Pixel coord = new();

      if (degree >= 0 && degree <= 180)
        coord.X = x + (int)(rayon * Math.Sin(Math.PI * degree / 180));
      else
        coord.X = x - (int)(rayon * -Math.Sin(Math.PI * degree / 180)) - 1;

      coord.Y = y - (int)(rayon * Math.Cos(Math.PI * degree / 180) + 0.5);

      if (coord.X < 0)
        return null;

      if (coord.Y < 0)
        return null;

      if (coord.X > PixelList.Largeur - 1)
        return null;

      if (coord.Y > PixelList.Hauteur - 1)
        return null;

      return coord;
    }

    /// <summary>
    /// Fade
    /// </summary>
    /// <param name="fade"></param>
    public void Fade(int fade)
    {
      Couleur = Couleur.Get((byte)(Couleur.R / (byte)fade), (byte)(Couleur.G / (byte)fade), (byte)(Couleur.B / (byte)fade));
    }
  }
}