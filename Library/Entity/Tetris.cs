using Library.Collection;
using Library.Util;

namespace Library.Entity
{
  public class Tetris
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Score { get; set; }
    public int XOptimal { get; set; }
    public int RotationOptimal { get; set; }
    public List<int>? Poche { get; set; }
    public TetrisPieceList? Nexts { get; set; }
    public TetrisPieceList Pieces { get; set; }
    public TetrisPieceList PieceTombes { get; set; }

    public bool LignePleine
    {
      get { return PieceTombes.Lines > 0; }
    }

    public bool Mort
    {
      get { return PieceTombes.Top < -2; }
    }

    public double Vitesse
    {
      get
      {
        if (40 - Score / 10 > 2)
          return 50 - Score / 10;

        return 2;
      }
    }

    public string ScoreUn
    {
      get { return (Score % 100).ToString().Substring(0, 1); }
    }

    public string ScoreDeux
    {
      get
      {
        if ((Score % 100) > 9)
          return (Score % 100).ToString().Substring(1, 1);

        return string.Empty;
      }
    }

    public TetrisPieceList Centaines
    {
      get
      {
        TetrisPieceList centaines = new();

        for (int y = 0; y < (Score % 1000) / 100; y++)
          centaines.Add(new TetrisPiece { X = 13, Y = 7 + y });

        return centaines;
      }
    }

    public TetrisPieceList Milliers
    {
      get
      {
        TetrisPieceList milliers = new();

        for (int y = 0; y < Score / 1000; y++)
          milliers.Add(new TetrisPiece { X = 13, Y = 7 + y });

        return milliers;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Tetris()
    {
      Pieces = new TetrisPieceList();
      PieceTombes = new TetrisPieceList();

      NouvellePiece();
    }

    /// <summary>
    /// NouvellePiece
    /// </summary>
    /// <param name="version"></param>
    public void NouvellePiece()
    {
      Y = -3;
      X = 6;

      SetPoche();

      if (Nexts == null)
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(GetNext()));
      else
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(Nexts.PieceID));

      Nexts = new TetrisPieceList(TetrisPieceList.GetPiece(GetNext()));

      TetrisHorizontalList tetrisHorizontals = new();

      for (int rotation = 0; rotation < 4; rotation++)
        for (int x = 2; x < 12; x++)
          if (RotateX(rotation, x) is TetrisPieceList tmpPiece)
          {
            double score = PieceTombes.GetAiScore(tmpPiece);
            tetrisHorizontals.Add(new TetrisHorizontal(rotation, x, score));
          }

      XOptimal = tetrisHorizontals.ScoreX;
      RotationOptimal = tetrisHorizontals.ScoreRotation;
    }

    /// <summary>
    /// SetPoche
    /// </summary>
    private void SetPoche()
    {
      Random r = new();

      if (Poche == null || !Poche.Any())
      {
        Poche = new List<int>();

        while (Poche.Count < 7)
        {
          int i = r.Next(0, 7);

          if (!Poche.Any(p => p.Equals(i)))
            Poche.Add(i);
        }
      }
    }

    /// <summary>
    /// GetNext
    /// </summary>
    /// <returns></returns>
    private int GetNext()
    {
      if (Poche?.FirstOrDefault() is int id)
      {
        Poche.Remove(id);

        return id;
      }

      return 0;
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    /// <param name="cycle"></param>
    public void Mouvement(int cycle, Manette manette)
    {
      if (cycle++ % Vitesse == 0)
      {
        if (manette.Start)
        {
          Y++;

          X += (int)manette.AxisCX;

          if (manette.BtnA)
            Pieces = Rotate(Pieces.Rotation + 1);

          if (manette.BtnB)
            Pieces = Rotate(Pieces.Rotation + 3);
        }
        else
        {
          //Y est la position de la piece
          if (Y++ >= -1 || PieceTombes.Top < 10)
          {
            if (XOptimal > X && X + Pieces.Largeur < 11)
              X += 1;

            if (XOptimal < X)
              X -= 1;

            if (Pieces.Rotation < RotationOptimal)
              Pieces = Rotate(Pieces.Rotation + 1);
          }
        }
      }
    }

    /// <summary>
    /// EffacerLigne
    /// </summary>
    /// <returns></returns>
    public TetrisPieceList EffacerLigne()
    {
      int bonus = 1;
      TetrisPieceList tetrisPieces = new();

      for (int y = -3; y < 19; y++)
        if (PieceTombes.Count(p => p.Y == y) == 10)
        {
          Score += bonus;
          bonus *= 2;
          tetrisPieces.AddRange(PieceTombes.EffacerLigne(y));
        }

      return tetrisPieces;
    }

    /// <summary>
    /// Bottom
    /// </summary>
    public bool Bottom()
    {
      if (CheckBottom() && !PieceTombes.LignePleine)
        return SetPieceTombe();

      return false;
    }

    /// <summary>
    /// Rotate
    /// </summary>
    private TetrisPieceList Rotate(int rotation)
    {
      return new TetrisPieceList(TetrisPieceList.GetPiece(Pieces.PieceID, rotation));
    }

    /// <summary>
    /// RotateX
    /// </summary>
    /// <param name="rotation"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private TetrisPieceList? RotateX(int rotation, int x)
    {
      if (new TetrisPieceList(TetrisPieceList.GetPiece(Pieces.PieceID, rotation), x) is TetrisPieceList tetrisPieces)
        if (tetrisPieces.Largeur < 12)
          return tetrisPieces;

      return null;
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
    private bool SetPieceTombe()
    {
      foreach (TetrisPiece tetrisPiece in Pieces)
        PieceTombes.Add(new TetrisPiece(tetrisPiece, X, Y));

      return true;
    }
  }
}