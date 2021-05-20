using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class TetrisPieceList : List<TetrisPiece>
  {
    public double WeightHeight
    {
      get { return 0.510066; }
    }

    public double WeightLines
    {
      get { return 0.760666; }
    }

    public double WeightHoles
    {
      get { return 0.35663; }
    }

    public double WeightBumpiness
    {
      get { return 0.184483; }
    }

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

    public int Top
    {
      get
      {
        if (this.Any())
          return this.Min(p => p.Y);

        return 19;
      }
    }

    public double AggregateHeight
    {
      get
      {
        double height = 0;

        for (int x = 2; x < 12; x++)
          if (this.Any(p => p.X == x))
            height += 19 - this.Where(p => p.X == x).Min(p => p.Y);

        return height;
      }
    }

    public double Lines
    {
      get
      {
        double lines = 0;

        for (int y = -3; y < 19; y++)
          if (this.Count(p => p.Y == y) >= 10)
            lines++;

        return lines;
      }
    }

    public double Holes
    {
      get
      {
        double holes = 0;

        for (int x = 2; x < 12; x++)
          if (this.Any(p => p.X == x))
            holes += 19 - this.Where(p => p.X == x).Min(p => p.Y) - this.Count(p => p.X == x);

        return holes;
      }
    }

    public double Bumpiness
    {
      get
      {
        double bumpiness = 0;

        for (int x = 2; x < 11; x++)
          bumpiness += Math.Abs(ColumnHeight(x) - ColumnHeight(x + 1));

        return bumpiness;
      }
    }

    /// <summary>
    /// Vérifier si y a une ligne pleine pour arreter le jeu.
    /// </summary>
    public bool LignePleine
    {
      get
      {
        for (int y = -3; y < 19; y++)
          if (this.Count(p => p.Y == y) == 10)
            return true;

        return false;
      }
    }

    /// <summary>
    /// ColumnHeight
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public int ColumnHeight(int x)
    {
      if (this.Any(p => p.X == x))
        return 19 - this.Where(p => p.X == x).Min(p => p.Y);

      return 0;
    }

    /// <summary>
    /// GetAiScore
    /// </summary>
    /// <param name="pieces"></param>
    /// <returns></returns>
    public double GetAiScore(TetrisPieceList pieces)
    {
      int y = -3;

      while (!CheckBottom(pieces, y) && y < 19)
        y++;

      foreach (TetrisPiece tetrisPiece in pieces)
        Add(new TetrisPiece(tetrisPiece, y, true));

      double score = -WeightHeight * AggregateHeight + WeightLines * Lines - WeightHoles * Holes - WeightBumpiness * Bumpiness;

      RemoveTest();

      return score;
    }

    /// <summary>
    /// RemoveTest
    /// </summary>
    private void RemoveTest()
    {
      for (int y = -3; y < 19; y++)
        for (int x = 2; x < 12; x++)
          if (Find(t => t.X == x && t.Y == y && t.TestScore) is TetrisPiece piece)
            Remove(piece);
    }

    /// <summary>
    /// CheckBottom
    /// </summary>
    /// <param name="pieces"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool CheckBottom(TetrisPieceList pieces, int y)
    {
      if (pieces.Max(p => p.Y) + y >= 18)
        return true;

      foreach (TetrisPiece tetrisPiece in pieces)
        if (this.Any(p => p.X == tetrisPiece.X && p.Y == tetrisPiece.Y + y + 1))
          return true;

      return false;
    }

    /// <summary>
    /// EffacerLigne
    /// </summary>
    /// <param name="y"></param>
    public TetrisPieceList EffacerLigne(int y)
    {
      TetrisPieceList tetrisPieces = new TetrisPieceList();

      for (int x = 2; x < 12; x++)
      {
        if (this.FirstOrDefault(p => p.X == x && p.Y == y) is TetrisPiece pieceRemove)
        {
          tetrisPieces.Add(new TetrisPiece(pieceRemove));
          Remove(pieceRemove);
        }

        foreach (TetrisPiece piece in this.Where(p => p.X == x && p.Y < y))
          piece.Y++;
      }

      return tetrisPieces;
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
    /// Constructeur
    /// </summary>
    /// <param name="typePieces"></param>
    public TetrisPieceList(TetrisPieceList typePieces)
    {
      foreach (TetrisPiece tetrisPiece in typePieces)
        Add(new TetrisPiece(tetrisPiece));
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="typePieces"></param>
    public TetrisPieceList(TetrisPieceList typePieces, int x)
    {
      foreach (TetrisPiece tetrisPiece in typePieces)
        Add(new TetrisPiece(tetrisPiece, x));
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
      Couleur couleur = Couleur.Get(16, 16, 127);

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