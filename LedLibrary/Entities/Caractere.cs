using LedLibrary.Classes;
using LedLibrary.Collection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedLibrary.Entities
{
  public class Caractere
  {
    public char Lettre { get; set; }
    public int Largeur { get; set; }
    public int Offset { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="lettre"></param>
    public Caractere(int offset, char lettre)
    {
      Lettre = lettre;
      Offset = offset;
    }

    /// <summary>
    /// Polices
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public PoliceList Polices(int position)
    {
      return PoliceList.GetPolice(position, Offset, Lettre);
    }
  }
}