using LedLibrary.Collection;

namespace LedLibrary.Entities
{
  public class TetrisPiece
  {
    public int PieceID { get; set; }
    public int Rotation { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Couleur Couleur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public TetrisPiece(int pieceID)
    {
      PieceID = pieceID;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="tetrisPieces"></param>
    public TetrisPiece()
    {


    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="tetrisPiece"></param>
    public TetrisPiece(TetrisPiece tetrisPiece)
    {
      PieceID = tetrisPiece.PieceID;
      Rotation = tetrisPiece.Rotation;
      X = tetrisPiece.X;
      Y = tetrisPiece.Y;
      Couleur = tetrisPiece.Couleur;
    }
  }
}