using System;

namespace WebMatrix.Classes
{
  public class Pong
  {
    public decimal X { get; set; }
    public decimal Y { get; set; }
    public int ScoreP1 { get; set; }
    public int ScoreP2 { get; set; }
    public int Vitesse { get; set; }

    private decimal _xX;
    public decimal XX
    {
      get
      {
        if (_xX > (decimal)1.9)
          _xX = (decimal)1.9;

        if (_xX < (decimal)-1.9)
          _xX = (decimal)-1.9;

        return _xX;
      }
      set { _xX = value; }
    }

    private decimal _yY;
    public decimal YY
    {
      get
      {
        if (_yY > (decimal)1.9)
          _yY = (decimal)1.9;

        if (_yY < (decimal)-1.9)
          _yY = (decimal)-1.9;

        return _yY;
      }
      set { _yY = value; }
    }

    private decimal _pad1;
    public decimal Pad1
    {
      get
      {
        if (_pad1 < 3)
          _pad1 = 3;

        if (_pad1 > 16)
          _pad1 = 16;

        return _pad1;
      }
      set { _pad1 = value; }
    }

    private decimal _pad2;
    public decimal Pad2
    {
      get
      {
        if (_pad2 < 3)
          _pad2 = 3;

        if (_pad2 > 16)
          _pad2 = 16;

        return _pad2;
      }
      set { _pad2 = value; }
    }

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

    public bool IsBut
    {
      get
      {
        if (Droite)
          return Y > Pad2 + 3 || Y < Pad2 - 3;

        if (Gauche)
          return Y > Pad1 + 3 || Y < Pad1 - 3;

        return false;
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
    /// Frontiere vertical
    /// </summary>
    public bool Palette(int largeur)
    {
      bool but = false;

      //Les deux murs horisontal
      if (Vertical(largeur))
      {
        YY *= Deviation();
        but = Score();
        XX -= RebonPalette();

        if (Vitesse > 0)
          Vitesse--;

        //Pour éviter que la balle soit en loop
        if (Math.Round(YY, 1) == 0)
        {
          Random random = new Random();
          YY = (random.Next(0, 5) - 3) * (decimal)0.1;
        }
      }

      return but;
    }

    /// <summary>
    /// Vérifier que la balle ne rebondisse pas plus d'une fois.
    /// </summary>
    /// <returns></returns>
    public decimal RebonPalette()
    {
      if (XX > 0 && X > 10 || XX < 0 && X < 10)
        return XX * 2;

      return 0;
    }

    /// <summary>
    /// Quand on pogne le boute de la palette augmenter le yy
    /// </summary>
    /// <param name="pad"></param>
    /// <returns></returns>
    public decimal Deviation()
    {
      int facteur = 1;

      for (int i = -2; i < 3; i++)
      {
        if (Droite && (int)Math.Round(Y + i, 0) == (int)Math.Round(Pad2, 0))
          return facteur + (decimal)0.1 * -i;

        if (Gauche && (int)Math.Round(Y + i, 0) == (int)Math.Round(Pad1, 0))
          return facteur + (decimal)0.1 * -i;
      }

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
      }
      else if (X < 10 && XX < 0)
      {
        if (Math.Round(Pad1, 0) < Math.Round(Y, 0))
          Pad1 += VitessePalette;
        else if (Math.Round(Pad1, 0) > Math.Round(Y, 0))
          Pad1 -= VitessePalette;
      }
    }

    /// <summary>
    /// Vertical
    /// </summary>
    /// <param name="largeur"></param>
    /// <returns></returns>
    public bool Vertical(int largeur)
    {
      if (!IsBut)
        return X + XX > largeur - 3 || X + XX < 2;

      return X + XX > largeur - 2 || X + XX < 1;
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
    public bool Score()
    {
      bool but = false;

      if (IsBut)
      {
        Random random = new Random();

        if (Droite)
        {
          but = true;
          X = 4;
          XX = -random.Next(3, 12) / (decimal)10;
          ScoreP1++;
        }
        else if (Gauche)
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