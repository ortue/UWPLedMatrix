namespace LedLibrary.Entities
{
  public class Tick
  {
    public double PosUnit { get; set; }
    public int PosPixel { get; set; }
    public double SpanUnits { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="value"></param>
    /// <param name="pixel"></param>
    /// <param name="axisSpan"></param>
    public Tick(double value, int pixel, double axisSpan)
    {
      PosUnit = value;
      PosPixel = pixel;
      SpanUnits = axisSpan;
    }

    public string Label
    {
      get
      {
        if (SpanUnits < .01) 
          return string.Format("{0:0.0000}", PosUnit);

        if (SpanUnits < .1) 
          return string.Format("{0:0.000}", PosUnit);

        if (SpanUnits < 1) 
          return string.Format("{0:0.00}", PosUnit);

        if (SpanUnits < 10) 
          return string.Format("{0:0.0}", PosUnit);

        return string.Format("{0:0}", PosUnit);
      }
    }
  }
}
