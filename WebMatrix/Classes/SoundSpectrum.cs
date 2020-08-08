using Accord.Math;
using LedLibrary.Entities;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace WebMatrix.Classes
{
  public class SoundSpectrum
  {
    public BufferedWaveProvider bwp;

    //TODO:lier avec les coinfiguration de led
    public Figure fig = new Figure(20, 20);


    private List<XYData> XYDataList = new List<XYData>();
    private List<AxisLine> HLines = new List<AxisLine>();
    private List<AxisLine> VLines = new List<AxisLine>();


    private int Rate = 44100; // sample rate of the sound card
    private int BufferSize = (int)Math.Pow(2, 11); // must be a multiple of 2
    private List<SignalData> SignalDataList = new List<SignalData>();

    public void AudioDataAvailable(object sender, WaveInEventArgs e)
    {
      bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
    }

    public void StartListeningToMicrophone(int audioDeviceNumber = 0)
    {
      WaveIn wi = new WaveIn
      {
        DeviceNumber = audioDeviceNumber,
        WaveFormat = new WaveFormat(Rate, 1),
        BufferMilliseconds = (int)((double)BufferSize / Rate * 1000.0)
      };

      wi.DataAvailable += new EventHandler<WaveInEventArgs>(AudioDataAvailable);

      bwp = new BufferedWaveProvider(wi.WaveFormat)
      {
        BufferLength = BufferSize * 2,
        DiscardOnBufferOverflow = true
      };

      try
      {
        wi.StartRecording();
      }
      catch
      {
        string msg = "Could not record from audio device!\n\n";
        msg += "Is your microphone plugged in?\n";
        msg += "Is it set as your default recording device?";

      }
    }

    public void PlotLatestData()
    {
      // check the incoming microphone audio
      int frameSize = BufferSize;
      byte[] audioBytes = new byte[frameSize];
      bwp.Read(audioBytes, 0, frameSize);

      // return if there's nothing new to plot
      if (audioBytes.Length == 0)
        return;

      if (audioBytes[frameSize - 2] == 0)
        return;

      // incoming data is 16-bit (2 bytes per audio point)
      int bytesPerPoint = 2;

      // create a (32-bit) int array ready to fill with the 16-bit data
      int graphPointCount = audioBytes.Length / bytesPerPoint;

      // create double arrays to hold the data we will graph
      double[] pcm = new double[graphPointCount];
      double[] fft = new double[graphPointCount];
      double[] fftReal = new double[graphPointCount / 2];

      // populate Xs and Ys with double data
      for (int i = 0; i < graphPointCount; i++)
      {
        // read the int16 from the two bytes
        short val = BitConverter.ToInt16(audioBytes, i * 2);

        // store the value in Ys as a percent (+/- 100% = 200%)
        pcm[i] = val / Math.Pow(2, 16) * 200.0;
      }

      // calculate the full FFT
      fft = FFT(pcm);

      // determine horizontal axis units for graphs
      double pcmPointSpacingMs = Rate / 1000;
      double fftMaxFreq = Rate / 2;
      double fftPointSpacingHz = fftMaxFreq / graphPointCount;

      // just keep the real half (the other half imaginary)
      Array.Copy(fft, fftReal, fftReal.Length);

      // plot the Xs and Ys for both graphs
      //scottPlotUC1.Clear();
      //scottPlotUC1.PlotSignal(pcm, pcmPointSpacingMs, Color.Blue);
      //scottPlotUC2.Clear();

      //TODO:Instantier l'objet SignalData ici pour le passer en parametre

      PlotSignal(fftReal, fftPointSpacingHz, Color.Blue);

      // optionally adjust the scale to automatically fit the data
      //if (needsAutoScaling)
      //{
      //scottPlotUC1.AxisAuto();
      //scottPlotUC2.AxisAuto();
      //needsAutoScaling = false;
      //}

      //scottPlotUC1.PlotSignal(Ys, RATE);

      //numberOfDraws += 1;
      //lblStatus.Text = $"Analyzed and graphed PCM and FFT data {numberOfDraws} times";

      // this reduces flicker and helps keep the program responsive
      //Application.DoEvents();

    }

    public double[] FFT(double[] data)
    {
      double[] fft = new double[data.Length];

      Complex[] fftComplex = new Complex[data.Length];

      for (int i = 0; i < data.Length; i++)
        fftComplex[i] = new Complex(data[i], 0.0);

      FourierTransform.FFT(fftComplex, FourierTransform.Direction.Forward);

      for (int i = 0; i < data.Length; i++)
        fft[i] = fftComplex[i].Magnitude;

      return fft;
    }



    public void PlotSignal(double[] values, double sampleRate, Color? color = null, double offsetX = 0, double offsetY = 0)
    {
      SignalDataList.Add(new SignalData(values, sampleRate, lineColor: color, offsetX: offsetX, offsetY: offsetY));
      fig.GraphClear();
      Render();
    }

    private void Render(bool redrawFrame = false)
    {
      //fig.BenchmarkThis(showBenchmark);

      if (redrawFrame)
        fig.FrameRedraw();
      else
        fig.GraphClear();

      // plot XY points
      foreach (XYData xyData in XYDataList)
      {
        fig.PlotLines(xyData.Xs, xyData.Ys, xyData.LineWidth, xyData.LineColor);
        fig.PlotScatter(xyData.Xs, xyData.Ys, xyData.MarkerSize, xyData.MarkerColor);
      }

      // plot signals
      foreach (SignalData signalData in SignalDataList)
        fig.PlotSignal(signalData.Values, signalData.XSpacing, signalData.OffsetX, signalData.OffsetY, signalData.LineWidth, signalData.LineColor);

      // plot axis lines
      foreach (AxisLine axisLine in HLines)
      {
        fig.PlotLines(
            new double[] { fig.xAxis.Min, fig.xAxis.Max },
            new double[] { axisLine.Value, axisLine.Value },
            axisLine.LineWidth,
            axisLine.LineColor
            );
      }

      foreach (AxisLine axisLine in VLines)
      {
        fig.PlotLines(
            new double[] { axisLine.Value, axisLine.Value },
            new double[] { fig.yAxis.Min, fig.yAxis.Max },
            axisLine.LineWidth,
            axisLine.LineColor
            );
      }

      //pictureBox1.Image = fig.Render();
    }
  }
}
