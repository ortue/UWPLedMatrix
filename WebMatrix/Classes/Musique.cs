using LedLibrary.Classes;
using LedLibrary.Entities;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System;
using System.Linq;
using WebMatrix.Context;

namespace WebMatrix.Classes
{
  public class Musique
  {
    /// <summary>
    /// Spectrum
    /// </summary>
    public static void Spectrum2(Criteria criteria)
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        byte[] fftData = new byte[audioBuffer.Length];
        double[] fft = new double[audioBuffer.Length];

        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * criteria.AmplitudeSpectrum;

        LomontFFT.TableFFT(fft, true);

        for (int i = 0; i < fft.Length; i += 2)
        {
          double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
          fftData[i] = (byte)fftmag;
          fftData[i + 1] = fftData[i];
        }

        int xx = 0;
        int step = (fftData.Length / 20);

        for (int x = 1; x < fftData.Length - step - 1; x += step)
        {
          double moyenne = 0;

          for (int i = x; i < x + step; i++)
            moyenne += fftData[i];

          double yMax = moyenne / step;

          for (int y = 0; y < 20; y++)
            if (y < Math.Ceiling(yMax))
              if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
                pixel.Set(y * 5, 0, (19 - y) * 5);

          xx++;
        }

        Util.SetLeds();

        for (int x = 0; x < 20; x++)
          if (Util.Context.Pixels.Where(p => p.Coord.X == x && !p.Couleur.IsNoir).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
            pixel.Couleur = Couleur.Noir;
      }
    }

    /// <summary>
    /// Graph
    /// </summary>
    public static void Graph1(Criteria criteria)
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        double[] fft = new double[audioBuffer.Length];
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * criteria.AmplitudeGraph;

        for (int x = 0; x < 20; x++)
        {
          int y = (int)(fft[x * 2]) + 10;
          byte red = (byte)Math.Abs(fft[x * 2] * 11);

          if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
            pixel.Set(red, 0, 127 - red);
        }

        Util.SetLeds();
        Util.Context.Pixels.Reset();
      }
    }

    /// <summary>
    /// Graph
    /// </summary>
    public static void Graph2(Criteria criteria)
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        double[] fft = new double[audioBuffer.Length];
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * criteria.AmplitudeGraph;

        for (int x = 0; x < 20; x++)
        {
          int y = (int)fft[x * 4] + 10;
          byte red = (byte)Math.Abs(fft[x * 4] * 11);

          if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
            pixel.Set(red, 0, 127 - red);
        }

        Util.SetLeds();
        Util.Context.Pixels.Reset();
      }
    }

    /// <summary>
    /// SpectrumGraph
    /// </summary>
    public static void SpectrumGraph(Criteria criteria)
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        double[] fft = new double[audioBuffer.Length];
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * criteria.AmplitudeGraph;

        for (int x = 0; x < 20; x++)
        {
          int y = (int)fft[x * 2] + 8;
          byte red = (byte)Math.Abs(fft[x * 2] * 11);

          if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
            pixel.Set(red, 0, 127 - red);
        }

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * criteria.AmplitudeSpectrum;

        byte[] fftData = new byte[fft.Length];

        LomontFFT.TableFFT(fft, true);

        for (int i = 0; i < fft.Length; i += 2)
        {
          double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
          fftData[i] = (byte)fftmag;
          fftData[i + 1] = fftData[i];
        }

        int xx = 0;
        int step = (fftData.Length / 20);

        for (int x = 1; x < fftData.Length - step - 1; x += step)
        {
          double moyenne = 0;

          for (int i = x; i < x + step; i++)
            moyenne += fftData[i];

          double yMax = moyenne / step;

          for (int y = 0; y < 20; y++)
            if (y < Math.Ceiling(yMax))
              if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
                if (pixel.Couleur.IsNoir)
                  pixel.Set(y * 5, (19 - y) * 5, 0);

          xx++;
        }

        Util.SetLeds();
        Util.Context.Pixels.Reset();
      }
    }
  }
}