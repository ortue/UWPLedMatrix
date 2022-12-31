using Library.Collection;
using Library.Entity;

namespace Library.Util
{
  public class Manette
  {
    public double X { get; set; }
    public double Y { get; set; }

    public double AxisAX { get; set; }
    public double AxisAY { get; set; }

    public double AxisBX { get; set; }
    public double AxisBY { get; set; }

    public double AxisCX { get; set; }
    public double AxisCY { get; set; }

    public bool BtnA { get; set; }
    public bool BtnB { get; set; }
    public bool BtnX { get; set; }
    public bool BtnY { get; set; }

    public bool BtnL { get; set; }
    public bool BtnR { get; set; }
    public bool BtnZL { get; set; }
    public bool BtnZR { get; set; }

    public bool BtnMoins { get; set; }
    public bool BtnPlus { get; set; }

    public bool BtnAxisA { get; set; }
    public bool BtnAxisB { get; set; }

    public bool BtnH { get; set; }
    public bool BtnO { get; set; }

    public Pixel Pixel
    {
      get { return new Pixel { X = (int)Math.Round(X, 0), Y = (int)Math.Round(Y, 0) }; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Manette(int x, int y)
    {
      X = x;
      Y = y;
    }

    /// <summary>
    /// Set 
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="value">[0..32767]</param>
    public void Set(byte axis, double value)
    {
      _ = axis switch
      {
        0 => AxisAX = value,
        1 => AxisAY = value,

        2 => AxisBX = value,
        3 => AxisBY = value,

        4 => AxisCX = value,
        5 => AxisCY = value,

        _ => AxisAX = 0
      };
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="button"></param>
    /// <param name="pressed"></param>
    public void Set(byte button, bool pressed)
    {
      _ = button switch
      {
        0 => BtnA = pressed,
        1 => BtnB = pressed,

        2 => BtnX = pressed,
        3 => BtnY = pressed,

        4 => BtnL = pressed,
        5 => BtnR = pressed,

        6 => BtnZL = pressed,
        7 => BtnZR = pressed,

        8 => BtnMoins = pressed,
        9 => BtnPlus = pressed,

        10 => BtnAxisA = pressed,
        11 => BtnAxisB = pressed,

        12 => BtnH = pressed,
        13 => BtnO = pressed,

        _ => BtnA = false
      };
    }

    /// <summary>
    /// NextAxisA
    /// </summary>
    public void NextAxisA()
    {
      X += AxisAX;
      Y += AxisAY;

      if (X > PixelList.Largeur - 1)
        X = PixelList.Largeur - 1;

      if (Y > PixelList.Hauteur - 1)
        Y = PixelList.Hauteur - 1;

      if (X < 0)
        X = 0;

      if (Y < 0)
        Y = 0;
    }

    /// <summary>
    /// NextAxisA
    /// </summary>
    /// <param name="largeur"></param>
    /// <param name="hauteur"></param>
    public void NextAxisA(int largeur, int hauteur)
    {
      X += AxisAX;
      Y += AxisAY;

      if (X > largeur)
        X = largeur;

      if (Y > hauteur)
        Y = hauteur;

      if (X < 1)
        X = 1;

      if (Y < 1)
        Y = 1;
    }
  }
}