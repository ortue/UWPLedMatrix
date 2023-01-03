using Library.Util;

namespace Library.Entity
{
  public class Pong
  {
    public double X { get; set; }
    public double Y { get; set; }
    public int ScoreP1 { get; set; }
    public int ScoreP2 { get; set; }
    public int Vitesse { get; set; }

    private double _xX;
    public double XX
    {
      get
      {
        if (_xX > 1.9d)
          _xX = 1.9d;

        if (_xX < -1.9d)
          _xX = -1.9d;

        return _xX;
      }
      set { _xX = value; }
    }

    private double _yY;
    public double YY
    {
      get
      {
        if (_yY > 1.9d)
          _yY = 1.9d;

        if (_yY < -1.9d)
          _yY = -1.9d;

        return _yY;
      }
      set { _yY = value; }
    }

    private double _pad1;
    public double Pad1
    {
      get
      {
        if (_pad1 < 3d)
          _pad1 = 3d;

        if (_pad1 > 16d)
          _pad1 = 16d;

        return _pad1;
      }
      set { _pad1 = value; }
    }

    private double _pad2;
    public double Pad2
    {
      get
      {
        if (_pad2 < 3d)
          _pad2 = 3d;

        if (_pad2 > 16d)
          _pad2 = 16d;

        return _pad2;
      }
      set { _pad2 = value; }
    }

    public double VitessePalette
    {
      get { return 0.6d - (40d - Vitesse) / 100d; }
    }

    public bool Gauche
    {
      get
      {
        if (X < 10d)
          return true;

        return false;
      }
    }

    public bool Droite
    {
      get
      {
        if (X > 10d)
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
          return Y > Pad2 + 3d || Y < Pad2 - 3d;

        if (Gauche)
          return Y > Pad1 + 3d || Y < Pad1 - 3d;

        return false;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Pong()
    {
      Random random = new();
      XX = random.Next(3, 12) / 10d;
      YY = random.Next(3, 12) / 10d;

      X = 10d;
      Y = 10d;

      Vitesse = 50;

      ScoreP1 = 0;
      ScoreP2 = 0;

      Pad1 = 9d;
      Pad2 = 9d;
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
          Random random = new();
          YY = (random.Next(0, 5) - 3) * 0.1d;
        }
      }

      return but;
    }

    /// <summary>
    /// Vérifier que la balle ne rebondisse pas plus d'une fois.
    /// </summary>
    /// <returns></returns>
    public double RebonPalette()
    {
      if (XX > 0d && X > 10d || XX < 0d && X < 10d)
        return XX * 2d;

      return 0d;
    }

    /// <summary>
    /// Quand on pogne le boute de la palette augmenter le yy
    /// </summary>
    /// <returns></returns>
    public double Deviation()
    {
      int facteur = 1;

      for (int i = -2; i < 3; i++)
      {
        if (Droite && (int)Math.Round(Y + i, 0) == (int)Math.Round(Pad2, 0))
          return facteur + 0.1 * -i;

        if (Gauche && (int)Math.Round(Y + i, 0) == (int)Math.Round(Pad1, 0))
          return facteur + 0.1 * -i;
      }

      return 0;
    }

    /// <summary>
    /// PositionPalette
    /// </summary>
    public void PositionPalette(Manette manette)
    {
      if (manette.Start)
      {
        Pad1 += manette.AxisAY;

        if (Math.Round(Pad2, 0) < Math.Round(Y, 0))
          Pad2 += VitessePalette;
        else if (Math.Round(Pad2, 0) > Math.Round(Y, 0))
          Pad2 -= VitessePalette;
      }
      else
      {
        if (X > 10d && XX > 0d)
        {
          if (Math.Round(Pad2, 0) < Math.Round(Y, 0))
            Pad2 += VitessePalette;
          else if (Math.Round(Pad2, 0) > Math.Round(Y, 0))
            Pad2 -= VitessePalette;
        }
        else if (X < 10d && XX < 0d)
        {
          if (Math.Round(Pad1, 0) < Math.Round(Y, 0))
            Pad1 += VitessePalette;
          else if (Math.Round(Pad1, 0) > Math.Round(Y, 0))
            Pad1 -= VitessePalette;
        }
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
        return X + XX > largeur - 3 || X + XX < 2d;

      return X + XX > largeur - 2 || X + XX < 1d;
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
        Random random = new();

        if (Droite)
        {
          but = true;
          X = 4;
          XX = -random.Next(3, 12) / 10;
          ScoreP1++;
        }
        else if (Gauche)
        {
          but = true;
          X = 14;
          XX = random.Next(3, 12) / 10;
          ScoreP2++;
        }

        Y = 10;
        YY = random.Next(3, 12) / 10;
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
