using Library.Collection;
using Library.Entity;
using MathNet.Numerics.IntegralTransforms;
using System.Diagnostics;
using System.Numerics;
//using OpenTK.Audio;
//using OpenTK.Audio.OpenAL;

namespace LedMatrix.Components.Layout
{
  public partial class Spectrum
  {
    private PixelList TabSpec { get; set; } = new PixelList(false);

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      _ = Task.Run(() =>
      {
        if (TaskGo.AudioCaptureConcurence)
        {
          TaskGo.StopTask();
          using ManualResetEventSlim waitHandle = new(false);
          waitHandle.Wait(TimeSpan.FromMilliseconds(100));
        }

        try
        {
          ExecSpectrum();
        }
        catch (Exception ex)
        {
          var a = ex;
        }
      });
    }

    private async void ExecSpectrum()
    {
      TaskGo.AudioCaptureConcurence = true;
      int task = TaskGo.StartTask("Spectrum");


      using Process process = new()
      {
        StartInfo = new ProcessStartInfo
        {
          FileName = "arecord",
          Arguments = "-D plughw:1,0 -f U8 -c 1 -r 22050 -t raw",
          RedirectStandardOutput = true,
          UseShellExecute = false,
          //RedirectStandardError = true, // pour debug si problème
          CreateNoWindow = true
        }
      };

      process.ErrorDataReceived += (sender, e) =>
      {
        if (!string.IsNullOrEmpty(e.Data))
          Console.WriteLine("Erreur arecord: " + e.Data);
      };

      process.Start();
      //process.BeginErrorReadLine();

      using Stream stream = process.StandardOutput.BaseStream;

      //using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      //audioCapture.Start();
      int cycle = 0;
      int debut = -20;
      int bufferSize = 256; // 1024 échantillons ≈ 46ms
      byte[] buffer = new byte[bufferSize];

      while (TaskGo.TaskWork(task))
      {

        //int bytesRead = stream.Read(buffer, 0, bufferSize);
        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);




        if (bytesRead == 0)
          continue;

        double[] samples = new double[bytesRead];

        for (int i = 0; i < bytesRead; i++)
          samples[i] = buffer[i] - 128;

        // FFT
        //Complex[] complexSamples = new Complex[samples.Length];
        // FFT
        Complex[] fft = new Complex[samples.Length];

        for (int i = 0; i < samples.Length; i++)
          fft[i] = new Complex(samples[i], 0);

        Fourier.Forward(fft, FourierOptions.Matlab);

        double amplitude = GetAmplitudeSpectrum(fft);


        //float[] fftData = SetFFT(buffer, complexSamples);





        AffHeure(cycle);
        debut = AffTitre(cycle, debut);

        SetSpectrum(fft, amplitude);

        foreach (Pixel spec in TabSpec)
        {
          Couleur couleur = spec.Position switch
          {
            1 => Couleurs.Get("Spectrum", "HeureCouleur", Couleur.RougePale),
            2 => Couleurs.Get("Spectrum", "HeureAltCouleur", Couleur.Rouge),
            3 => Couleurs.Get("Spectrum", "TitreCouleur", Couleur.RougePale),
            4 => Couleurs.Get("Spectrum", "TitreAltCouleur", Couleur.Rouge),
            _ => spec.Couleur,
          };

          Pixels.Get(spec).SetColor(couleur);
        }

        Pixels.SendPixels();
        SetSpectrum(cycle++);
      }

