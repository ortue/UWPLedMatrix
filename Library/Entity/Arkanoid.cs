using Library.Collection;

namespace Library.Entity
{
  public class Arkanoid
  {
    public double X { get; set; }
    public double Y { get; set; }
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
          _yY = -1.99d;

        return _yY;
      }
      set { _yY = value; }
    }

    private double _pad;
    public double Pad
    {
      get
      {
        if (_pad < 2d)
          _pad = 2d;

        if (_pad > 17d)
          _pad = 17d;

        return _pad;
      }
      set { _pad = value; }
    }

    public double VitessePalette
    {
      get { return 0.6d - (40d - Vitesse) / 100d; }
    }

    public bool IsMort
    {
      get { return X > Pad + 2 || X < Pad - 2; }
    }

    public BriqueList Briques { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public Arkanoid()
    {
      Reset();

      Briques = new BriqueList();
    }

    /// <summary>
    /// Reset
    /// </summary>
    public void Reset()
    {
      Random random = new();
      XX = random.Next(2, 6) / 10d;
      YY = random.Next(-8, -2) / 10d;

      X = 10d;
      Y = 17d;

      Vitesse = 90;

      Pad = 10d;
    }

    /// <summary>
    /// Frontiere
    /// </summary>
    /// <param name="largeur"></param>
    /// <returns></returns>
    public bool Frontiere()
    {
      if (!IsMort)
        return Y + YY > PixelList.Hauteur - 3;

      return Y + YY > PixelList.Hauteur - 2;
    }

    /// <summary>
    /// Quand on pogne le boute de la palette augmenter le xx
    /// </summary>
    /// <returns></returns>
    public double Deviation()
    {
      int facteur = 1;

      for (int i = -1; i < 2; i++)
        if ((int)Math.Round(X + i, 0) == (int)Math.Round(Pad, 0))
          return facteur + 0.1d * -i;

      return 0;
    }

    /// <summary>
    /// Palette
    /// </summary>
    /// <returns></returns>
    public bool Palette()
    {
      bool mort = false;

      if (Frontiere())
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
          XX = (random.Next(0, 5) - 3) * 0.1d;
        }
      }

      return mort;
    }

    /// <summary>
    /// RebonPalette
    /// </summary>
    /// <returns></returns>
    private double RebonPalette()
    {
      return YY * 2;
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
        mort = true;
        Reset();
      }

      return mort;
    }

    /// <summary>
    /// Mure
    /// </summary>
    public void Mure()
    {
      if (X + XX >= PixelList.Largeur - 2 || X + XX < 1d)
        XX -= XX * 2d;

      if (Y + YY < 1d)
        YY -= YY * 2d;
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

    /// <summary>
    /// Briques
    /// </summary>
    public bool CheckBrique()
    {
      bool check = false;

      if (Briques.Find(b => (b.X == (int)Math.Round(X + XX, 0) || b.XX == (int)Math.Round(X + XX, 0)) && b.Y == (int)Math.Round(Y + YY, 0) && b.Visible) is Brique brique)
      {
        brique.Visible = false;

        YY -= YY * 2d;

        if (Briques.Find(b => (b.X == (int)Math.Round(X + XX, 0) || b.XX == (int)Math.Round(X + XX, 0)) && b.Y == (int)Math.Round(Y, 0) && b.Visible) != null)
          XX -= XX * 2d;

        check = true;
      }

      if (check && Briques.Find(b => b.Visible) == null)
      {
        Reset();
        Briques = new BriqueList();

        return true;
      }

      return false;
    }
  }
}