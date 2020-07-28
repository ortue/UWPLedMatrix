using LedLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace LedLibrary.Collection
{
  public class CercleList : List<Cercle>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="nombre"></param>
    public CercleList(int nombre)
    {
      for (int i = 0; i < nombre; i++)
        Add(new Cercle(i * 90));
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
    /// <param name="v"></param>
    public void SetDegree(int degree)
    {
      foreach (Cercle cercle in this)
        cercle.SetDegree(degree);
    }
  }
}