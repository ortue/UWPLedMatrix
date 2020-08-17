using System.Drawing;

namespace LedLibrary.Entities
{
  public class AxisLine
  {
    public double Value;
    public float LineWidth;
    public Color LineColor;

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="Ypos"></param>
    /// <param name="lineWidth"></param>
    /// <param name="lineColor"></param>
    /// <param name="label"></param>
    public AxisLine(double Ypos, float lineWidth, Color lineColor)
    {
      Value = Ypos;
      LineWidth = lineWidth;
      LineColor = lineColor;
    }
  }
}