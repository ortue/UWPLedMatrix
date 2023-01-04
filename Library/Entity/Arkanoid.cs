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
        if (_xX > 1.9)
          _xX = 1.9;

        if (_xX < -1.9)
          _xX = -1.9;

        return _xX;
      }
      set { _xX = value; }
    }

    private double _yY;
    public double YY
    {
      get
      {
        if (_yY > 1.9)
          _yY = 1.9;

        if (_yY < -1.9)
          _yY = -1.99;

        return _yY;
      }
      set { _yY = value; }
    }

    private double _pad;
    public double Pad
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

    public double VitessePalette
    {
      get { return 0.6 - (40 - Vitesse) / 100; }
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
      XX = random.Next(2, 6) / 10;
      YY = random.Next(-8, -2) / 10;

      X = 10;
      Y = 17;

      Vitesse = 90;

      Pad = 10;
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
          return facteur + 0.1 * -i;

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
          XX = (random.Next(0, 5) - 3) * 0.1;
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
      if (X + XX >= PixelList.Largeur - 2 || X + XX < 1)
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

    /// <summary>
    /// Briques
    /// </summary>
    public bool CheckBrique()
    {
      bool check = false;

      if (Briques.Find(b => (b.X == (int)Math.Round(X + XX, 0) || b.XX == (int)Math.Round(X + XX, 0)) && b.Y == (int)Math.Round(Y + YY, 0) && b.Visible) is Brique brique)
      {
        brique.Visible = false;

        YY -= YY * 2;

        if (Briques.Find(b => (b.X == (int)Math.Round(X + XX, 0) || b.XX == (int)Math.Round(X + XX, 0)) && b.Y == (int)Math.Round(Y, 0) && b.Visible) != null)
          XX -= XX * 2;

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