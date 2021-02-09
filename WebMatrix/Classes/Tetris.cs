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
    public int LigneAnimation { get; set; }
    public int RotationOptimal { get; set; }
    public TetrisPieceList Nexts { get; set; }
    public TetrisPieceList Pieces { get; set; }
    public TetrisPieceList PieceTombes { get; set; }
    public TetrisHorizontalList TetrisHorizontals { get; set; }

    public double Vitesse
    {
      get
      {
        if (10 - Score / 3 > 2)
          return 10 - Score / 3;

        return 2;
      }
      //get { return 1; }
    }

    public string ScoreUn
    {
      get { return Score.ToString().Substring(0, 1); }
    }

    public string ScoreDeux
    {
      get
      {
        if (Score > 9)
          return Score.ToString().Substring(1, 1);

        return string.Empty;
      }
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
      RotationOptimal = 0;

      Random r = new Random();

      if (Nexts == null)
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(r.Next(0, 7)));
      else
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(Nexts.PieceID));

      Nexts = new TetrisPieceList(TetrisPieceList.GetPiece(r.Next(0, 7)));

      TetrisHorizontals = new TetrisHorizontalList();
      TetrisHorizontalList tetrisHorizontals = new TetrisHorizontalList();
      TetrisHorizontalList tmpTetrisHorizontals = new TetrisHorizontalList();

      for (int rotation = 0; rotation < 4; rotation++)
      {
        TetrisPieceList tmpPiece = Rotate(rotation);
        tmpTetrisHorizontals = PieceTombes.HorizontalScore(tmpPiece);

        if (!tetrisHorizontals.Any())
          tetrisHorizontals = tmpTetrisHorizontals;

        if (tmpTetrisHorizontals.Max(t => t.Score) > tetrisHorizontals.Max(t => t.Score))
        {
          RotationOptimal = rotation;
          tetrisHorizontals = tmpTetrisHorizontals;
        }
      }

      TetrisHorizontals = tetrisHorizontals;
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    /// <param name="cycle"></param>
    public void Mouvement(int cycle)
    {
      if (cycle++ % Vitesse == 0 && LigneAnimation == 0)
      {
        //Y est la position de la piece
        if (Y++ >= -1 || PieceTombes.Top < 10)
        {
          if (TetrisHorizontals.ScoreX > X && X + Pieces.Largeur < 11)
            X += 1;

          if (TetrisHorizontals.ScoreX < X)
            X -= 1;

          //TODO:Verifier pourquoi des fois les pieces ont l'air de rotater trop vite
          if (Pieces.Rotation < RotationOptimal)
            Pieces = Rotate(Pieces.Rotation + 1);
        }
      }
    }

    /// <summary>
    /// EffacerLigne
    /// </summary>
    public void EffacerLigne()
    {
      if (LigneAnimation < 20)
      {
        bool animation = false;

        for (int y = 0; y < 19; y++)
          if (PieceTombes.Count(p => p.Y == y) == 10)
          {
            animation = true;
            PieceTombes.AnimationEffacerLigne(y, LigneAnimation % 2 == 0);
          }

        if (animation)
          LigneAnimation++;
      }
      else
      {
        int bonus = 1;

        for (int y = 0; y < 19; y++)
          if (PieceTombes.Count(p => p.Y == y) == 10)
          {
            Score += bonus;
            PieceTombes.EffacerLigne(y);
            bonus *= 2;
          }

        LigneAnimation = 0;
      }
    }

    /// <summary>
    /// Bottom
    /// </summary>
    public bool Bottom()
    {
      if (CheckBottom() && !PieceTombes.LignePleine)
      {
        if (Y < -2)
          return true;

        SetPieceTombe();
        NouvellePiece();
      }

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