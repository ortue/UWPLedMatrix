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
    public static Couleur[,] SpectrographArray { get; set; }

    /// <summary>
    /// Graph
    /// </summary>
    public static void Graph(Criteria criteria)
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

        Graph(audioBuffer, fft);
        AffHeure();
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

    /// <summary>
    /// Spectrum3
    /// </summary>
    public static void Spectrum()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);
        double[] fft = new double[audioBuffer.Length];

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128);

        Spectrum(audioBuffer, fft, 0.15);
        AffHeure();

        Util.SetLeds();

        for (int x = 0; x < Util.Context.Largeur; x++)
          if (Util.Context.Pixels.Where(p => p.Coord.X == x && !p.Couleur.IsNoir && !p.Couleur.IsRouge).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
            pixel.Couleur = Couleur.Noir;
      }
    }

    /// <summary>
    /// Spectrograph
    /// </summary>
    public static void Spectrograph()
    {
      SpectrographArray = new Couleur[20, 20];

      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);
        double[] fft = new double[audioBuffer.Length];

        for (int i = 0; i < audioBuffer.Length; i++)
          fft[i] = (audioBuffer[i] - 128);

        Spectrograph(audioBuffer, fft);

        Util.SetLeds();
      }
    }

    /// <summary>
    /// Spectrograph
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    private static void Spectrograph(byte[] audioBuffer, double[] fft)
    {
      LomFFT LomFFT = new LomFFT();
      LomFFT.RealFFT(fft, true);

      float[] fftData = new float[audioBuffer.Length / 2];
      double lengthSqrt = Math.Sqrt(audioBuffer.Length);

      for (int j = 0; j < audioBuffer.Length / 2; j++)
      {
        double re = fft[2 * j] * lengthSqrt;
        double img = fft[2 * j + 1] * lengthSqrt;

        // do the Abs calculation and add with Math.Sqrt(audio_data.Length);
        // i.e. the magnitude spectrum
        fftData[j] = (float)(Math.Sqrt(re * re + img * img));
      }

      int yy = 0;
      int step = (fftData.Length / Util.Context.Largeur);

      for (int x = 0; x < fftData.Length - step-2; x += step)
      {
        double moyenne = 0;

        for (int i = x; i < x + step; i++)
          moyenne += fftData[i];

        //Calcul du restant a droite
        if (yy == 19)
          for (int i = x + step; i < fftData.Count(); i++)
            moyenne += fftData[i];

        Couleur couleur = Couleur.Get(0, 0, (int)(moyenne / step));

        for (int xx = 0; xx < Util.Context.Largeur-1; xx++)
          for (int y = 0; y < Util.Context.Hauteur; y++)
            if (SpectrographArray[xx + 1, 19 - y] is Couleur spectrograph)
              SpectrographArray[xx, 19 - y] = spectrograph;

        SpectrographArray[19, 19 - yy] = couleur;

        for (int xx = 0; xx < Util.Context.Hauteur; xx++)
          if (Util.Context.Pixels.GetCoordonnee(xx, yy) is Pixel pixel)
            if (SpectrographArray[xx, yy] is Couleur spectrograph)
              pixel.SetColor(spectrograph);

        yy++;
      }


      //int xx = 0;
      //int step = (fftData.Length / Util.Context.Largeur);

      //for (int x = 0; x < fftData.Length - step; x += step)
      //{
      //  double moyenne = 0;

      //  for (int i = x; i < x + step; i++)
      //    moyenne += fftData[i];

      //  //Calcul du restant a droite
      //  if (xx == 19)
      //    for (int i = x + step; i < fftData.Count(); i++)
      //      moyenne += fftData[i];

      //  double yMax = moyenne / step * Ajustement(xx);

      //  for (int y = 0; y < Util.Context.Hauteur; y++)
      //    if (y < Math.Ceiling(yMax))
      //      if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
      //        pixel.Set(y * 5, 0, (20 - y) * 5);

      //  xx++;
      //}
    }

    /// <summary>
    /// AffHeure
    /// </summary>
    private static void AffHeure()
    {
      if (Criteria.AffHeure)
      {
        foreach (Pixel pixel in Util.Context.Pixels.Where(p => p.Couleur.IsRouge))
          pixel.Couleur = Couleur.Noir;

        CaractereList textes = new CaractereList(Util.Context.Largeur);

        textes.SetText(Temps.Heure);

        foreach (Police lettre in textes.GetCaracteres().Where(c => c.Point))
          if (Util.Context.Pixels.GetCoordonnee(lettre.X + 1, lettre.Y + 13) is Pixel pixel)
            if (pixel.Couleur.IsNoir)//|| pixel.Couleur.IsRouge
              pixel.SetColor(Couleur.RougePale);
            else
              pixel.SetColor(Couleur.Rouge);
      }
    }

    /// <summary>
    /// Graph
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    private static void Graph(byte[] audioBuffer, double[] fft)
    {
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
    }

    /// <summary>
    /// Spectrum
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    private static void Spectrum(byte[] audioBuffer, double[] fft, double amplitude)
    {
      LomFFT LomFFT = new LomFFT();
      LomFFT.RealFFT(fft, true);

      float[] fftData = new float[audioBuffer.Length / 2];
      double lengthSqrt = Math.Sqrt(audioBuffer.Length);

      for (int j = 0; j < audioBuffer.Length / 2; j++)
      {
        double re = fft[2 * j] * lengthSqrt;
        double img = fft[2 * j + 1] * lengthSqrt;

        // do the Abs calculation and add with Math.Sqrt(audio_data.Length);
        // i.e. the magnitude spectrum
        fftData[j] = (float)(Math.Sqrt(re * re + img * img) * amplitude);
      }

      int xx = 0;
      int step = (fftData.Length / Util.Context.Largeur);

      for (int x = 0; x < fftData.Length - step; x += step)
      {
        double moyenne = 0;

        for (int i = x; i < x + step; i++)
          moyenne += fftData[i];

        //Calcul du restant a droite
        if (xx == 19)
          for (int i = x + step; i < fftData.Count(); i++)
            moyenne += fftData[i];

        double yMax = moyenne / step * Ajustement(xx);

        for (int y = 0; y < Util.Context.Hauteur; y++)
          if (y < Math.Ceiling(yMax))
            if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
              pixel.Set(y * 5, 0, (20 - y) * 5);

        xx++;
      }
    }

    /// <summary>
    /// Ajustement
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private static double Ajustement(int x)
    {
      return x switch
      {
        0 => 0.5,
        1 => 0.7,
        3 => 0.9,
        _ => 1,
      };
    }
  }
}