using System;

namespace LedLibrary.Entities
{
  public class Sinus
  {
    public Couleur Couleur { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double XX { get; set; }
    public double Hauteur { get; set; }

    public Coordonnee Coord
    {
      get
      {
        return new Coordonnee
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
      Random random = new Random();
      XX = random.Next(400, 700) / 100d;
      Hauteur = random.Next(70, 100) / 10d;
      Couleur = Couleur.Rnd();
      X = 0;
      Y = 10;
    }

    /// <summary>
    /// Next
    /// </summary>
    public void Next()
    {
      X += XX;
      Y = 10d + Math.Sin(X / Math.PI) * Hauteur;
    }
  }
}