namespace LedLibrary.Entities
{
  public class Color
  {
    public byte A { get; set; }
    public byte B { get; set; }
    public byte G { get; set; }
    public byte R { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Red"></param>
    /// <param name="Green"></param>
    /// <param name="Blue"></param>
    /// <returns></returns>
    public static Color FromArgb(byte Red, byte Green, byte Blue)
    {
      return new Color { R = Red, G = Green, B = Blue };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public bool Egal(Color color)
    {
      return R == color.R && G == color.G && B == color.B;
    }
  }
}
