using LedLibrary.Entities;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace LedLibrary.Collection
{
  public class PoliceList : List<Police>
  {
    public int Offset { get; set; }
    public char Lettre { get; set; }
    public int Position { get; set; }
    public int Compteur { get; set; }

    public int Largeur
    {
      get
      {
        return Lettre switch
        {
          char x when x == ' ' || x == '\'' || x == '!' || x == ':' || x == '.' => 2,
          char x when x == '-' || x == ',' || x == ';' => 3,
          char x when x == 'C' || x == 'G' || x == 'K' || x == 'N' || x == 'O' || x == 'Q' || x == 'U' || x == 'P' => 5,
          char x when x == '«' || x == '»' || x == '%' || x == 'V' || x == 'M' || x == 'W' => 6,
          _ => 4
        };
      }
    }

    public static List<bool> Espace
    {
      get
      {
        return new List<bool>
        {
          false, false,
          false, false,
          false, false,
          false, false,
          false, false
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
    //public PoliceList(int largeur, int position)
    //{
    //  Largeur = largeur;
    //  Position = position;
    //}

    /// <summary>
    /// GetPolice
    /// </summary>
    /// <param name="lettre"></param>
    /// <returns></returns>
    public static PoliceList GetPolice(int offSet, int position, char lettre)
    {
      PoliceList polices = new PoliceList
      {
        Lettre = lettre,
        Offset = offSet,
        Position = position
      };

      return lettre switch
      {
        ' ' => GetPolice(polices, Espace),
        'A' => GetPolice(polices, A),
        'B' => GetPolice(polices, B),
        'C' => GetPolice(polices, C),
        'D' => GetPolice(polices, D),
        'E' => GetPolice(polices, E),
        'F' => GetPolice(polices, F),
        'G' => GetPolice(polices, G),
        'H' => GetPolice(polices, H),
        'I' => GetPolice(polices, I),
        'J' => GetPolice(polices, J),
        'K' => GetPolice(polices, K),
        'L' => GetPolice(polices, L),
        'M' => GetPolice(polices, M),
        'N' => GetPolice(polices, N),
        'O' => GetPolice(polices, O),
        'P' => GetPolice(polices, P),
        'Q' => GetPolice(polices, Q),
        'R' => GetPolice(polices, R),
        'S' => GetPolice(polices, S),
        'T' => GetPolice(polices, T),
        'U' => GetPolice(polices, U),
        'V' => GetPolice(polices, V),
        'W' => GetPolice(polices, W),
        'X' => GetPolice(polices, X),
        'Y' => GetPolice(polices, Y),
        'Z' => GetPolice(polices, Z),
        '0' => GetPolice(polices, Zero),
        '1' => GetPolice(polices, Un),
        '2' => GetPolice(polices, Deux),
        '3' => GetPolice(polices, Trois),
        '4' => GetPolice(polices, Quatre),
        '5' => GetPolice(polices, Cinq),
        '6' => GetPolice(polices, Six),
        '7' => GetPolice(polices, Sept),
        '8' => GetPolice(polices, Huit),
        '9' => GetPolice(polices, Neuf),
        '.' => GetPolice(polices, Point),
        ',' => GetPolice(polices, Virgule),
        '\'' => GetPolice(polices, Guillemet),
        ':' => GetPolice(polices, DeuxPoint),
        ';' => GetPolice(polices, PointVirgule),
        '?' => GetPolice(polices, Interrogation),
        '-' => GetPolice(polices, Moins),
        '«' => GetPolice(polices, OuvrirGuillemet),
        '»' => GetPolice(polices, FermerGuillemet),
        '%' => GetPolice(polices, Pourcent),
        _ => null,
      };
    }


    /// <summary>
    /// GetPolice
    /// </summary>
    /// <param name="polices"></param>
    /// <param name="points"></param>
    /// <returns></returns>
    public static PoliceList GetPolice(PoliceList polices, List<bool> points)
    {
      foreach (bool point in points)
        polices.AddNew(point, polices.Offset);

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