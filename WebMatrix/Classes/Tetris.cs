using LedLibrary.Collection;
using System;

namespace WebMatrix.Classes
{
  public class Tetris
  {
    public TetrisPieceList PieceTombes { get; set; }
    public TetrisPieceList Nexts { get; set; }
    public TetrisPieceList Pieces { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Tetris()
    {
      SetNext();
    }

    /// <summary>
    /// SetNext
    /// </summary>
    private void SetNext()
    {
      Random r = new Random();

      Nexts = new TetrisPieceList(TetrisPieceList.GetPiece(r.Next(0, 6)));
    }
  }
}