using Library.Collection;
using Library.Entity;
using Library.Util;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace BlazorAppMatrix.Components.Layout
{
  public partial class Spectrograph
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

        try
        {
          ExecSpectrograph();
        }
        catch (Exception)
        {

        }
      });
    }

    /// <summary>
    /// Spectrograph
    /// </summary>
    private void ExecSpectrograph()
    {
      TaskGo.AudioCaptureConcurence = true;

      int task = TaskGo.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;

      while (TaskGo.TaskWork(task))
      {
        double[] fft = Capture(audioCapture, audioBuffer);
        double amplitude = GetAmplitudeSpectroGraph(fft);
        float[] fftData = SetFFT(audioBuffer, fft);

        SetSpectrograph(fftData, amplitude);
        SetSpectrograph(cycle++);
        Pixels.SendPixels();

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(1));
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
    /// GetAmplitudeSpectroGraph
    /// </summary>
    /// <param name="fft"></param>
    /// <returns></returns>
    private static double GetAmplitudeSpectroGraph(double[] fft)
    {
      double max = fft.Max(Math.Abs);
      return max switch
      {
        > 75 => 0.05,
        > 50 => 0.1,
        > 25 => 0.2,
        > 15 => 0.4,
        > 10 => 0.7,
        > 5 => 0.6,
        > 4 => 0.7,
        > 3 => 0.8,
        > 1 => 0.9,
        _ => 0.1
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
    /// Spectrograph
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    private void SetSpectrograph(float[] fftData, double amplitude)
    {
      for (int y = 0; y < PixelList.Hauteur; y++)
      {
        int volume = (int)Volume(fftData, y, amplitude);

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

        if (Pixels.Get(19, 19 - y) is Pixel pixel)
          pixel.SetColor(Couleur.Get(rouge, vert, bleu));
      }
    }

    /// <summary>
    /// Spectrograph Défillement
    /// </summary>
    /// <param name="cycle"></param>
    private void SetSpectrograph(int cycle)
    {
      if (cycle % 12 == 0)
        for (int x = 0; x < PixelList.Largeur - 1; x++)
          for (int y = 0; y < PixelList.Hauteur; y++)
            if (Pixels.Get(x, y) is Pixel pixel)
              if (Pixels.Get(x + 1, y) is Pixel pixelPlusUn)
                pixel.SetColor(pixelPlusUn.Couleur);
    }

    /// <summary>
    /// Magnitude
    /// </summary>
    /// <param name="fftData"></param>
    /// <param name="x"></param>
    /// <param name="amplitude"></param>
    /// <returns></returns>
    private static double Volume(float[] fftData, int x, double amplitude)
    {
      return x switch
      {
        0 => (fftData[x] - 100) * amplitude,

        1 => (fftData[x] - 2) * amplitude,// * 0.6,
        2 => (fftData[x] - 2) * amplitude,// * 0.7,
        3 => (fftData[x] - 2) * amplitude,// * 0.8,

        11 => (fftData.Skip(10).Take(3).Average() - 2) * amplitude,
        12 => (fftData.Skip(13).Take(5).Average() - 2) * amplitude,
        13 => (fftData.Skip(18).Take(7).Average() - 4) * amplitude * 2,
        14 => (fftData.Skip(25).Take(9).Average() - 4) * amplitude * 2,
        15 => (fftData.Skip(34).Take(11).Average() - 4) * amplitude * 2,
        16 => (fftData.Skip(45).Take(13).Average() - 4) * amplitude * 3,
        17 => (fftData.Skip(58).Take(17).Average() - 5) * amplitude * 4,
        18 => (fftData.Skip(75).Take(22).Average() - 6) * amplitude * 5,
        19 => (fftData.Skip(97).Take(30).Average() - 4) * amplitude * 6,

        _ => (fftData[x] - 2) * amplitude// * 0.8
      };
    }
  }
}