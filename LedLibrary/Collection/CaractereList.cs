using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class CaractereList : List<Caractere>
  {
    public int Largeur { get; set; }
    public string Text { get; set; }
    public string TextTmp { get; set; }

    //public PoliceList Caracteres
    //{
    //  get
    //  {
    //    int position = 0;
    //    PoliceList polices = new PoliceList();

    //    foreach (Caractere caractere in this)
    //      if (caractere.Polices(position) is PoliceList lettre)
    //      {
    //        polices.AddRange(lettre);
    //        position += lettre.Largeur;
    //      }

    //    return polices;
    //  }
    //}

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    public CaractereList(int largeur)
    {
      Largeur = largeur;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="text"></param>
    //public CaractereList(string text, int debut, int fin, bool a)
    //{
    //  int i = 0;
    //  int distance = 0;

    //  while (distance < fin && text.Length > i)
    //  {
    //    int offset = 0;
    //    char lettre = ' ';

    //    if (debut + i > -1 && text.Length > debut + i)
    //      lettre = text[debut + i];

    //    int largeur = PoliceList.GetLargeur(lettre);

    //    if (distance == 0)
    //      offset = debut % largeur;

    //    Add(new Caractere(offset, lettre));

    //    distance += largeur;
    //    i++;
    //  }
    //}

    public void SetText(string nouvelleStr)
    {
      if (TextTmp != nouvelleStr)
      {
        TextTmp = nouvelleStr;

        foreach (char lettre in TextTmp)
          Add(new Caractere(lettre));
      }
    }

    /// <summary>
    /// Caracteres
    /// </summary>
    /// <param name="debut"></param>
    /// <param name="fin"></param>
    /// <returns></returns>
    public PoliceList GetCaracteres(int debut)
    {
      int i = 0;
      int offset = 0;
      int position = 0;
      PoliceList polices = new PoliceList();

      while (position < debut && Count > i)
        if (this[i++].Polices(0, position) is PoliceList lettre)
        {
          position += lettre.Largeur;
          offset = lettre.Largeur - (position - debut);
        }

      position = 0;

      while (debut++ < 0)
        polices.AddRange(PoliceList.GetPolice(0, position++, ' '));

      while (position < Largeur && Count > i)
        if (this[i++].Polices(offset, position) is PoliceList lettre)
        {
          polices.AddRange(lettre);
          position += lettre.Largeur;
        }

      return polices;
    }
  }
}