namespace Library.Entity
{
  public class TetrisPiece
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int PieceID { get; set; }
    public int Rotation { get; set; }
    public bool TestScore { get; set; }
    public Couleur Couleur { get; set; }
    public Couleur TmpCouleur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public TetrisPiece(int pieceID)
    {
      Couleur = new Couleur();
      TmpCouleur = new Couleur();

      PieceID = pieceID;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="tetrisPieces"></param>
    public TetrisPiece()
    {
      Couleur = new Couleur();
      TmpCouleur = new Couleur();
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
      TmpCouleur = tetrisPiece.Couleur;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="tetrisPiece"></param>
    /// <param name="x"></param>
    public TetrisPiece(TetrisPiece tetrisPiece, int x)
    {
      TmpCouleur = new Couleur();

      PieceID = tetrisPiece.PieceID;
      Rotation = tetrisPiece.Rotation;
      X = tetrisPiece.X + x;
      Y = tetrisPiece.Y;
      Couleur = tetrisPiece.Couleur;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="tetrisPiece"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public TetrisPiece(TetrisPiece tetrisPiece, int x, int y)
    {
      PieceID = tetrisPiece.PieceID;
      Rotation = tetrisPiece.Rotation;
      X = tetrisPiece.X + x;
      Y = tetrisPiece.Y + y;
      Couleur = tetrisPiece.Couleur;
      TmpCouleur = tetrisPiece.Couleur;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="tetrisPiece"></param>
    /// <param name="y"></param>
    /// <param name="testScore"></param>
    public TetrisPiece(TetrisPiece tetrisPiece, int y, bool testScore)
    {
      PieceID = tetrisPiece.PieceID;
      Rotation = tetrisPiece.Rotation;
      X = tetrisPiece.X;
      Y = tetrisPiece.Y + y;
      Couleur = tetrisPiece.Couleur;
      TmpCouleur = tetrisPiece.Couleur;
      TestScore = testScore;
    }
  }
}