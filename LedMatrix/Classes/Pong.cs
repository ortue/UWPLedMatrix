using System;

namespace LedMatrix.Classes
{
  public class Pong
  {
    public decimal X { get; set; }
    public decimal Y { get; set; }
    public decimal XX { get; set; }
    public decimal YY { get; set; }
    public int Vitesse { get; set; }
    public decimal Pad1 { get; set; }
    public decimal Pad2 { get; set; }
    public int ScoreP1 { get; set; }
    public int ScoreP2 { get; set; }

    public decimal VitessePalette
    {
      get { return (decimal)0.6 - (40 - Vitesse) / (decimal)100; }
    }

    public bool Gauche
    {
      get
      {
        if (X < 10)
          return true;

        return false;
      }
    }

    public bool Droite
    {
      get
      {
        if (X > 10)
          return true;

        return false;
      }
    }

    public int ScorePad
    {
      get
      {
        if (ScoreP1 > 9)
          return 1;

        return 5;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Pong()
    {
      Random random = new Random();
      XX = random.Next(3, 12) / (decimal)10;
      YY = random.Next(3, 12) / (decimal)10;

      X = 10;
      Y = 10;

      Vitesse = 40;

      ScoreP1 = 0;
      ScoreP2 = 0;

      Pad1 = 9;
      Pad2 = 9;
    }

    /// <summary>
    /// Quand on pogne le boute de la palette augmenter le yy
    /// </summary>
    public bool Palette(int largeur)
    {
      bool but = false;

      //Les deux murs horisontal
      if (X + XX > largeur - 2 || X + XX < 1)
      {
        if (Droite)
        {
          YY *= Deviation(Pad2);
          but = Score(2, Pad2);
        }
        else if (Gauche)
        {
          YY *= Deviation(Pad1);
          but = Score(1, Pad1);
        }

        XX -= (XX * 2);

        if (Vitesse > 0)
          Vitesse--;

        //Pour éviter que la balle soit en loop
        if (Math.Round(YY, 1) == 0)
        {
          Random random = new Random();
          YY = (random.Next(0, 5) - 3) * (decimal)0.1;
        }

        if (XX > (decimal)1.9)
          XX = (decimal)1.9;

        if (XX < (decimal)-1.9)
          XX = (decimal)-1.9;

        if (YY > (decimal)1.9)
          YY = (decimal)1.9;

        if (YY < (decimal)-1.9)
          YY = (decimal)-1.9;
      }

      return but;
    }

    /// <summary>
    /// Deviation
    /// </summary>
    /// <param name="pad"></param>
    /// <returns></returns>
    public decimal Deviation(decimal pad)
    {
      for (int i = -2; i < 3; i++)
        if ((int)Math.Round(Y + i, 0) == (int)Math.Round(pad, 0))
          return 1 + (decimal)0.1 * i;

      return 0;
    }

    /// <summary>
    /// PositionPalette
    /// </summary>
    public void PositionPalette()
    {
      if (X > 10 && XX > 0)
      {
        if (Math.Round(Pad2, 0) < Math.Round(Y, 0))
          Pad2 += VitessePalette;
        else if (Math.Round(Pad2, 0) > Math.Round(Y, 0))
          Pad2 -= VitessePalette;

        if (Pad2 < 3)
          Pad2 = 3;

        if (Pad2 > 16)
          Pad2 = 16;
      }
      else if (X < 10 && XX < 0)
      {
        if (Math.Round(Pad1, 0) < Math.Round(Y, 0))
          Pad1 += VitessePalette;
        else if (Math.Round(Pad1, 0) > Math.Round(Y, 0))
          Pad1 -= VitessePalette;

        if (Pad1 < 3)
          Pad1 = 3;

        if (Pad1 > 16)
          Pad1 = 16;
      }
    }

    /// <summary>
    /// Murs horizontaux
    /// </summary>
    /// <param name="hauteur"></param>
    public void Horizontal(int hauteur)
    {
      if (Y + YY >= hauteur - 2 || Y + YY < 1)
        YY -= (YY * 2);
    }

    /// <summary>
    /// ResetScore
    /// </summary>
    public bool Score(int cote, decimal pad)
    {
      bool but = false;

      if (Y > pad + 3 || Y < pad - 3)
      {
        Random random = new Random();

        if (cote == 2)
        {
          but = true;
          X = 4;
          XX = -random.Next(3, 12) / (decimal)10;
          ScoreP1++;
        }

        if (cote == 1)
        {
          but = true;
          X = 14;
          XX = random.Next(3, 12) / (decimal)10;
          ScoreP2++;
        }

        Y = 10;
        YY = random.Next(3, 12) / (decimal)10;
        Vitesse = 40;

        if (ScoreP1 > 99 || ScoreP2 > 99)
        {
          ScoreP1 = 0;
          ScoreP2 = 0;
        }
      }

      return but;
    }
  }
}