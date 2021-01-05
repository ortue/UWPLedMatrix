using LedLibrary.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class CaractereList : List<Caractere>
  {
    public int Largeur { get; set; }
    public int LargeurTotal { get; set; }
    public string TextTmp { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    public CaractereList(int largeur)
    {
      Largeur = largeur;
    }

    /// <summary>
    /// SetText
    /// </summary>
    /// <param name="nouvelleStr"></param>
    public int SetText(string nouvelleStr)
    {
      if (TextTmp != nouvelleStr)
      {
        TextTmp = nouvelleStr;

        Clear();

        foreach (char lettre in TextTmp)
          Add(new Caractere(lettre));

        LargeurTotal = this.Sum(p => p.Largeur);
      }

      return LargeurTotal;
    }

    /// <summary>
    /// Caracteres
    /// </summary>
    /// <param name="debut"></param>
    /// <param name="fin"></param>
    /// <returns></returns>
    public PoliceList GetCaracteres(int debut = 0)
    {
      int i = 0;
      int position = 0;
      PoliceList polices = new PoliceList();

      //Définir le offset de la fraction de lettre qui disparait du coté gauche
      while (position < debut && Count > i)
        if (this[i++].Polices(0, position) is PoliceList lettre)
        {
          position += lettre.Largeur;
          polices.Offset = lettre.Largeur - (position - debut);
        }

      position = 0;

      if (polices.Offset != 0)
        i--;

      //Espace du coté gauche au début du défilement
      while (debut++ < 0)
        polices.AddRange(PoliceList.GetPolice(0, position++, ' '));

      while (position <= Largeur && Count > i)
        if (this[i++].Polices(polices.Offset, position) is PoliceList lettre)
        {
          polices.AddRange(lettre);
          position += lettre.Largeur;
        }

      //Fraction de la derniere lettre du coté droit
      if (Count > i && this[i++].Polices(polices.Offset, position) is PoliceList derniereLettre)
        polices.AddRange(derniereLettre);

      return polices;
    }
  }
}