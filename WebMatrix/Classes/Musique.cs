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

        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * 0.78125;

        double max = fft.Max(a => Math.Abs(a));
        double amplitude = ((101 - max) / 100) * 2.5;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        LomontFFT.TableFFT(fft, true);

        for (int i = 0; i < fft.Length; i += 2)
        {
          double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
          fftData[i] = (byte)fftmag;
          fftData[i + 1] = fftData[i];
        }

        int xx = 0;
        int step = (fftData.Length / Util.Context.Largeur);

        for (int x = 1; x < fftData.Length - step - 1; x += step)
        {
          double moyenne = 0;

          for (int i = x; i < x + step; i++)
            moyenne += fftData[i];

          double yMax = moyenne / step;

          for (int y = 0; y < Util.Context.Largeur; y++)
            if (y < Math.Ceiling(yMax))
              if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
                pixel.Set(y * 5, 0, (19 - y) * 5);

          xx++;
        }

        Util.SetLeds();

        for (int x = 0; x < Util.Context.Largeur; x++)
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

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * 0.78125;

        double max = fft.Max(a => Math.Abs(a));
        double amplitude = ((101 - max) / 100) * 0.5;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        for (int x = 0; x < Util.Context.Largeur; x++)
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

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * 0.78125;

        double max = fft.Max(a => Math.Abs(a));
        double amplitude = ((101 - max) / 100) * 0.5;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        for (int x = 0; x < Util.Context.Largeur; x++)
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

      PixelList pixels = new PixelList(Util.Context.Largeur, Util.Context.Largeur);
      double[] yMax = new double[21] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

      while (Util.TaskWork(task))
      {
        double[] fft = new double[audioBuffer.Length];
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * 0.78125;

        double max = fft.Max(a => Math.Abs(a));
        double amplitude = ((101 - max) / 100) * 0.5;

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        for (int x = 0; x < Util.Context.Largeur; x++)
        {
          yMax[x]--;

          int y = (int)fft[x * 2] + 8;
          byte red = (byte)Math.Abs(fft[x * 2] * 11);

          if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
            pixel.Set(red, 0, 127 - red);
        }

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * (amplitude * 5);

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

          if (yMax[xx] < moyenne / step)
            yMax[xx] = moyenne / step;

          for (int y = 0; y < Util.Context.Largeur; y++)
            if (y < Math.Ceiling(yMax[xx]))
              if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
                if (pixel.Couleur.IsNoir)
                  pixel.Set(y * 5, (19 - y) * 5, 0);

          xx++;
        }

        //foreach (Pixel pixel in pixels)
        //  Util.Context.Pixels.GetCoordonnee(pixel.Coord).SetColor(pixel.Couleur);

        Util.SetLeds();
        Util.Context.Pixels.Reset();

        //for (int x = 0; x < 20; x++)
        //  if (pixels.Where(p => p.Coord.X == x && !p.Couleur.IsNoir).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
        //    pixel.Couleur = Couleur.Noir;
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

      Couleur couleur = Couleur.Get(0, 0, 8);
      Random ra = new Random();
      bool whiteBgColor = true;

      if (ra.Next(1, 3) == 1)
      {
        couleur = Couleur.Get(63, 63, 127);
        whiteBgColor = false;
      }

      double max = 0;
      CaractereList caracteres = new CaractereList(Util.Context.Largeur);

      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        max -= 1;
        double[] fft = new double[audioBuffer.Length];
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128) * 0.703125;

        if (fft.Max(a => Math.Abs(a)) > max)
          max = fft.Max(a => Math.Abs(a));

        if (whiteBgColor)
          foreach (Pixel pixel in Util.Context.Pixels)
            pixel.Set(127, 127, 127);

        caracteres.SetText("VU");
        Util.Context.Pixels.Print(caracteres.GetCaracteres(), 5, 12, couleur);

        Couleur couleurMax = couleur;

        //lumiere max
        if (max > 75)
          couleurMax = Couleur.Get(127, 0, 0);

        Util.Context.Pixels.GetCoordonnee(17, 13).SetColor(couleurMax);
        Util.Context.Pixels.GetCoordonnee(18, 13).SetColor(couleurMax);
        Util.Context.Pixels.GetCoordonnee(17, 14).SetColor(couleurMax);
        Util.Context.Pixels.GetCoordonnee(18, 14).SetColor(couleurMax);

        //dessin
        Util.Context.Pixels.GetCoordonnee(1, 10).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(2, 10).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(3, 10).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(4, 9).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(5, 9).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(6, 9).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(7, 9).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(8, 8).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(9, 8).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(10, 8).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(11, 8).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(12, 9).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(13, 9).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(14, 9).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(15, 9).SetColor(Couleur.Get(127, 0, 0));

        Util.Context.Pixels.GetCoordonnee(16, 10).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(17, 10).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(18, 10).SetColor(Couleur.Get(127, 0, 0));

        //Moins
        Util.Context.Pixels.GetCoordonnee(1, 4).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(2, 4).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(3, 4).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(2, 8).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(2, 9).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(6, 7).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(6, 8).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(9, 6).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(9, 7).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(11, 6).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(11, 7).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(13, 7).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(13, 8).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(15, 7).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(15, 8).SetColor(Couleur.Get(127, 0, 0));

        Util.Context.Pixels.GetCoordonnee(17, 8).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(17, 9).SetColor(Couleur.Get(127, 0, 0));

        //Plus
        Util.Context.Pixels.GetCoordonnee(17, 3).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(16, 4).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(17, 4).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(18, 4).SetColor(Couleur.Get(127, 0, 0));
        Util.Context.Pixels.GetCoordonnee(17, 5).SetColor(Couleur.Get(127, 0, 0));

        //base
        Util.Context.Pixels.GetCoordonnee(8, 18).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(9, 18).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(10, 18).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(11, 18).SetColor(couleur);

        Util.Context.Pixels.GetCoordonnee(7, 19).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(8, 19).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(9, 19).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(10, 19).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(11, 19).SetColor(couleur);
        Util.Context.Pixels.GetCoordonnee(12, 19).SetColor(couleur);

        //aiguille
        for (int r = 2; r < 18; r++)
          Util.Context.Pixels.GetCoordonnee(GetCercleCoord(max + 315, r)).SetColor(couleur);

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
      Coordonnee coord = new Coordonnee(Util.Context.Largeur, Util.Context.Hauteur);

      if (degree % 360 >= 0 && degree % 360 <= 180)
        coord.X = 10 + (int)(rayon * Math.Sin(Math.PI * degree / 180));
      else
        coord.X = 10 - (int)(rayon * -Math.Sin(Math.PI * degree / 180)) - 1;

      coord.Y = 19 - (int)(rayon * Math.Cos(Math.PI * degree / 180) + 0.5);

      return coord.CheckCoord();
    }


    //public static void Test1()
    //{
    //  // Initialize the led strip
    //  Util.Setup();
    //  int task = Util.StartTask();
    //  byte[] audioBuffer = new byte[256];
    //  using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
    //  audioCapture.Start();

    //  while (Util.TaskWork(task))
    //  {
    //    double[] fft = new double[audioBuffer.Length];
    //    audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

    //    for (int i = 0; i < audioBuffer.Length; i++)
    //      fft[i] = (audioBuffer[i] - 128) * 0.78125;

    //    double max = fft.Max(a => Math.Abs(a));
    //    double amplitude = ((101 - max) / 100) * 0.5;

    //    for (int i = 0; i < audioBuffer.Length; i++)
    //      fft[i] = (audioBuffer[i] - 128) * amplitude;

    //    for (int x = 0; x < 20; x++)
    //    {
    //      int y = (int)(fft[x * 2]) + 10;
    //      byte red = (byte)Math.Abs(fft[x * 2] * 11);

    //      if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
    //        pixel.Set(red, 0, 127 - red);
    //    }

    //    Util.SetLeds();
    //    Util.Context.Pixels.Reset();
    //  }
    //}
  }
}