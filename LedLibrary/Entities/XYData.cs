using System.Drawing;

namespace LedLibrary.Entities
{
  public class XYData
  {
    public double[] Xs;
    public double[] Ys;
    public float LineWidth;
    public Color LineColor;
    public float MarkerSize;
    public Color MarkerColor;
    public string Label;

    public XYData(double[] Xs, double[] Ys, float lineWidth = 1, Color? lineColor = null, float markerSize = 3, Color? markerColor = null, string label = null)
    {
      Xs = Xs;
      Ys = Ys;

      LineWidth = lineWidth;
      MarkerSize = markerSize;
      Label = label;

      if (lineColor == null)
        lineColor = Color.Red;

      LineColor = (Color)lineColor;

      if (markerColor == null)
        markerColor = Color.Red;

      MarkerColor = (Color)markerColor;
    }
  }
}
