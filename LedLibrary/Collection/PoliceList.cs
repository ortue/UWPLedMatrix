using LedLibrary.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace LedLibrary.Collection
{
  public class PoliceList : List<Police>
  {
    public int Largeur { get; set; }
    public int Position { get; set; }
    public static int Hauteur { get { return 5; } }

    public static List<bool> Espace
    {
      get
      {
        return new List<bool>
        {
          false,
          false,
          false,
          false,
          false
        };
      }
    }

    public static List<bool> A
    {
      get
      {
        return new List<bool>
        {
          false, true, false, false,
          true, false, true, false,
          true, true, true, false,
          true, false, true, false,
          true, false, true, false
        };
      }
    }

    public static List<bool> B
    {
      get
      {
        return new List<bool>
        {
          true, true, false, false,
          true, false, true, false,
          true, true, false, false,
          true, false, true, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> C
    {
      get
      {
        return new List<bool>
        {
          false, true, false, false,
          true, false, true, false,
          true, false, false, false,
          true, false, true, false,
          false, true, false, false
        };
      }
    }

    public static List<bool> D
    {
      get
      {
        return new List<bool>
        {
          true, true, false, false,
          true, false, true, false,
          true, false, true, false,
          true, false, true, false,
          true, true, false, false
        };
      }
    }

    public static List<bool> E
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, false, false,
          true, true, false, false,
          true, false, false, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> F
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, false, false,
          true, true, false, false,
          true, false, false, false,
          true, false, false, false
        };
      }
    }

    public static List<bool> G
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, false, false,
          true, true, true, false,
          true, false, true, false,
          true, false, true, false
        };
      }
    }

    public static List<bool> H
    {
      get
      {
        return new List<bool>
        {
          true, false, true, false,
          true, false, true, false,
          true, true, true, false,
          true, false, true, false,
          true, false, true, false
        };
      }
    }

    public static List<bool> I
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          false, true, false, false,
          false, true, false, false,
          false, true, false, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> J
    {
      get
      {
        return new List<bool>
        {
          false, false, true, false,
          false, false, true, false,
          false, false, true, false,
          true, false, true, false,
          false, true, false, false
        };
      }
    }

    public static List<bool> K
    {
      get
      {
        return new List<bool>
        {
          true, false, false, true, false,
          true, false, true, false, false,
          true, true, false, false, false,
          true, false, true, false, false,
          true, false, false, true, false
        };
      }
    }

    public static List<bool> L
    {
      get
      {
        return new List<bool>
        {
          true, false, false, false,
          true, false, false, false,
          true, false, false, false,
          true, false, false, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> M
    {
      get
      {
        return new List<bool>
        {
          true, false, false, false, true, false,
          true, true, false, true, true, false,
          true, false, true, false, true, false,
          true, false, false, false, true, false,
          true, false, false, false, true, false
        };
      }
    }

    public static List<bool> N
    {
      get
      {
        return new List<bool>
        {
          true, false, false, true, false,
          true, true, false, true, false,
          true, false, true, true, false,
          true, false, false, true, false,
          true, false, false, true, false,
        };
      }
    }

    public static List<bool> O
    {
      get
      {
        return new List<bool>
        {
          false, true, false, false,
          true, false, true, false,
          true, false, true, false,
          true, false, true, false,
          false, true, false, false
        };
      }
    }

    public static List<bool> P
    {
      get
      {
        return new List<bool>
        {
          true, true, false, false,
          true, false, true, false,
          true, true, false, false,
          true, false, false, false,
          true, false, false, false
        };
      }
    }

    public static List<bool> Q
    {
      get
      {
        return new List<bool>
        {
          false, true, true, false, false,
          true, false, true, false, false,
          true, false, true, false, false,
          true, false, true, false, false,
          false, true, false, true, false
        };
      }
    }

    public static List<bool> R
    {
      get
      {
        return new List<bool>
        {
          true, true, false, false,
          true, false, true, false,
          true, true, false, false,
          true, false, true, false,
          true, false, true, false
        };
      }
    }

    public static List<bool> S
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, false, false,
          true, true, true, false,
          false, false, true, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> T
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          false, true, false, false,
          false, true, false, false,
          false, true, false, false,
          false, true, false, false
        };
      }
    }

    public static List<bool> U
    {
      get
      {
        return new List<bool>
        {
          true, false, true, false,
          true, false, true, false,
          true, false, true, false,
          true, false, true, false,
          false, true, false, false
        };
      }
    }

    public static List<bool> V
    {
      get
      {
        return new List<bool>
        {
          true, false, false, false, true, false,
          true, false, false, false, true, false,
          false, true, false, true, false, false,
          false, true, false, true, false, false,
          false, false, true, false, false, false
        };
      }
    }

    public static List<bool> W
    {
      get
      {
        return new List<bool>
        {
          true, false, false, false, true, false,
          true, false, false, false, true, false,
          true, false, true, false, true, false,
          true, true, false, true, true, false,
          true, false, false, false, true, false
        };
      }
    }

    public static List<bool> X
    {
      get
      {
        return new List<bool>
        {
          true, false, true, false,
          true, false, true, true,
          false, true, false, false,
          true, false, true, true,
          true, false, true, false,
        };
      }
    }

    public static List<bool> Y
    {
      get
      {
        return new List<bool>
        {
          true, false, true, false,
          true, false, true, false,
          false, true, false, false,
          false, true, false, false,
          false, true, false, false
        };
      }
    }

    public static List<bool> Z
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          false, false, true, false,
          false, true, false, false,
          true, false, false, false,
          true, true, true, false
        };
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public PoliceList()
    {

    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    public PoliceList(int largeur, int position)
    {
      Largeur = largeur;
      Position = position;
    }

    /// <summary>
    /// GetPolice
    /// </summary>
    /// <param name="lettre"></param>
    /// <returns></returns>
    public static PoliceList GetPolice(int position, int offSet, char lettre)
    {
      switch (lettre)
      {
        case ' ':
          return GetPolice(offSet, position, Espace);

        case 'A':
          return GetPolice(offSet, position, A);

        case 'B':
          return GetPolice(offSet, position, B);

        case 'C':
          return GetPolice(offSet, position, C);

        case 'D':
          return GetPolice(offSet, position, D);

        case 'E':
          return GetPolice(offSet, position, E);

        case 'F':
          return GetPolice(offSet, position, F);

        case 'G':
          return GetPolice(offSet, position, G);

        case 'H':
          return GetPolice(offSet, position, H);

        case 'I':
          return GetPolice(offSet, position, I);

        case 'J':
          return GetPolice(offSet, position, J);

        case 'K':
          return GetPolice(offSet, position, K);

        case 'L':
          return GetPolice(offSet, position, L);

        case 'M':
          return GetPolice(offSet, position, M);

        case 'N':
          return GetPolice(offSet, position, N);

        case 'O':
          return GetPolice(offSet, position, O);

        case 'P':
          return GetPolice(offSet, position, P);

        case 'Q':
          return GetPolice(offSet, position, Q);

        case 'R':
          return GetPolice(offSet, position, R);

        case 'S':
          return GetPolice(offSet, position, S);

        case 'T':
          return GetPolice(offSet, position, T);

        case 'U':
          return GetPolice(offSet, position, U);

        case 'V':
          return GetPolice(offSet, position, V);

        case 'W':
          return GetPolice(offSet, position, W);

        case 'X':
          return GetPolice(offSet, position, X);

        case 'Y':
          return GetPolice(offSet, position, Y);

        case 'Z':
          return GetPolice(offSet, position, Z);
      }

      return null;
    }

    /// <summary>
    /// GetLargeur
    /// </summary>
    /// <param name = "lettre" ></ param >
    /// < returns ></ returns >
    public static int GetLargeur(char lettre)
    {
      switch (lettre)
      {
        case ' ':
          return 1;

        case 'K':
        case 'N':
        case 'Q':
          return 5;

        case 'M':
        case 'V':
        case 'W':
          return 6;

        default:
          return 4;
      }
    }

    /// <summary>
    /// GetPolice
    /// </summary>
    /// <param name="largeur"></param>
    /// <param name="points"></param>
    /// <returns></returns>
    public static PoliceList GetPolice(int offSet, int position, List<bool> points)
    {
      PoliceList polices = new PoliceList(points.Count / Hauteur, position);

      foreach (bool point in points)
        polices.AddNew(point, offSet);

      return polices;
    }

    /// <summary>
    /// AddNew
    /// </summary>
    /// <param name="point"></param>
    /// <param name="offSet"></param>
    private void AddNew(bool point, int offSet)
    {
      if (new Police(Largeur, Count, Position, point) is Police police && police.X >= offSet)
        Add(police);
    }

    //private void PrintLettre(Coordonnee coord, char lettre, Couleur couleur)
    //{
    //  Coordonnee coo = new Coordonnee(coord);

    //switch (lettre)
    //{
    //  case '0':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case '1':
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Droite(1).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case '2':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case '3':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;


    //  case '4':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    break;

    //  case '5':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case '6':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case '7':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    break;

    //  case '8':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case '9':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    break;

    //  case 'C':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case '°':
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
    //    break;

    //  case ':':
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(2)).SetColor(couleur);
    //    break;

    //  case '.':
    //    GetCoordonnee(coo.Bas(4)).SetColor(couleur);
    //    break;

    //  case '%':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    break;

    //  case ' ':
    //    break;

    //  case '-':
    //    GetCoordonnee(coo.Bas(2)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    break;

    //  case 'H':
    //    GetCoordonnee(coo).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    break;
    //}
    //}

  }
}
