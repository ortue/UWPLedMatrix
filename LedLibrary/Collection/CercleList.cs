using LedLibrary.Entities;
using System.Collections.Generic;

namespace LedLibrary.Collection
{
  public class CercleList : List<Cercle>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="inter"></param>
    /// <param name="degre"></param>
    public CercleList(int nombre, int inter, int degre)
    {
      for (int i = 0; i < nombre; i++)
        Add(new Cercle(i * degre, inter));
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="rayon"></param>
    public CercleList(int nombre, int rayon)
    {
      for (int i = 0; i < nombre; i++)
        Add(new Cercle(i * rayon));
    }

    /// <summary>
    /// Variation
    /// </summary>
    public void Variation()
    {
      foreach (Cercle cercle in this)
        cercle.Variation();
    }

    /// <summary>
    /// SetDegree
    /// </summary>
    /// <param name="degree"></param>
    public void SetDegree(int degree)
    {
      foreach (Cercle cercle in this)
        cercle.SetDegree(degree);
    }

    /// <summary>
    /// SetRayon
    /// </summary>
    /// <param name="v"></param>
    public void SetRayon(double rayon, bool random)
    {
      foreach (Cercle cercle in this)
        cercle.SetRayon(rayon, random);
    }
  }
}