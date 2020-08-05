using LedLibrary.Classes;
using LedLibrary.Entities;
using System;

namespace LedLibrary.Entities
{
  public class Cercle
  {
    public int Probabilite { get; set; }
    public int Degree { get; set; }
    public int Rayon { get; set; }
    public int Inter { get; set; }
    public Couleur Couleur { get; set; }

    public int DegreeInter
    {
      get
      {
        switch (Inter)
        {
          case 1:
          case 5:
            return Degree;

          case 2:
            return 360 - Degree;

          case 3:
            return (180 + 360) % 360 - Degree;

          case 4:
            return (180 + Degree) % 360;
        }

        return Degree;
      }
    }

    public Coordonnee Centre
    {
      get
      {
        switch (Inter)
        {
          case 1:
            return new Coordonnee { X = 5, Y = 5 };

          case 2:
            return new Coordonnee { X = 14, Y = 5 };

          case 3:
            return new Coordonnee { X = 5, Y = 14 };

          case 4:
            return new Coordonnee { X = 14, Y = 14 };

          case 5:
            return new Coordonnee { X = 10, Y = 10 };
        }

        return new Coordonnee { X = 14, Y = 14 };
      }
    }

    public bool VariationProbabilite
    {
      get
      {
        Random random = new Random();

        return random.Next(0, Probabilite) == 0;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="degree"></param>
    public Cercle(int degree, int inter)
    {
      Inter = inter;
      Rayon = 5;
      Probabilite = 4;
      Degree = degree;
      Couleur = Couleur.Rnd();
    }

    /// <summary>
    /// Variation
    /// </summary>
    public void Variation()
    {
      if (Inter == 1 && VariationProbabilite && Degree == 90)
        Inter = 2;

      if (Inter == 1 && VariationProbabilite && Degree == 180)
        Inter = 3;

      if (Inter == 2 && VariationProbabilite && Degree == 90)
        Inter = 1;

      if (Inter == 2 && VariationProbabilite && Degree == 180)
        Inter = 4;

      if (Inter == 3 && VariationProbabilite && Degree == 180)
        Inter = 1;

      if (Inter == 3 && VariationProbabilite && Degree == 90)
        Inter = 4;

      if (Inter == 4 && VariationProbabilite && Degree == 180)
        Inter = 2;

      if (Inter == 4 && VariationProbabilite && Degree == 90)
        Inter = 3;
    }

    /// <summary>
    /// SetDegree
    /// </summary>
    /// <param name="degree"></param>
    public void SetDegree(int degree)
    {
      Degree += degree;

      if (Degree > 360)
        Degree = 0;
    }
  }
}