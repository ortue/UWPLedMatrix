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
        0 => Carre(rotation),
        1 => Barre(rotation),
        2 => Elle(rotation),
        3 => Erre(rotation),
        4 => Te(rotation),
        5 => Esse(rotation),
        6 => Zede(rotation),
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
          new TetrisPiece(2) { Rotation = 3, X = 2, Y = 2, Couleur = couleur }
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
          new TetrisPiece(3) { Rotation = 1, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 1, X = 1, Y = 1, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 1, X = 1, Y = 2, Couleur = couleur }
        },
        2 => new TetrisPieceList()
        {
          new TetrisPiece(3) { Rotation = 2, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 2, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 2, X = 0, Y = 1, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 2, X = 0, Y = 2, Couleur = couleur }
        },
        3 => new TetrisPieceList()
        {
          new TetrisPiece(3) { Rotation = 3, X = 0, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 3, X = 1, Y = 0, Couleur = couleur },
          new TetrisPiece(3) { Rotation = 3, X = 2, Y = 0, Couleur = couleur },
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
          new TetrisPiece(5) { Rotation = 1, X = 0, Y = 0, Couleur = couleur },
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
          new TetrisPiece(5) { Rotation = 3, X = 0, Y = 0, Couleur = couleur },
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
          new TetrisPiece(6) { Rotation = 1, X = 1, Y = 2, Couleur = couleur }
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
          new TetrisPiece(6) { Rotation = 3, X = 1, Y = 2, Couleur = couleur }
        },
        _ => null
      };
    }
  }
}