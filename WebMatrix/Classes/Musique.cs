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
    public static void Spectrum()
    {
      // Initialize the led strip
      Criteria.Spectrum = true;
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;
      int debut = -20;

      while (Util.TaskWork(task))
      {
        double[] fft = Capture(audioCapture, audioBuffer);
        Spectrum(audioBuffer, fft, 0.15);
        debut = AffTitre(cycle, debut);
        AffHeure();
        Util.SetLeds();

        Spectrum(cycle++);
      }
    }

    /// <summary>
    /// Graph
    /// </summary>
    public static void Graph()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;
      int debut = -20;

      while (Util.TaskWork(task))
      {
        Graph(audioCapture, audioBuffer);
        AffHeure();
        debut = AffTitre(cycle++, debut);
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
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (Util.TaskWork(task))
      {
        max -= 1;

        double[] fft = Capture(audioCapture, audioBuffer);

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
    /// Spectrum trainée
    /// </summary>
    private static void Spectrum(int cycle)
    {
      if (cycle % 4 == 0)
      {
        for (int x = 0; x < Util.Context.Largeur; x++)
          if (Util.Context.Pixels.Where(p => p.Coord.X == x && !p.Couleur.IsNoir && !p.Couleur.IsRouge).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
            pixel.Couleur = Couleur.Noir;

        for (int x = 0; x < Util.Context.Largeur; x++)
          if (Util.Context.Pixels.Where(p => p.Coord.X == x && p.Couleur.R == 127).OrderBy(p => p.Coord.Y).FirstOrDefault() is Pixel pixel)
            pixel.Couleur = Couleur.RougePale;



        //if (pixel.Couleur.IsRouge)
        //  pixel.Couleur = Couleur.RougePale;
        //else
      }
    }

    /// <summary>
    /// Spectrograph
    /// </summary>
    public static void Spectrograph()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;

      while (Util.TaskWork(task))
      {
        double[] fft = Capture(audioCapture, audioBuffer);
        Spectrograph(audioBuffer, fft);
        Spectrograph(cycle++);
        Util.SetLeds();
      }
    }

    /// <summary>
    /// Capture
    /// </summary>
    /// <param name="audioCapture"></param>
    /// <param name="audioBuffer"></param>
    /// <returns></returns>
    private static double[] Capture(AudioCapture audioCapture, byte[] audioBuffer)
    {
      audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);
      double[] fft = new double[audioBuffer.Length];

      for (int i = 0; i < audioBuffer.Length; i++)
        fft[i] = audioBuffer[i] - 128;

      return fft;
    }

    /// <summary>
    /// Spectrograph Défillement
    /// </summary>
    /// <param name="spectrographArray"></param>
    private static void Spectrograph(int cycle)
    {
      if (cycle % 8 == 0)
        for (int x = 0; x < Util.Context.Largeur - 1; x++)
          for (int y = 0; y < Util.Context.Hauteur; y++)
            if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
              if (Util.Context.Pixels.GetCoordonnee(x + 1, y) is Pixel pixelPlusUn)
                pixel.SetColor(pixelPlusUn.Couleur);
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
        fftData[j] = (float)Math.Sqrt(re * re + img * img);
      }

      int yy = 0;
      int step = fftData.Length / Util.Context.Largeur;

      for (int x = 0; x < fftData.Length - step - 2; x += step)
      {
        double moyenne = 0;

        for (int i = x; i < x + step; i++)
          moyenne += fftData[i];

        //Calcul du restant a droite
        if (yy == 19)
          for (int i = x + step; i < fftData.Count(); i++)
            moyenne += fftData[i];

        int volume = (int)(((moyenne / step) - 10) * Ajustement(19 - yy));

        if (volume < 0)
          volume = 0;

        byte bleu = (byte)volume;
        byte vert = 0;
        byte rouge = 0;

        if (volume > 127)
        {
          bleu = 127;
          rouge = (byte)(volume - 127);
        }

        if (volume > 255)
        {
          rouge = 127;
          vert = (byte)(volume - 255);
        }

        if (Util.Context.Pixels.GetCoordonnee(19, 19 - yy) is Pixel pixel)
          pixel.SetColor(Couleur.Get(rouge, vert, bleu));

        yy++;
      }
    }

    /// <summary>
    /// Afficher titre
    /// </summary>
    private static int AffTitre(int cycle, int debut)
    {
      if (Criteria.AffTitre)
      {
        foreach (Pixel pixel in Util.Context.Pixels.Where(p => p.Couleur.IsRouge && p.Coord.Y < 8))
          pixel.Couleur = Couleur.Noir;

        CaractereList textes = new CaractereList(Util.Context.Largeur);
        int largeur = textes.SetText(Util.Musique);

        foreach (Police lettre in textes.GetCaracteres(debut).Where(c => c.Point))
          if (Util.Context.Pixels.GetCoordonnee(lettre.X, lettre.Y + 1) is Pixel pixel)
            if (pixel.Couleur.IsNoir)//|| pixel.Couleur.IsRouge
              pixel.SetColor(Couleur.Rouge);
            else
              pixel.SetColor(Couleur.RougePale);

        if (cycle % Criteria.CycleMod == 0)
          debut++;

        //Reset après avoir défiler tout le texte
        if (cycle % 10000 == 0 || largeur < debut)
        {
          debut = -20;
          Util.GetMusiqueAsync();
        }
      }

      return debut;
    }

    /// <summary>
    /// AffHeure
    /// </summary>
    private static void AffHeure()
    {
      if (Criteria.AffHeure)
      {
        foreach (Pixel pixel in Util.Context.Pixels.Where(p => p.Couleur.IsRouge && p.Coord.Y > 12))
        pixel.Couleur = Couleur.Noir;

        CaractereList textes = new CaractereList(Util.Context.Largeur);
        textes.SetText(Temps.Heure);

        foreach (Police lettre in textes.GetCaracteres().Where(c => c.Point))
          if (Util.Context.Pixels.GetCoordonnee(lettre.X + 1, lettre.Y + 13) is Pixel pixel)
            if (pixel.Couleur.IsNoir)// || pixel.Couleur.R == 127)
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
    private static void Graph(AudioCapture audioCapture, byte[] audioBuffer)
    {
      double[] fft = new double[audioBuffer.Length];
      audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);

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