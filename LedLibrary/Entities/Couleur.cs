
namespace LedLibrary.Entities
{
  public class Couleur
  {
    public int FrameCompteur { get; set; }
    public int Position { get; set; }
    public byte A { get; set; }
    public byte B { get; set; }
    public byte G { get; set; }
    public byte R { get; set; }

    public bool IsNoir
    {
      get { return R == 0 && G == 0 && B == 0; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Couleur()
    {

    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    public Couleur(int position, Couleur couleur)
    {
      Position = position;

      A = couleur.A;
      R = couleur.R;
      G = couleur.G;
      B = couleur.B;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="frameCompteur"></param>
    /// <param name="color"></param>
    public Couleur(int frameCompteur, int position, Couleur couleur)
    {
      FrameCompteur = frameCompteur;
      Position = position;

      A = couleur.A;
      R = couleur.R;
      G = couleur.G;
      B = couleur.B;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Couleur Get(int r, int g, int b)
    {
      return new Couleur { R = (byte)r, G = (byte)g, B = (byte)b };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Red"></param>
    /// <param name="Green"></param>
    /// <param name="Blue"></param>
    /// <returns></returns>
    public static Couleur FromArgb(byte Red, byte Green, byte Blue)
    {
      return new Couleur { R = Red, G = Green, B = Blue };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public bool Egal(Couleur couleur)
    {
      return R == couleur.R && G == couleur.G && B == couleur.B;
    }
  }
}