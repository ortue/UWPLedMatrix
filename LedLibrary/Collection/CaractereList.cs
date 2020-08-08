﻿using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class CaractereList : List<Caractere>
  {
    public PoliceList Caracteres
    {
      get
      {
        int position = 0;
        PoliceList polices = new PoliceList();

        foreach (Caractere caractere in this)
          if (caractere.Polices(position) is PoliceList lettre)
          {
            polices.AddRange(lettre);
            position += lettre.Largeur;
          }

        return polices;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="text"></param>
    public CaractereList(string text, int debut, int fin)
    {
      int i = 0;
      int distance = 0;

      while (distance < fin && text.Length > i)
      {
        char lettre = text[i++];
        int largeur = PoliceList.GetLargeur(lettre);

        int offset = 0;

        if (distance <= debut && debut < distance + largeur)
        {

        }

        Add(new Caractere(offset, lettre));

        distance += largeur;
      }
    }
  }
}