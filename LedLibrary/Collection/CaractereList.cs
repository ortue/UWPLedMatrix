using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedLibrary.Collection
{
  public class CaractereList : List<Caractere>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="text"></param>
    public CaractereList(string text)
    {
      foreach (char lettre in text)
        Add(new Caractere(lettre));

    }
  }
}