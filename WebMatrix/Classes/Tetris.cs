using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Linq;

namespace WebMatrix.Classes
{
  public class Tetris
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Score { get; set; }
    public TetrisPieceList Nexts { get; set; }
    public TetrisPieceList Pieces { get; set; }
    public TetrisPieceList PieceTombes { get; set; }

    public double Vitesse
    {
      get { return 20 - Score / 2; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Tetris()
    {
      PieceTombes = new TetrisPieceList();

      NouvellePiece();
    }

    /// <summary>
    /// NouvellePiece
    /// </summary>
    public void NouvellePiece()
    {
      Y = -3;
      X = 6;

      Random r = new Random();

      if (Nexts == null)
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(r.Next(0, 7)));
      else
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(Nexts.PieceID));

      Nexts = new TetrisPieceList(TetrisPieceList.GetPiece(r.Next(0, 7)));
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    /// <param name="cycle"></param>
    public void Mouvement(int cycle)
    {
      if (cycle++ % Vitesse == 0)
      {
        Y++;

        if (PieceTombes.GetXBottom > X)
          X += 1;

        if (PieceTombes.GetXBottom < X)
          X -= 1;

        //Rotate();
      }
    }

    /// <summary>
    /// Rotate
    /// </summary>
    private void Rotate()
    {
      Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(Pieces.PieceID, Pieces.Rotation + 1));
    }

    /// <summary>
    /// Bottom
    /// </summary>
    public bool Bottom()
    {
      if (CheckBottom())
      {
        if (Y < -2)
          return true;

        SetPieceTombe();
        NouvellePiece();
      }

      return false;
    }

    /// <summary>
    /// CheckBottom
    /// </summary>
    /// <returns></returns>
    private bool CheckBottom()
    {
      if (Pieces.Max(p => p.Y) + Y >= 18)
        return true;

      foreach (TetrisPiece tetrisPiece in Pieces)
        if (PieceTombes.Any(p => p.X == tetrisPiece.X + X && p.Y == tetrisPiece.Y + Y + 1))
          return true;

      return false;
    }

    /// <summary>
    /// SetPieceTombe
    /// </summary>
    private void SetPieceTombe()
    {
      foreach (TetrisPiece tetrisPiece in Pieces)
        PieceTombes.Add(new TetrisPiece(tetrisPiece, X, Y));
    }
  }
}