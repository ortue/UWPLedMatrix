using LedLibrary.Entities;
using System.Collections.Generic;

namespace LedLibrary.Collection
{
  public class EGCList : List<EGC>
  {
    public int Largeur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public EGCList(int nombre, int largeur)
    {
      Largeur = largeur;

      for (int i = nombre; i > 0; i--)
      {
        Couleur couleur = Couleur.Get(2, 4, 2);

        if (i == nombre)
          couleur = Couleur.Get(95, 127, 95);

        Add(new EGC(i, Largeur, couleur));
      }
    }

    /// <summary>
    /// Next
    /// </summary>
    public void Next()
    {
      foreach (EGC egc in this)
        egc.Next();
    }
  }
}