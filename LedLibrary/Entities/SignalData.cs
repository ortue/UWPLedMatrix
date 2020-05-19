using System.Drawing;

namespace LedLibrary.Entities
{
  public class SignalData
  {
    public double[] Values;
    public double SampleRate;
    public double XSpacing;
    public double OffsetX;
    public double OffsetY;
    public float LineWidth;
    public Color LineColor;
    public string Label;

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="values"></param>
    /// <param name="sampleRate"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineWidth"></param>
    /// <param name="label"></param>
    public SignalData(double[] values, double sampleRate, double offsetX = 0, double offsetY = 0, Color? lineColor = null, float lineWidth = 1, string label = null)
    {
      Values = values;
      SampleRate = sampleRate;
      XSpacing = 1.0 / sampleRate;
      OffsetX = offsetX;
      OffsetY = offsetY;
      LineColor = (Color)lineColor;
      LineWidth = lineWidth;
      Label = label;

      if (lineColor == null)
        LineColor = Color.Red;

    }
  }
}
