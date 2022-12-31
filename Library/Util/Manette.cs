namespace Library.Util
{
  public class Manette
  {
    public decimal AxisAX { get; set; }
    public decimal AxisAY { get; set; }

    public decimal AxisBX { get; set; }
    public decimal AxisBY { get; set; }

    public decimal AxisCX { get; set; }
    public decimal AxisCY { get; set; }

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

    //public decimal AxisX
    //{
    //  get { return new decimal[] { AxisAX, AxisBX, AxisCX }.Max(); }
    //}

    //public decimal AxisY
    //{
    //  get { return new decimal[] { AxisAY, AxisBY, AxisCY }.Max(); }
    //}

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="value"></param>
    public void Set(byte axis, decimal value)
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
  }
}