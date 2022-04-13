using System;

namespace WebMatrix.Classes
{
  public class Arkanoid
  {
    public decimal X { get; set; }
    public decimal Y { get; set; }
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

    private decimal _pad;
    public decimal Pad
    {
      get
      {
        if (_pad < 2)
          _pad = 2;

        if (_pad > 17)
          _pad = 17;

        return _pad;
      }
      set { _pad = value; }
    }

    public decimal VitessePalette
    {
      get { return (decimal)0.6 - (40 - Vitesse) / (decimal)100; }
    }

    public bool IsMort
    {
      get { return X > Pad + 2 || X < Pad - 2; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Arkanoid()
    {
      Random random = new();
      XX = random.Next(3, 12) / (decimal)10;
      YY = random.Next(-12, -3) / (decimal)10;

      X = 10;
      Y = 17;

      Vitesse = 50;

      Pad = 10;
    }

    /// <summary>
    /// Frontiere
    /// </summary>
    /// <param name="largeur"></param>
    /// <returns></returns>
    public bool Frontiere(int hauteur)
    {
      if (!IsMort)
        return Y + YY > hauteur - 3;

      return Y + YY > hauteur - 2;
    }

    /// <summary>
    /// Quand on pogne le boute de la palette augmenter le xx
    /// </summary>
    /// <returns></returns>
    public decimal Deviation()
    {
      int facteur = 1;

      for (int i = -1; i < 2; i++)
        if ((int)Math.Round(X + i, 0) == (int)Math.Round(Pad, 0))
          return facteur + (decimal)0.1 * -i;

      return 0;
    }

    /// <summary>
    /// Palette
    /// </summary>
    /// <param name="hauteur"></param>
    /// <returns></returns>
    public bool Palette(int hauteur)
    {
      bool mort = false;

      if (Frontiere(hauteur))
      {
        mort = Mort();
        YY -= RebonPalette();
        XX *= Deviation();

        if (Vitesse > 0)
          Vitesse--;

        //Pour éviter que la balle soit en loop
        if (Math.Round(XX, 1) == 0)
        {
          Random random = new();
          XX = (random.Next(0, 5) - 3) * (decimal)0.1;
        }
      }

      return mort;
    }

    /// <summary>
    /// RebonPalette
    /// </summary>
    /// <returns></returns>
    private decimal RebonPalette()
    {
      return YY * 2;

      //return 0;
    }

    /// <summary>
    /// Mort
    /// </summary>
    /// <returns></returns>
    private bool Mort()
    {
      bool mort = false;

      if (IsMort)
      {
        Random random = new();

        mort = true;

        X = 10;
        Y = 15;

        XX = random.Next(3, 12) / (decimal)10;
        YY = random.Next(-12, -3) / (decimal)10;

        Vitesse = 40;
      }

      return mort;
    }

    /// <summary>
    /// Mure
    /// </summary>
    /// <param name="hauteur"></param>
    /// <param name="largeur"></param>
    public void Mure(int hauteur, int largeur)
    {
      if (X + XX >= largeur - 2 || X + XX < 1)
        XX -= XX * 2;

      if (Y + YY < 1)
        YY -= YY * 2;
    }

    /// <summary>
    /// PositionPalette
    /// </summary>
    public void PositionPalette()
    {
      if (Y > 10 && YY > 0)
      {
        if (Math.Round(Pad, 0) < Math.Round(X, 0))
          Pad += VitessePalette;
        else if (Math.Round(Pad, 0) > Math.Round(X, 0))
          Pad -= VitessePalette;
      }
    }
  }
}