      TaskGo.AudioCaptureConcurence = false;
    }

    /// <summary>
    /// Spectrum
    /// </summary>
    //private void ExecSpectrum(bool inter)
    //{
    //  TaskGo.AudioCaptureConcurence = true;
    //  int task = TaskGo.StartTask("Spectrum");

    //  byte[] audioBuffer = new byte[256];
    //  using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
    //  audioCapture.Start();
    //  int cycle = 0;
    //  int debut = -20;

    //  while (TaskGo.TaskWork(task))
    //  {
    //    double[] fft = Capture(audioCapture, audioBuffer);
    //    double amplitude = GetAmplitudeSpectrum(fft);
    //    float[] fftData = SetFFT(audioBuffer, fft);

    //    AffHeure(cycle);
    //    debut = AffTitre(cycle, debut);

    //    SetSpectrum(fftData, amplitude);

    //    foreach (Pixel spec in TabSpec)
    //    {
    //      Couleur couleur = spec.Position switch
    //      {
    //        1 => Couleurs.Get("Spectrum", "HeureCouleur", Couleur.RougePale),
    //        2 => Couleurs.Get("Spectrum", "HeureAltCouleur", Couleur.Rouge),
    //        3 => Couleurs.Get("Spectrum", "TitreCouleur", Couleur.RougePale),
    //        4 => Couleurs.Get("Spectrum", "TitreAltCouleur", Couleur.Rouge),
    //        _ => spec.Couleur,
    //      };

    //      Pixels.Get(spec).SetColor(couleur);
    //    }

    //    Pixels.SendPixels();
    //    SetSpectrum(cycle++);
    //  }

    //  TaskGo.AudioCaptureConcurence = false;
    //}

    /// <summary>
    /// Capture
    /// </summary>
    /// <param name="audioCapture"></param>
    /// <param name="audioBuffer"></param>
    /// <returns></returns>
    //private static double[] Capture(AudioCapture audioCapture, byte[] audioBuffer)
    //{
    //  audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);
    //  double[] fft = new double[audioBuffer.Length];

    //  for (int i = 0; i < audioBuffer.Length; i++)
    //    fft[i] = audioBuffer[i] - 128;

    //  return fft;
    //}

    /// <summary>
    /// GetAmplitude
    /// </summary>
    /// <param name="fft"></param>
    /// <returns></returns>
    public static double GetAmplitudeSpectrum(Complex[] fft)
    {
      double max = fft.Max(a => a.Magnitude);

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
    //private static float[] SetFFT(byte[] audioBuffer, double[] fft)
    //{
    //  LomFFT LomFFT = new();
    //  LomFFT.RealFFT(fft, true);

    //  float[] fftData = new float[audioBuffer.Length / 2];
    //  double lengthSqrt = Math.Sqrt(audioBuffer.Length);

    //  for (int j = 0; j < audioBuffer.Length / 2; j++)
    //  {
    //    double re = fft[2 * j] * lengthSqrt;
    //    double img = fft[2 * j + 1] * lengthSqrt;
    //    fftData[j] = (float)Math.Sqrt(re * re + img * img);
    //  }

    //  return fftData;
    //}

    /// <summary>
    /// Spectrum
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    private void SetSpectrum(Complex[] fftData, double amplitude)
    {
      for (int x = 0; x < PixelList.Largeur; x++)
      {
        double yMax = Magnitude(fftData, x, amplitude);

        for (int y = 0; y < PixelList.Hauteur; y++)
          if (y < Math.Ceiling(yMax))
            TabSpec.Get(x, 19 - y).SetColor(ProportionCouleur(y));
      }
    }

    /// <summary>
    /// ProportionCouleur
    /// </summary>
    /// <returns></returns>
    private Couleur ProportionCouleur(int y)
    {
      double bas = (19d - y) / 19d;
      double haut = y / 19d;

      int r = (int)Math.Floor((Couleurs.Get("Spectrum", "BasCouleur", Couleur.Bleu).R * bas) + (Couleurs.Get("Spectrum", "HautCouleur", Couleur.Rouge).R * haut));
      int g = (int)Math.Floor((Couleurs.Get("Spectrum", "BasCouleur", Couleur.Bleu).G * bas) + (Couleurs.Get("Spectrum", "HautCouleur", Couleur.Rouge).G * haut));
      int b = (int)Math.Floor((Couleurs.Get("Spectrum", "BasCouleur", Couleur.Bleu).B * bas) + (Couleurs.Get("Spectrum", "HautCouleur", Couleur.Rouge).B * haut));

      return Couleur.Get(r, g, b);
    }

    /// <summary>
    /// Spectrum trainée
    /// </summary>
    private void SetSpectrum(int cycle)
    {
      //Plus le chffre du mod est grand, plus la trainer sera ralenti
      if (cycle % 10 == 0)
        for (int x = 0; x < PixelList.Largeur; x++)
          if (TabSpec.Where(p => p.X == x && !p.Couleur.IsNoir).OrderBy(p => p.Y).FirstOrDefault() is Pixel pixel)
            pixel.Couleur = Couleur.Noir;
    }

    /// <summary>
    /// Magnitude
    /// </summary>
    /// <param name="fftData"></param>
    /// <param name="x"></param>
    /// <param name="amplitude"></param>
    /// <returns></returns>
    private static double Magnitude(Complex[] fft, int x, double amplitude)
    {
      double[] fftData = new double[fft.Length];

      for (int i = 0; i < fft.Length; i++)
        fftData[i] = fft[i].Real;

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
      if (cycle % 8 == 0)
      {
        foreach (Pixel pixel in TabSpec.Where(p => (p.Position == 1 || p.Position == 2) && p.Y > 12))
          pixel.Position = 0;

        if (TaskGo.HeureMusique)
        {
          CaractereList textes = new(PixelList.Largeur);
          textes.SetText(CaractereList.Heure);

          foreach (Police lettre in textes.GetCaracteres().Where(c => c.Point))
            if (TabSpec.Get(lettre.X + 1, lettre.Y + 13) is Pixel pixel)
              if (TabSpec.Any(p => p.X == pixel.X && p.Y < pixel.Y))
                if (pixel.Couleur.IsNoir)
                  pixel.Position = 1;
                else
                  pixel.Position = 2;
        }
      }
    }

    /// <summary>
    /// Afficher titre
    /// </summary>
    private int AffTitre(int cycle, int debut)
    {
      if (cycle % 16 == 0)
      {
        foreach (Pixel pixel in TabSpec.Where(p => (p.Position == 3 || p.Position == 4) && p.Y < 8))
          pixel.Position = 0;

        if (TaskGo.TitreKodi)
        {
          CaractereList textes = new(PixelList.Largeur);
          int largeur = textes.SetText(Titre.Musique);

          foreach (Police lettre in textes.GetCaracteres(debut).Where(c => c.Point))
            if (TabSpec.Get(lettre.X, lettre.Y + 1) is Pixel pixel)
              if (pixel.Couleur.IsNoir)//|| pixel.Couleur.IsRouge
                pixel.Position = 3;
              else
                pixel.Position = 4;

          debut++;

          //Reset après avoir défiler tout le texte
          if (cycle % 100000 == 0 || largeur < debut)
          {
            debut = -20;
            Titre.Refresh();
          }
        }
      }

      return debut;
    }
  }
}