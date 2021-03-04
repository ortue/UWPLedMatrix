using LedLibrary.Classes;
using LedLibrary.Collection;
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

    /// <summary>
    /// VuMeter
    /// </summary>
    /// <param name="criteria"></param>
    public static void VuMeter()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      CaractereList caracteres = new CaractereList(20);
      double max = 0;

      while (Util.TaskWork(task))
      {
        max -= 1;
        double[] fft = new double[audioBuffer.Length];
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * 0.703125;

        if (fft.Max(a => Math.Abs(a)) > max)
          max = fft.Max(a => Math.Abs(a));

        foreach (Pixel pixel in Util.Context.Pixels)
          pixel.Set(127, 127, 127);

        caracteres.SetText("VU");
        Util.Context.Pixels.Print(caracteres.GetCaracteres(), 5, 12, Couleur.Noir);

        Couleur couleurMax = Couleur.Noir;

        //lumiere max
        if (max > 80)
          couleurMax = Couleur.Get(127, 0, 0);

        Util.Context.Pixels.GetCoordonnee(16, 2).SetColor(couleurMax);
        Util.Context.Pixels.GetCoordonnee(17, 2).SetColor(couleurMax);
        Util.Context.Pixels.GetCoordonnee(16, 3).SetColor(couleurMax);
        Util.Context.Pixels.GetCoordonnee(17, 3).SetColor(couleurMax);

        //dessin



        //base
        Util.Context.Pixels.GetCoordonnee(8, 18).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(9, 18).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(10, 18).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(11, 18).SetColor(Couleur.Noir);

        Util.Context.Pixels.GetCoordonnee(7, 19).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(8, 19).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(9, 19).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(10, 19).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(11, 19).SetColor(Couleur.Noir);
        Util.Context.Pixels.GetCoordonnee(12, 19).SetColor(Couleur.Noir);

        //aiguille
        for (int r = 2; r < 18; r++)
          Util.Context.Pixels.GetCoordonnee(GetCercleCoord(max + 315, r)).SetColor(Couleur.Noir);

        Util.SetLeds();
        Util.Context.Pixels.Reset();
      }
    }

    /// <summary>
    ///GetCercleCoord
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    private static Coordonnee GetCercleCoord(double degree, int rayon)
    {
      Coordonnee coord = new Coordonnee(20, 20);

      if (degree % 360 >= 0 && degree % 360 <= 180)
        coord.X = 10 + (int)(rayon * Math.Sin(Math.PI * degree / 180));
      else
        coord.X = 10 - (int)(rayon * -Math.Sin(Math.PI * degree / 180)) - 1;

      coord.Y = 19 - (int)(rayon * Math.Cos(Math.PI * degree / 180) + 0.5);

      return coord.CheckCoord();
    }
  }
}