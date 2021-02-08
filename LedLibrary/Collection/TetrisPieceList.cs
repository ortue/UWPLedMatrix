using LedLibrary.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class TetrisPieceList : List<TetrisPiece>
  {
    public int PieceID
    {
      get { return this.FirstOrDefault().PieceID; }
    }

    public int Rotation
    {
      get { return this.FirstOrDefault().Rotation; }
    }

    public int Largeur
    {
      get { return this.Max(p => p.X); }
    }

    /// <summary>
    /// HorizontalScore
    /// </summary>
    /// <param name="pieces"></param>
    public TetrisHorizontalList HorizontalScore(TetrisPieceList pieces)
    {
      //Prendre les pixel les plus bas de la piece de tetris
      TetrisPieceList pieceMax = new TetrisPieceList();

      foreach (int x in pieces.Select(p => p.X).Distinct())
        pieceMax.Add(new TetrisPiece(x, pieces.Where(p => p.X == x).Max(p => p.Y)));

      //Remettre les coordonné pret de zéro pour calculer seulement la différence des Y
      while (pieceMax.All(p => p.Y != 0))
        foreach (TetrisPiece max in pieceMax)
          max.Y--;

      //Prendre les pixel les plus haut de la base du jeu
      List<KeyValuePair<int, int>> horisontalMin = new List<KeyValuePair<int, int>>();

      for (int x = 2; x < 11; x++)
        if (this.Any(p => p.X == x))
          horisontalMin.Add(new KeyValuePair<int, int>(x, this.Where(p => p.X == x).Min(p => p.Y)));
        else
          horisontalMin.Add(new KeyValuePair<int, int>(x, 19));

      //Trouver le pourcentage de fittage entre les deux collections, en bas de 10
      TetrisHorizontalList horizontalScore = new TetrisHorizontalList();

      for (int x = 2; x < 11; x++)
      {
        int score = GetScore(x, pieceMax, horisontalMin);

        horizontalScore.Add(new TetrisHorizontal(x, score));
      }

      return horizontalScore;
    }

    /// <summary>
    /// GetScore
    /// </summary>
    /// <param name="x"></param>
    /// <param name="horisontalMin"></param>
    /// <returns></returns>
    private int GetScore(int x, TetrisPieceList pieceMax, List<KeyValuePair<int, int>> horisontalMin)
    {
      int? tmpY = null;
      List<bool> fit = new List<bool>();

      foreach (TetrisPiece max in pieceMax.OrderBy(p => p.X))
        if (horisontalMin.SingleOrDefault(h => h.Key == max.X + x) is KeyValuePair<int, int> min)
        {
          if (tmpY == null)
          {
            tmpY = min.Value;

            fit.Add(true);
          }
          else if (min.Value == tmpY + max.Y)
            fit.Add(true);
          else
            fit.Add(false);
        }

      //Si il y a un fit parfait on rajoute 6 pour privilégier ce move
      if (fit.Count(f => f.Equals(true)) == pieceMax.Count)
        return (int)((double)tmpY * (fit.Count(f => f.Equals(true)) / pieceMax.Count)) + 6;

      return (int)((double)tmpY * (fit.Count(f => f.Equals(true)) / pieceMax.Count));
    }

    /// <summary>
    /// GetPiece
    /// </summary>
    /// <param name="pieceID"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList GetPiece(int pieceID, int rotation = 0)
    {
      return pieceID switch
      {
        0 => Carre(rotation % 4),
        1 => Barre(rotation % 4),
        2 => Elle(rotation % 4),
        3 => Erre(rotation % 4),
        4 => Te(rotation % 4),
        5 => Esse(rotation % 4),
        6 => Zede(rotation % 4),
        _ => null
      };
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public TetrisPieceList()
    {

    }

    /// <summary>
    /// TetrisPieceList
    /// </summary>
    /// <param name="typePieces"></param>
    public TetrisPieceList(TetrisPieceList typePieces)
    {
      foreach (TetrisPiece tetrisPiece in typePieces)
        Add(new TetrisPiece(tetrisPiece));
    }

    /// <summary>
    /// Carre
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList Carre(int rotation)
    {
      Couleur couleur = Couleur.Get(127, 0, 127);

      return rotation switch
      {
        0 => new TetrisPieceList()
        {
          new TetrisPiece(0) { Rotation = 0, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 0, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 0, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 0, X = 1, Y = 1, Couleur = couleur }
        },
        1 => new TetrisPieceList()
        {
          new TetrisPiece(0) { Rotation = 1, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 1, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 1, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 0, X = 1, Y = 1, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(0) { Rotation = 2, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 2, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 2, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 0, X = 1, Y = 1, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(0) { Rotation = 3, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 3, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 3, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(0) { Rotation = 0, X = 1, Y = 1, Couleur = couleur }
        },
        _ => null
      };
    }

    /// <summary>
    /// Barre
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList Barre(int rotation)
    {
      Couleur couleur = Couleur.Get(0, 0, 127);

      return rotation switch
      {
        0 => new TetrisPieceList()
        {
          new TetrisPiece(1) { Rotation = 0, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 0, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 0, X = 0, Y = 2, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 0, X = 0, Y = 3, Couleur = couleur }
        },
        1 => new TetrisPieceList()
        {
          new TetrisPiece(1) { Rotation = 1, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 1, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 1, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 1, X = 3, Y = 0, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(1) { Rotation = 2, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 2, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 2, X = 0, Y = 2, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 2, X = 0, Y = 3, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(1) { Rotation = 3, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 3, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 3, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(1) { Rotation = 3, X = 3, Y = 0, Couleur = couleur }
        },
        _ => null
      };
    }

    /// <summary>
    /// Elle
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList Elle(int rotation)
    {
      Couleur couleur = Couleur.Get(0, 127, 0);

      return rotation switch
      {
        0 => new TetrisPieceList()
        {
          new TetrisPiece(2) { Rotation = 0, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 0, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 0, X = 0, Y = 2, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 0, X = 1, Y = 2, Couleur = couleur }
        },
        1 => new TetrisPieceList()
        {
          new TetrisPiece(2) { Rotation = 1, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 1, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 1, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 1, X = 0, Y = 1, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(2) { Rotation = 2, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 2, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 2, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 2, X = 1, Y = 2, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(2) { Rotation = 3, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 3, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 3, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(2) { Rotation = 3, X = 2, Y = 1, Couleur = couleur }
        },
        _ => null
      };
    }

    /// <summary>
    /// Erre
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList Erre(int rotation)
    {
      Couleur couleur = Couleur.Get(127, 127, 0);

      return rotation switch
      {
        0 => new TetrisPieceList()
        {
          new TetrisPiece(3) { Rotation = 0, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 0, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 0, X = 0, Y = 2, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 0, X = 1, Y = 2, Couleur = couleur }
        },
        1 => new TetrisPieceList()
        {
          new TetrisPiece(3) { Rotation = 1, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 1, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 1, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 1, X = 2, Y = 1, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(3) { Rotation = 2, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 2, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 2, X = 0, Y = 2, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 2, X = 1, Y = 2, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(3) { Rotation = 3, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 3, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 3, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 3, X = 2, Y = 1, Couleur = couleur }
        },
        _ => null
      };

    }

    /// <summary>
    /// Te
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList Te(int rotation)
    {
      Couleur couleur = Couleur.Get(127, 0, 0);

      return rotation switch
      {
        0 => new TetrisPieceList()
        {
          new TetrisPiece(4) { Rotation = 0, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 0, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 0, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 0, X = 0, Y = 2, Couleur = couleur }
        },
        1 => new TetrisPieceList()
        {
          new TetrisPiece(4) { Rotation = 1, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 1, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 1, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 1, X = 1, Y = 1, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(4) { Rotation = 2, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 2, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 2, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 2, X = 1, Y = 2, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(4) { Rotation = 3, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 3, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 3, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(4) { Rotation = 3, X = 2, Y = 1, Couleur = couleur }
        },
        _ => null
      };
    }

    /// <summary>
    /// Esse
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList Esse(int rotation)
    {
      Couleur couleur = Couleur.Get(0, 127, 127);

      return rotation switch
      {
        0 => new TetrisPieceList()
        {
          new TetrisPiece(5) { Rotation = 0, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 0, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 0, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 0, X = 1, Y = 2, Couleur = couleur }
        },
        1 => new TetrisPieceList()
        {
          new TetrisPiece(5) { Rotation = 1, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 1, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 1, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 1, X = 1, Y = 1, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(5) { Rotation = 2, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 2, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 2, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 2, X = 1, Y = 2, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(5) { Rotation = 3, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 3, X = 2, Y = 0, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 3, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(5) { Rotation = 3, X = 1, Y = 1, Couleur = couleur }
        },
        _ => null
      };
    }

    /// <summary>
    /// Zede
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static TetrisPieceList Zede(int rotation)
    {
      Couleur couleur = Couleur.Get(127, 63, 0);

      return rotation switch
      {
        0 => new TetrisPieceList()
        {
          new TetrisPiece(6) { Rotation = 0, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 0, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 0, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 0, X = 0, Y = 2, Couleur = couleur }
        },
        1 => new TetrisPieceList()
        {
          new TetrisPiece(6) { Rotation = 1, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 1, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 1, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 1, X = 2, Y = 1, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(6) { Rotation = 2, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 2, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 2, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 2, X = 0, Y = 2, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(6) { Rotation = 3, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 3, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 3, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(6) { Rotation = 3, X = 2, Y = 1, Couleur = couleur }
        },
        _ => null
      };
    }
  }
}