using Library.Util;
using Nfw.Linux.Joystick.Xpad;

namespace Library.Entity
{
  public class Sinus
  {
    public bool Manette { get; set; }
    public Couleur Couleur { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double XX { get; set; }
    public double Hauteur { get; set; }

    public Pixel Coord
    {
      get
      {
        return new Pixel
        {
          X = (int)X % 20,
          Y = (int)Y
        };
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Sinus()
    {
      Random random = new();
      XX = random.Next(100, 1000) / 100d;
      Hauteur = random.Next(70, 90) / 10d;
      Couleur = Couleur.Rnd;
      X = 0;
      Y = 10;
    }

    /// <summary>
    /// SinusManette
    /// </summary>
    /// <param name="manette"></param>
    public void SinusManette(Manette manette)
    {
      XX = manette.X;
      Hauteur = manette.Y;
    }

    /// <summary>
    /// Next
    /// </summary>
    public void Next()
    {
      X += XX;
      //X+=1;
      Y = 10d + Math.Sin(X / Math.PI) * Hauteur;
    }
  }
}
