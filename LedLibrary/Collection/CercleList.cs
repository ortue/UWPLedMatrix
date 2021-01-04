using LedLibrary.Classes;
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
    public CercleList(int nombre, int inter, int degre)
    {
      for (int i = 0; i < nombre; i++)
        Add(new Cercle(i * degre, inter));
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
  }
}