using LedLibrary.Collection;

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
    
    }
  }
}