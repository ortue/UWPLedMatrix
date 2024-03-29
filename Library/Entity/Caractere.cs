﻿using Library.Collection;

namespace Library.Entity
{
  public class Caractere
  {
    public char Lettre { get; set; }

    public int Largeur
    {
      get { return PoliceList.GetLargeur(Lettre); }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="lettre"></param>
    public Caractere(char lettre)
    {
      Lettre = lettre;
    }

    /// <summary>
    /// Polices
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public PoliceList Polices(int offset, int position)
    {
      return PoliceList.GetPolice(offset, position, Lettre);
    }
  }
}