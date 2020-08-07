using LedLibrary.Collection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedLibrary.Entities
{
  public class Caractere
  {
    public char Lettre { get; set; }

    //public CoordonneeList Coordonnees
    //{
    //  get 
    //  {
    //  return 
    //  }
    //}

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="lettre"></param>
    public Caractere(char lettre)
    {
      Lettre = lettre;
    }
  }
}
