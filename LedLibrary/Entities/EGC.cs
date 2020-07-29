using System;

namespace LedLibrary.Entities
{
  public class EGC
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Largeur { get; set; }
    public Couleur Couleur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public EGC(int x, int largeur, Couleur couleur)
    {
      X = x;
      Y = 10;
      Largeur = largeur;
      Couleur = couleur;
    }

    /// <summary>
    /// Next
    /// </summary>
    public void Next()
    {
      X = (X + 1) % Largeur;
    }
  }
}