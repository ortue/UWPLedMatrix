using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Series;

namespace AudioSpectrum
{

  public class MainViewModel
  {
    public MainViewModel()
    {
      this.MyModel = new PlotModel { Title = "Example 1" };




      //this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

      this.Points = new List<DataPoint>
                              {
                                  new DataPoint(0, 4),
                                  new DataPoint(10, 13),
                                  new DataPoint(20, 15),
                                  new DataPoint(30, 16),
                                  new DataPoint(40, 12),
                                  new DataPoint(50, 12)
                              };


    }

    public IList<DataPoint> Points { get; private set; }

    public PlotModel MyModel { get; private set; }
  }
}