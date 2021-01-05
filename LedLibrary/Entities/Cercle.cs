using System;

namespace LedLibrary.Entities
{
  public class Cercle
  {
    public int Probabilite { get; set; }
    public int Degree { get; set; }
    public double Rayon { get; set; }
    public int Inter { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
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
        return Inter switch
        {
          1 => new Coordonnee { X = 5, Y = 5 },
          2 => new Coordonnee { X = 14, Y = 5 },
          3 => new Coordonnee { X = 5, Y = 14 },
          4 => new Coordonnee { X = 14, Y = 14 },
          5 => new Coordonnee { X = 10, Y = 10 },
          _ => new Coordonnee { X = 14, Y = 14 },
        };
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
    /// <param name="inter"></param>
    public Cercle(int degree, int inter)
    {
      Inter = inter;
      Rayon = 5;
      Probabilite = 4;
      Degree = degree;
      Couleur = Couleur.Rnd();
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="rayon"></param>
    public Cercle(int rayon)
    {
      Rayon = rayon;
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

    /// <summary>
    /// SetRayon
    /// </summary>
    /// <param name="rayon"></param>
    public void SetRayon(double rayon, bool random)
    {
      Rayon += rayon;

      if (Rayon > 20)
      {
        Rayon = 1;
        Couleur = Couleur.Rnd();

        if (random)
        {
          Random r = new Random();
          X = r.Next(4, 16);
          Y = r.Next(4, 16);
        }
        else
        {
          X = 10;
          Y = 10;
        }
      }
    }
  }
}