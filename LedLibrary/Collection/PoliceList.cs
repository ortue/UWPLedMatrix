using LedLibrary.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace LedLibrary.Collection
{
  public class PoliceList : List<Police>
  {
    public int Largeur { get; set; }
    public int Position { get; set; }
    public int Compteur { get; set; }
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
          false, true, true, false, false,
          true, false, false, true, false,
          true, false, false, false, false,
          true, false, false, true, false,
          false, true, true, false, false
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
          false, true, true, false, false,
          true, false, false, false, false,
          true, false, true, true, false,
          true, false, false, true, false,
          false, true, true, false, false
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
          false, true, true, false, false,
          true, false, false, true, false,
          true, false, false, true, false,
          true, false, false, true, false,
          false, true, true, false, false
        };
      }
    }

    public static List<bool> P
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false, false,
          true, false, false, true, false,
          true, true, true, false, false,
          true, false, false, false, false,
          true, false, false, false, false
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
          true, false, false, true, false,
          true, false, false, true, false,
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
          false, true, true, false,
          true, false, false, false,
          true, true, true, false,
          false, false, true, false,
          true, true, false, false
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
          true, false, false, true, false,
          true, false, false, true, false,
          true, false, false, true, false,
          true, false, false, true, false,
          false, true, true, false, false
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
          true, false, true, false,
          false, true, false, false,
          true, false, true, false,
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

    public static List<bool> Zero
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, true, false,
          true, false, true, false,
          true, false, true, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> Un
    {
      get
      {
        return new List<bool>
        {
          false, true, false, false,
          true, true, false, false,
          false, true, false, false,
          false, true, false, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> Deux
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          false, false, true, false,
          true, true, true, false,
          true, false, false, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> Trois
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          false, false, true, false,
          true, true, true, false,
          false, false, true, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> Quatre
    {
      get
      {
        return new List<bool>
        {
          true, false, true, false,
          true, false, true, false,
          true, true, true, false,
          false, false, true, false,
          false, false, true, false
        };
      }
    }

    public static List<bool> Cinq
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

    public static List<bool> Six
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, false, false,
          true, true, true, false,
          true, false, true, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> Sept
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          false, false, true, false,
          false, false, true, false,
          false, false, true, false,
          false, false, true, false
        };
      }
    }

    public static List<bool> Huit
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, true, false,
          true, true, true, false,
          true, false, true, false,
          true, true, true, false
        };
      }
    }

    public static List<bool> Neuf
    {
      get
      {
        return new List<bool>
        {
          true, true, true, false,
          true, false, true, false,
          true, true, true, false,
          false, false, true, false,
          false, false, true, false
        };
      }
    }

    public static List<bool> Point
    {
      get
      {
        return new List<bool>
        {
          false, false,
          false, false,
          false, false,
          false, false,
          true, false
        };
      }
    }

    public static List<bool> DeuxPoint
    {
      get
      {
        return new List<bool>
        {
          false, false,
          true, false,
          false, false,
          true, false,
          false, false
        };
      }
    }

    public static List<bool> PointVirgule
    {
      get
      {
        return new List<bool>
        {
          false, false, false,
          false, true, false,
          false, false, false,
          false, true, false,
          true, false, false
        };
      }
    }

    public static List<bool> Virgule
    {
      get
      {
        return new List<bool>
        {
          false, false, false,
          false, false, false,
          false, false, false,
          false, true, false,
          true, false, false
        };
      }
    }

    public static List<bool> Guillemet
    {
      get
      {
        return new List<bool>
        {
          true, false,
          true, false,
          false, false,
          false, false,
          false, false
        };
      }
    }

    public static List<bool> Interrogation
    {
      get
      {
        return new List<bool>
        {
          true, true, false, false,
          false, false, true, false,
          true, true, false, false,
          false, false, false, false,
          true, false, false, false,
        };
      }
    }

    public static List<bool> Moins
    {
      get
      {
        return new List<bool>
        {
          false, false, false,
          false, false, false,
          true, true, false,
          false, false, false,
          false, false, false
        };
      }
    }

    public static List<bool> OuvrirGuillemet
    {
      get
      {
        return new List<bool>
        {
          false, false, false, false, false, false,
          false, true, false, false, true, false,
          true, false, false, true, false, false,
          false, true, false, false, true, false,
          false, false, false, false, false, false
        };
      }
    }

    public static List<bool> FermerGuillemet
    {
      get
      {
        return new List<bool>
        {
          false, false, false, false, false, false,
          true, false, false, true, false, false,
          false, true, false, false, true, false,
          true, false, false, true, false, false,
          false, false, false, false, false, false
        };
      }
    }

    public static List<bool> Pourcent
    {
      get
      {
        return new List<bool>
        {
          false, true, false, false, true, false,
          true, false, true, true, false, false,
          false, true, true, true, false, false,
          false, true, true, false, true, false,
          true, false, false, true, false, false
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
    public static PoliceList GetPolice(int offSet, int position, char lettre)
    {
      return lettre switch
      {
        ' ' => GetPolice(offSet, position, Espace),
        'A' => GetPolice(offSet, position, A),
        'B' => GetPolice(offSet, position, B),
        'C' => GetPolice(offSet, position, C),
        'D' => GetPolice(offSet, position, D),
        'E' => GetPolice(offSet, position, E),
        'F' => GetPolice(offSet, position, F),
        'G' => GetPolice(offSet, position, G),
        'H' => GetPolice(offSet, position, H),
        'I' => GetPolice(offSet, position, I),
        'J' => GetPolice(offSet, position, J),
        'K' => GetPolice(offSet, position, K),
        'L' => GetPolice(offSet, position, L),
        'M' => GetPolice(offSet, position, M),
        'N' => GetPolice(offSet, position, N),
        'O' => GetPolice(offSet, position, O),
        'P' => GetPolice(offSet, position, P),
        'Q' => GetPolice(offSet, position, Q),
        'R' => GetPolice(offSet, position, R),
        'S' => GetPolice(offSet, position, S),
        'T' => GetPolice(offSet, position, T),
        'U' => GetPolice(offSet, position, U),
        'V' => GetPolice(offSet, position, V),
        'W' => GetPolice(offSet, position, W),
        'X' => GetPolice(offSet, position, X),
        'Y' => GetPolice(offSet, position, Y),
        'Z' => GetPolice(offSet, position, Z),
        '0' => GetPolice(offSet, position, Zero),
        '1' => GetPolice(offSet, position, Un),
        '2' => GetPolice(offSet, position, Deux),
        '3' => GetPolice(offSet, position, Trois),
        '4' => GetPolice(offSet, position, Quatre),
        '5' => GetPolice(offSet, position, Cinq),
        '6' => GetPolice(offSet, position, Six),
        '7' => GetPolice(offSet, position, Sept),
        '8' => GetPolice(offSet, position, Huit),
        '9' => GetPolice(offSet, position, Neuf),
        '.' => GetPolice(offSet, position, Point),
        ',' => GetPolice(offSet, position, Virgule),
        '\'' => GetPolice(offSet, position, Guillemet),
        ':' => GetPolice(offSet, position, DeuxPoint),
        ';' => GetPolice(offSet, position, PointVirgule),
        '?' => GetPolice(offSet, position, Interrogation),
        '-' => GetPolice(offSet, position, Moins),
        '«' => GetPolice(offSet, position, OuvrirGuillemet),
        '»' => GetPolice(offSet, position, FermerGuillemet),
        '%' => GetPolice(offSet, position, Pourcent),
        _ => null,
      };
    }

    /// <summary>
    /// GetLargeur
    /// </summary>
    /// <param name = "lettre" ></ param >
    /// < returns ></ returns >
    public static int GetLargeur(char lettre)
    {
      return lettre switch
      {
        ' ' => 1,
        char x when x == '\'' || x == '!' || x == ':' || x == '.' => 2,
        char x when x == '-' || x == ',' || x == ';' => 3,
        char x when x == 'C' || x == 'G' || x == 'K' || x == 'N' || x == 'O' || x == 'Q' || x == 'U' => 5,
        char x when x == '«' || x == '»' || x == '%' => 6,
        _ => 4
      };
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
    /// Ajouter les pixel de la lettre, gestion des fractions de lettre
    /// </summary>
    /// <param name="point"></param>
    /// <param name="offSet"></param>
    private void AddNew(bool point, int offSet)
    {
      if (new Police(Largeur, Compteur++, Position - offSet, point) is Police police)//&& police.X >= offSet
        Add(police);
    }

    //  case '°':
    //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
    //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
    //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
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
  }
}