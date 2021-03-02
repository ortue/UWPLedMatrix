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
    //public static void Spectrum1()
    //{
    //  // Initialize the led strip
    //  Util.Setup();
    //  int task = Util.StartTask();
    //  byte[] audioBuffer = new byte[256];

    //  using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, 256);
    //  audioCapture.Start();

    //  while (Util.TaskWork(task))
    //  {
    //    byte[] fftData = new byte[256];
    //    double[] fft = new double[256];

    //    for (int i = 0; i < 256; i++)
    //    {
    //      audioBuffer[i] = 0;
    //      fftData[i] = 0;
    //      fft[i] = 0;
    //    }

    //    audioCapture.ReadSamples(audioBuffer, 256);
    //    float amplitude = 10.0f;

    //    for (int i = 0; i < 256; i++)
    //      fft[i] = (audioBuffer[i] - 128) * amplitude;

    //    LomontFFT fftTransoformer = new LomontFFT();
    //    fftTransoformer.TableFFT(fft, true);

    //    for (int i = 0; i < 256; i += 2)
    //    {
    //      double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
    //      fftData[i] = (byte)fftmag;
    //      fftData[i + 1] = fftData[i];
    //    }

    //    double facteur = 13.42;
    //    for (int x = 0; x < 20; x++)
    //      for (int y = 0; y < 20; y++)
    //        if (fftData[(int)(x * facteur)] > (255 / 30 * (20 - y)))
    //          if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
    //            pixel.Set(0 + (20 - y) * 5, 0, 127);

    //    Util.SetLeds();

    //    for (int x = 0; x < 20; x++)
    //      if (Util.Context.Pixels.Where(p => p.Coord.X == x && !p.Couleur.IsNoir).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
    //        pixel.Couleur = Couleur.Noir;
    //  }
    //}

    /// <summary>
    /// Spectrum
    /// </summary>
    //public static void Spectrum2()
    //{
    //  // Initialize the led strip
    //  Util.Setup();
    //  int task = Util.StartTask();
    //  byte[] audioBuffer = new byte[256];
    //  using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, 256);
    //  audioCapture.Start();

    //  while (Util.TaskWork(task))
    //  {
    //    byte[] fftData = new byte[256];
    //    double[] fft = new double[256];
    //    float amplitude = 26.0f;
    //    LomontFFT fftTransoformer = new LomontFFT();
    //    audioCapture.ReadSamples(audioBuffer, 256);

    //    for (int i = 0; i < 256; i++)
    //      fft[i] = (audioBuffer[i] - 128) * amplitude;

    //    fftTransoformer.TableFFT(fft, true);

    //    for (int i = 0; i < 256; i += 2)
    //    {
    //      double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
    //      fftData[i] = (byte)fftmag;
    //      fftData[i + 1] = fftData[i];
    //    }

    //    int xx = 0;

    //    for (int x = 1; x < 256 - 11; x += 12)
    //    {
    //      double facteur = 13.42;
    //      double yMax = (double)(new double[]
    //      {
    //        fftData[x] / facteur,
    //        fftData[x + 1] / facteur,
    //        fftData[x + 2] / facteur,
    //        fftData[x + 3] / facteur,
    //        fftData[x + 4] / facteur,
    //        fftData[x + 5] / facteur,
    //        fftData[x + 6] / facteur,
    //        fftData[x + 7] / facteur,
    //        fftData[x + 8] / facteur,
    //        fftData[x + 9] / facteur,
    //        fftData[x + 10] / facteur,
    //        fftData[x + 11] / facteur
    //      }).Average();

    //      for (int y = 0; y < 20; y++)
    //        if (y < Math.Ceiling(yMax))
    //          if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
    //            pixel.Set(y * 5, 0, (19 - y) * 5);

    //      xx++;
    //    }

    //    Util.SetLeds();

    //    for (int x = 0; x < 20; x++)
    //      if (Util.Context.Pixels.Where(p => p.Coord.X == x && !p.Couleur.IsNoir).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
    //        pixel.Couleur = Couleur.Noir;
    //  }
    //}

    ///// <summary>
    ///// Spectrum
    ///// </summary>
    //public static void Spectrum2()
    //{
    //  // Initialize the led strip
    //  Util.Setup();
    //  int task = Util.StartTask();
    //  byte[] audioBuffer = new byte[256];
    //  using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, 256);
    //  audioCapture.Start();

    //  while (Util.TaskWork(task))
    //  {
    //    byte[] fftData = new byte[256];
    //    double[] fft = new double[256];

    //    float amplitude = 1f;

    //    LomontFFT fftTransoformer = new LomontFFT();
    //    audioCapture.ReadSamples(audioBuffer, 256);

    //    for (int i = 0; i < 256; i++)
    //      fft[i] = (audioBuffer[i] - 128) * amplitude;

    //    fftTransoformer.TableFFT(fft, true);

    //    for (int i = 0; i < 256; i += 2)
    //    {
    //      double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
    //      fftData[i] = (byte)fftmag;
    //      fftData[i + 1] = fftData[i];
    //    }

    //    int xx = 0;

    //    for (int x = 1; x < 256 - 11; x += 12)
    //    {
    //      double yMax = (double)(new double[]
    //      {
    //        fftData[x],
    //        fftData[x + 1],
    //        fftData[x + 2],
    //        fftData[x + 3],
    //        fftData[x + 4],
    //        fftData[x + 5],
    //        fftData[x + 6],
    //        fftData[x + 7],
    //        fftData[x + 8],
    //        fftData[x + 9],
    //        fftData[x + 10],
    //        fftData[x + 11]
    //      }).Average();

    //      for (int y = 0; y < 20; y++)
    //        if (y < Math.Ceiling(yMax))
    //          if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
    //            pixel.Set(y * 5, 0, (19 - y) * 5);

    //      xx++;
    //    }

    //    Util.SetLeds();

    //    for (int x = 0; x < 20; x++)
    //      if (Util.Context.Pixels.Where(p => p.Coord.X == x && !p.Couleur.IsNoir).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
    //        pixel.Couleur = Couleur.Noir;
    //  }
    //}

    /// <summary>
    /// Graph
    /// </summary>
    //public static void Graph1()
    //{
    //  // Initialize the led strip
    //  Util.Setup();
    //  int task = Util.StartTask();
    //  byte[] audioBuffer = new byte[256];
    //  using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, 256);
    //  audioCapture.Start();

    //  while (Util.TaskWork(task))
    //  {
    //    double[] fft = new double[256];
    //    audioCapture.ReadSamples(audioBuffer, 256);

    //    //TODO:Ajuster l'amplitude en fonction du volume, avec le audioBuffer.Max(), 64=7 128=5 256=3, genre
    //    float amplitude = 5;

    //    if (audioBuffer.Max() < 96)
    //      amplitude = 7;

    //    if (audioBuffer.Max() > 160)
    //      amplitude = 3;

    //    for (int i = 0; i < 256; i++)
    //      fft[i] = (audioBuffer[i] - 128) * amplitude;

    //    for (int x = 0; x < 20; x++)
    //    {
    //      double facteur = 13.42;
    //      int y = (int)(fft[(int)(x * facteur)] / facteur) + 10;
    //      byte red = (byte)Math.Abs(fft[(int)(x * facteur)]);

    //      if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
    //        pixel.Set(red, 0, 127 - red);
    //    }

    //    Util.SetLeds();
    //    Util.Context.Pixels.Reset();
    //  }
    //}


    /// <summary>
    /// Spectrum
    /// </summary>
    public static void Spectrum2()
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

        float amplitude = 1f;

        LomontFFT fftTransoformer = new LomontFFT();
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        fftTransoformer.TableFFT(fft, true);

        for (int i = 0; i < fft.Length; i += 2)
        {
          double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
          fftData[i] = (byte)fftmag;
          fftData[i + 1] = fftData[i];
        }

        int xx = 0;
        int step = (fftData.Length / 20);

        for (int x = 1; x < fftData.Length - step-1; x += step)
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
    public static void Graph1()
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
        float amplitude = 0.4f;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

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
    public static void Graph2()
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
        float amplitude = 0.4f;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

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
    public static void SpectrumGraph()
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
        float amplitude = 0.4f;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        for (int x = 0; x < 20; x++)
        {
          int y = (int)fft[x * 2] + 8;
          byte red = (byte)Math.Abs(fft[x * 2] * 11);

          if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
            pixel.Set(red, 0, 127 - red);
        }

        amplitude = 0.8f;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        byte[] fftData = new byte[fft.Length];
        LomontFFT fftTransoformer = new LomontFFT();
        fftTransoformer.TableFFT(fft, true);

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







    /// <summary>
    /// Spectrum
    /// </summary>
    public static void Spectrum1()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, 256);
      audioCapture.Start();

      byte[] audioBuffer = new byte[256];

      while (Util.TaskWork(task))
      {
        //byte[] fftData = new byte[audioBuffer.Length];
        double[] fft = new double[audioBuffer.Length];

        float amplitude = 0.5f;

        LomontFFT fftTransoformer = new LomontFFT();
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        //fftTransoformer.TableFFT(fft, true);

        Complex[] complex =Transform.FFT(fft);

        //for (int i = 0; i < audioBuffer.Length; i += 2)
        //{
        //  double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
        //  fftData[i] = (byte)fftmag;
        //  fftData[i + 1] = fftData[i];
        //}

        int xx = 0;
        int step = (complex.Length / 20);
        for (int x = 0; x < complex.Length - step - 1; x += step)
        {
          double moyenne = 0;

          for (int i = x; i < x + step; i++)
            moyenne += complex[i].Magnitude;

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
  }
}