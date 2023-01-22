using Library.Collection;
using Library.Entity;
using Library.Util;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace BLedMatrix.Shared
{
  public partial class Spectrum
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(() =>
      {
        if (TaskGo.AudioCaptureConcurence)
        {
          TaskGo.StopTask();
          using ManualResetEventSlim waitHandle = new(false);
          waitHandle.Wait(TimeSpan.FromMilliseconds(100));
        }

        ExecSpectrum();
      });
    }

    /// <summary>
    /// Spectrum
    /// </summary>
    private void ExecSpectrum()
    {
      TaskGo.AudioCaptureConcurence = true;
      int task = TaskGo.StartTask();

      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;
      int debut = -20;

      while (TaskGo.TaskWork(task))
      {
        double[] fft = Capture(audioCapture, audioBuffer);
        double amplitude = GetAmplitudeSpectrum(fft);
        float[] fftData = SetFFT(audioBuffer, fft);

        AffHeure(cycle);
        debut = AffTitre(cycle, debut);

        SetSpectrum(fftData, amplitude);
        Pixels.SendPixels();
        SetSpectrum(cycle++);
      }

      TaskGo.AudioCaptureConcurence = false;
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
    /// GetAmplitude
    /// </summary>
    /// <param name="fft"></param>
    /// <returns></returns>
    public static double GetAmplitudeSpectrum(double[] fft)
    {
      double max = fft.Max(Math.Abs);

      return max switch
      {
        > 75 => 0.005,
        > 50 => 0.01,
        > 25 => 0.02,
        > 15 => 0.03,
        > 10 => 0.04,
        > 5 => 0.05,
        > 4 => 0.06,
        > 3 => 0.07,
        > 1 => 0.08,
        _ => 0.03
      };
    }

    /// <summary>
    /// SetFFT
    /// do the Abs calculation and add with Math.Sqrt(audio_data.Length);
    /// i.e. the magnitude spectrum
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    /// <returns></returns>
    private static float[] SetFFT(byte[] audioBuffer, double[] fft)
    {
      LomFFT LomFFT = new();
      LomFFT.RealFFT(fft, true);

      float[] fftData = new float[audioBuffer.Length / 2];
      double lengthSqrt = Math.Sqrt(audioBuffer.Length);

      for (int j = 0; j < audioBuffer.Length / 2; j++)
      {
        double re = fft[2 * j] * lengthSqrt;
        double img = fft[2 * j + 1] * lengthSqrt;
        fftData[j] = (float)(Math.Sqrt(re * re + img * img));
      }

      return fftData;
    }

    /// <summary>
    /// Spectrum
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    private void SetSpectrum(float[] fftData, double amplitude)
    {
      for (int x = 0; x < PixelList.Largeur; x++)
      {
        double yMax = Magnitude(fftData, x, amplitude);

        for (int y = 0; y < PixelList.Hauteur; y++)
          if (y < Math.Ceiling(yMax))
            if (Pixels.Get(x, 19 - y) is Pixel pixel)
              if (pixel.Couleur.IsRouge)
                pixel.SetColor(Couleur.Rouge);
              else
                pixel.SetColor(y * 5, 0, (20 - y) * 5);
      }
    }

    /// <summary>
    /// Spectrum trainée
    /// </summary>
    private void SetSpectrum(int cycle)
    {
      if (cycle % 8 == 0)
        for (int x = 0; x < PixelList.Largeur; x++)
          if (Pixels.Where(p => p.X == x && !p.Couleur.IsNoir && !p.Couleur.IsRouge).OrderBy(p => p.Y).FirstOrDefault() is Pixel pixel)
            pixel.Couleur = Couleur.Noir;
    }

    /// <summary>
    /// Magnitude
    /// </summary>
    /// <param name="fftData"></param>
    /// <param name="x"></param>
    /// <param name="amplitude"></param>
    /// <returns></returns>
    private static double Magnitude(float[] fftData, int x, double amplitude)
    {
      return x switch
      {
        0 => (fftData[x] - 140) * amplitude,

        1 => (fftData[x] - 20) * amplitude * 0.6,
        2 => (fftData[x] - 25) * amplitude * 0.7,
        3 => (fftData[x] - 26) * amplitude * 0.8,

        11 => (fftData.Skip(10).Take(3).Average() - 11) * amplitude,
        12 => (fftData.Skip(13).Take(5).Average() - 11) * amplitude,
        13 => (fftData.Skip(18).Take(7).Average() - 11) * amplitude * 2,
        14 => (fftData.Skip(25).Take(9).Average() - 10) * amplitude * 2,
        15 => (fftData.Skip(34).Take(11).Average() - 10) * amplitude * 2,
        16 => (fftData.Skip(45).Take(13).Average() - 9) * amplitude * 3,
        17 => (fftData.Skip(58).Take(17).Average() - 8) * amplitude * 4,
        18 => (fftData.Skip(75).Take(22).Average() - 9) * amplitude * 5,
        19 => (fftData.Skip(97).Take(30).Average() - 7) * amplitude * 6,

        _ => (fftData[x] - 20) * amplitude * 0.8
      };
    }

    /// <summary>
    /// AffHeure
    /// </summary>
    private void AffHeure(int cycle)
    {
      if (TaskGo.HeureMusique && cycle % 16 == 0)
      {
        foreach (Pixel pixel in Pixels.Where(p => p.Couleur.IsRouge && p.Y > 12))
          pixel.Couleur = Couleur.Noir;

        CaractereList textes = new(PixelList.Largeur);
        textes.SetText(CaractereList.Heure);

        foreach (Police lettre in textes.GetCaracteres().Where(c => c.Point))
          if (Pixels.Get(lettre.X + 1, lettre.Y + 13) is Pixel pixel)
            if (Pixels.Any(p => !p.Couleur.IsNoir && !p.Couleur.IsRouge && p.X == pixel.X && p.Y < pixel.Y))
              pixel.SetColor(Couleur.Rouge);
            else
              pixel.SetColor(Couleur.RougePale);
      }
    }

    /// <summary>
    /// Afficher titre
    /// </summary>
    private int AffTitre(int cycle, int debut)
    {
      if (TaskGo.TitreKodi && (cycle % 16 == 0))
      {
        foreach (Pixel pixel in Pixels.Where(p => p.Couleur.IsRouge && p.Y < 8))
          pixel.Couleur = Couleur.Noir;

        CaractereList textes = new(PixelList.Largeur);
        int largeur = textes.SetText(Titre.Musique);

        foreach (Police lettre in textes.GetCaracteres(debut).Where(c => c.Point))
          if (Pixels.Get(lettre.X, lettre.Y + 1) is Pixel pixel)
            if (pixel.Couleur.IsNoir)//|| pixel.Couleur.IsRouge
              pixel.SetColor(Couleur.Rouge);
            else
              pixel.SetColor(Couleur.RougePale);

        debut++;

        //Reset après avoir défiler tout le texte
        if (cycle % 100000 == 0 || largeur < debut)
        {
          debut = -20;
          Titre.Refresh();
        }
      }

      return debut;
    }
  }
}