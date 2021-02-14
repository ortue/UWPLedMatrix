using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMatrix.Classes
{
  public class Tetris
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Score { get; set; }
    public int XOptimal { get; set; }
    public int LigneAnimation { get; set; }
    public int RotationOptimal { get; set; }
    public List<int> Poche { get; set; }
    public TetrisPieceList Nexts { get; set; }
    public TetrisPieceList Pieces { get; set; }
    public TetrisPieceList PieceTombes { get; set; }

    public bool Mort
    {
      get { return PieceTombes.Top < -2; }
    }

    public double Vitesse
    {
      get
      {
        if (10 - Score / 10 > 2)
          return 10 - Score / 10;

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
        TetrisPieceList centaines = new TetrisPieceList();

        for (int y = 0; y < Score / 100; y++)
          centaines.Add(new TetrisPiece { X = 13, Y = 7 + y });

        return centaines;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Tetris()
    {
      PieceTombes = new TetrisPieceList();

      NouvellePiece(1);
    }

    /// <summary>
    /// NouvellePiece
    /// </summary>
    /// <param name="version"></param>
    public void NouvellePiece(int version)
    {
      Y = -3;
      X = 6;

      SetPoche();

      if (Nexts == null)
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(GetNext()));
      else
        Pieces = new TetrisPieceList(TetrisPieceList.GetPiece(Nexts.PieceID));

      Nexts = new TetrisPieceList(TetrisPieceList.GetPiece(GetNext()));

      TetrisHorizontalList tetrisHorizontals = new TetrisHorizontalList();

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
    /// NouvellePiece
    /// </summary>
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

      TetrisHorizontalList tetrisHorizontals = new TetrisHorizontalList();
      TetrisHorizontalList tmpTetrisHorizontals = new TetrisHorizontalList();

      for (int rotation = 0; rotation < 4; rotation++)
      {
        TetrisPieceList tmpPiece = Rotate(rotation);
        tmpTetrisHorizontals = PieceTombes.HorizontalScore(tmpPiece);

        if (!tetrisHorizontals.Any())
          tetrisHorizontals = tmpTetrisHorizontals;

        if (tmpTetrisHorizontals.Max(t => t.Score) > tetrisHorizontals.Max(t => t.Score))
          tetrisHorizontals = tmpTetrisHorizontals;
      }

      XOptimal = tetrisHorizontals.ScoreX;
      RotationOptimal = tetrisHorizontals.ScoreRotation;
    }

    /// <summary>
    /// SetPoche
    /// </summary>
    private void SetPoche()
    {
      Random r = new Random();

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
      int id = Poche.FirstOrDefault();
      Poche.Remove(id);

      return id;
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
          if (XOptimal > X && X + Pieces.Largeur < 11)
            X += 1;

          if (XOptimal < X)
            X -= 1;

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
            bonus *= 2;

            PieceTombes.EffacerLigne(y);
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
    private TetrisPieceList RotateX(int rotation, int x)
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