using Library.Entity;

namespace Library.Collection
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
          ' ' => 2,
          '\'' => 2,
          '’' => 2,
          '!' => 2,
          ':' => 2,
          '.' => 2,

          '-' => 3,
          ',' => 3,
          ';' => 3,

          'C' => 5,
          'G' => 5,
          'K' => 5,
          'N' => 5,
          'O' => 5,
          'P' => 5,
          'Q' => 5,
          'U' => 5,

          '«' => 6,
          '»' => 6,
          'M' => 6,
          'W' => 6,
          'V' => 6,

          _ => 4
        };
      }
    }

    public static List<bool> Espace => new() { false, false, false, false, false, false, false, false, false, false };
    public static List<bool> A => new() { false, true, false, false, true, false, true, false, true, true, true, false, true, false, true, false, true, false, true, false };
    public static List<bool> B => new() { true, true, false, false, true, false, true, false, true, true, false, false, true, false, true, false, true, true, true, false };
    public static List<bool> C => new() { false, true, true, false, false, true, false, false, true, false, true, false, false, false, false, true, false, false, true, false, false, true, true, false, false };
    public static List<bool> D => new() { true, true, false, false, true, false, true, false, true, false, true, false, true, false, true, false, true, true, false, false };
    public static List<bool> E => new() { true, true, true, false, true, false, false, false, true, true, false, false, true, false, false, false, true, true, true, false };
    public static List<bool> F => new() { true, true, true, false, true, false, false, false, true, true, false, false, true, false, false, false, true, false, false, false };
    public static List<bool> G => new() { false, true, true, false, false, true, false, false, false, false, true, false, true, true, false, true, false, false, true, false, false, true, true, false, false };
    public static List<bool> H => new() { true, false, true, false, true, false, true, false, true, true, true, false, true, false, true, false, true, false, true, false };
    public static List<bool> I => new() { true, true, true, false, false, true, false, false, false, true, false, false, false, true, false, false, true, true, true, false };
    public static List<bool> J => new() { false, false, true, false, false, false, true, false, false, false, true, false, true, false, true, false, false, true, false, false };
    public static List<bool> K => new() { true, false, false, true, false, true, false, true, false, false, true, true, false, false, false, true, false, true, false, false, true, false, false, true, false };
    public static List<bool> L => new() { true, false, false, false, true, false, false, false, true, false, false, false, true, false, false, false, true, true, true, false };
    public static List<bool> M => new() { true, false, false, false, true, false, true, true, false, true, true, false, true, false, true, false, true, false, true, false, false, false, true, false, true, false, false, false, true, false };
    public static List<bool> N => new() { true, false, false, true, false, true, true, false, true, false, true, false, true, true, false, true, false, false, true, false, true, false, false, true, false };
    public static List<bool> O => new() { false, true, true, false, false, true, false, false, true, false, true, false, false, true, false, true, false, false, true, false, false, true, true, false, false };
    public static List<bool> P => new() { true, true, true, false, false, true, false, false, true, false, true, true, true, false, false, true, false, false, false, false, true, false, false, false, false };
    public static List<bool> Q => new() { false, true, true, false, false, true, false, false, true, false, true, false, false, true, false, true, false, true, false, false, false, true, false, true, false };
    public static List<bool> R => new() { true, true, false, false, true, false, true, false, true, true, false, false, true, false, true, false, true, false, true, false };
    public static List<bool> S => new() { false, true, true, false, true, false, false, false, true, true, true, false, false, false, true, false, true, true, false, false };
    public static List<bool> T => new() { true, true, true, false, false, true, false, false, false, true, false, false, false, true, false, false, false, true, false, false };
    public static List<bool> U => new() { true, false, false, true, false, true, false, false, true, false, true, false, false, true, false, true, false, false, true, false, false, true, true, false, false };
    public static List<bool> V => new() { true, false, false, false, true, false, true, false, false, false, true, false, false, true, false, true, false, false, false, true, false, true, false, false, false, false, true, false, false, false };
    public static List<bool> W => new() { true, false, false, false, true, false, true, false, false, false, true, false, true, false, true, false, true, false, true, true, false, true, true, false, true, false, false, false, true, false };
    public static List<bool> X => new() { true, false, true, false, true, false, true, false, false, true, false, false, true, false, true, false, true, false, true, false, };
    public static List<bool> Y => new() { true, false, true, false, true, false, true, false, false, true, false, false, false, true, false, false, false, true, false, false };
    public static List<bool> Z => new() { true, true, true, false, false, false, true, false, false, true, false, false, true, false, false, false, true, true, true, false };

    public static List<bool> Zero => new() { true, true, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, true, true, false };
    public static List<bool> Un => new() { false, true, false, false, true, true, false, false, false, true, false, false, false, true, false, false, true, true, true, false };
    public static List<bool> Deux => new() { true, true, true, false, false, false, true, false, true, true, true, false, true, false, false, false, true, true, true, false };
    public static List<bool> Trois => new() { true, true, true, false, false, false, true, false, true, true, true, false, false, false, true, false, true, true, true, false };
    public static List<bool> Quatre => new() { true, false, true, false, true, false, true, false, true, true, true, false, false, false, true, false, false, false, true, false };
    public static List<bool> Cinq => new() { true, true, true, false, true, false, false, false, true, true, true, false, false, false, true, false, true, true, true, false };
    public static List<bool> Six => new() { true, true, true, false, true, false, false, false, true, true, true, false, true, false, true, false, true, true, true, false };
    public static List<bool> Sept => new() { true, true, true, false, false, false, true, false, false, false, true, false, false, false, true, false, false, false, true, false };
    public static List<bool> Huit => new() { true, true, true, false, true, false, true, false, true, true, true, false, true, false, true, false, true, true, true, false };
    public static List<bool> Neuf => new() { true, true, true, false, true, false, true, false, true, true, true, false, false, false, true, false, false, false, true, false };

    public static List<bool> Point => new() { false, false, false, false, false, false, false, false, true, false };
    public static List<bool> DeuxPoint => new() { false, false, true, false, false, false, true, false, false, false };
    public static List<bool> PointVirgule => new() { false, false, false, false, true, false, false, false, false, false, true, false, true, false, false };
    public static List<bool> Virgule => new() { false, false, false, false, false, false, false, false, false, false, true, false, true, false, false };
    public static List<bool> Guillemet => new() { true, false, true, false, false, false, false, false, false, false };
    public static List<bool> Interrogation => new() { true, true, false, false, false, false, true, false, true, true, false, false, false, false, false, false, true, false, false, false };
    public static List<bool> Moins => new() { false, false, false, false, false, false, true, true, false, false, false, false, false, false, false };
    public static List<bool> OuvrirGuillemet => new() { false, false, false, false, false, false, false, true, false, false, true, false, true, false, false, true, false, false, false, true, false, false, true, false, false, false, false, false, false, false };
    public static List<bool> FermerGuillemet => new() { false, false, false, false, false, false, true, false, false, true, false, false, false, true, false, false, true, false, true, false, false, true, false, false, false, false, false, false, false, false };
    public static List<bool> Pourcent => new() { true, false, true, false, false, false, true, false, false, true, false, false, true, false, false, false, true, false, true, false };
    public static List<bool> Degree => new() { false, true, false, false, true, false, true, false, false, true, false, false, false, false, false, false, false, false, false, false };

    /// <summary>
    /// Constructeur
    /// </summary>
    public PoliceList()
    {

    }

    /// <summary>
    /// GetPolice
    /// </summary>
    /// <param name="lettre"></param>
    /// <returns></returns>
    public static PoliceList GetPolice(int offSet, int position, char lettre)
    {
      PoliceList polices = new()
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
        '’' => GetPolice(polices, Guillemet),
        '\'' => GetPolice(polices, Guillemet),
        ':' => GetPolice(polices, DeuxPoint),
        ';' => GetPolice(polices, PointVirgule),
        '?' => GetPolice(polices, Interrogation),
        '-' => GetPolice(polices, Moins),
        '«' => GetPolice(polices, OuvrirGuillemet),
        '»' => GetPolice(polices, FermerGuillemet),
        '%' => GetPolice(polices, Pourcent),
        '°' => GetPolice(polices, Degree),
        _ => GetPolice(polices, Espace),
      };
    }

    /// <summary>
    /// GetLargeur
    /// </summary>
    /// <param name="lettre"></param>
    /// <returns></returns>
    public static int GetLargeur(char lettre)
    {
      PoliceList polices = new() { Lettre = lettre };

      return polices.Largeur;
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
  }
}