using System;
using System.Threading;
using WebMatrix.Context;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System.Linq;
using LedLibrary.Entities;

namespace WebMatrix.Classes
{
  public class Musique
  {
    /// <summary>
    /// Spectrum
    /// </summary>
    public static void Spectrum1()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      byte[] audioBuffer = new byte[256];
      byte[] fftData = new byte[256];
      double[] fft = new double[256];
      double fftavg = 0;
      float amplitude = 10.0f;

      LomontFFT fftTransoformer = new LomontFFT();

      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, 256);
      audioCapture.Start();
      audioCapture.ReadSamples(audioBuffer, 256);

      while (Util.TaskWork(task))
      {
        for (int i = 0; i < 256; i++)
        {
          audioBuffer[i] = 0;
          fftData[i] = 0;
          fft[i] = 0;
        }

        audioCapture.ReadSamples(audioBuffer, 256);

        for (int i = 0; i < 256; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        fftTransoformer.TableFFT(fft, true);

        for (int i = 0; i < 256; i += 2)
        {
          double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
          fftavg += fftmag;
          fftData[i] = (byte)fftmag;
          fftData[i + 1] = fftData[i];
        }

        fftavg /= 10;

        for (int x = 0; x < 20; x++)
          for (int y = 0; y < 20; y++)
            if (fftData[(int)(x * 13.42)] > (255 / 30 * (20 - y)))
              if (Util.Context.Pixels.GetCoordonnee(x, y) is Pixel pixel)
                pixel.Set(0 + (20 - y) * 5, 32, 127);

        Util.SetLeds();
        Util.Context.Pixels.Reset();

        using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(4));
      }
    }

    /// <summary>
    /// Spectrum
    /// </summary>
    public static void Spectrum2()
    {
      // Initialize the led strip
      Util.Setup();
      int task = Util.StartTask();

      byte[] audioBuffer = new byte[256];
      //byte[] fftData = new byte[256];
      //double[] fft = new double[256];
      //double fftavg = 0;
      //float amplitude = 10.0f;
      //LomontFFT fftTransoformer = new LomontFFT();

      using AudioCapture audioCapture = new AudioCapture(AudioCapture.AvailableDevices[1], 8000, ALFormat.Mono8, 256);
      audioCapture.Start();
      audioCapture.ReadSamples(audioBuffer, 256);

      while (Util.TaskWork(task))
      {
        byte[] fftData = new byte[256];
        double[] fft = new double[256];
        double fftavg = 0;
        float amplitude = 10.0f;
        LomontFFT fftTransoformer = new LomontFFT();


        audioCapture.ReadSamples(audioBuffer, 256);

        for (int i = 0; i < 256; i++)
          fft[i] = (audioBuffer[i] - 128) * amplitude;

        fftTransoformer.TableFFT(fft, true);

        for (int i = 0; i < 256; i += 2)
        {
          double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
          fftavg += fftmag;
          fftData[i] = (byte)fftmag;
          fftData[i + 1] = fftData[i];
        }

        fftavg /= 10;

        int xx = 0;

        for (int x = 1; x < 256 - 11; x += 12)
        {
          double yMax = (double)(new double[]
          {
            fftData[x]/ 13.43,
            fftData[x + 1]/ 13.43,
            fftData[x + 2]/ 13.43,
            fftData[x + 3]/ 13.43,
            fftData[x + 4]/ 13.43,
            fftData[x + 5]/ 13.43,
            fftData[x + 6]/ 13.43,
            fftData[x + 7]/ 13.43,
            fftData[x + 8]/ 13.43,
            fftData[x + 9]/ 13.43,
            fftData[x + 10]/ 13.43,
            fftData[x + 11]/ 13.43
          }).Average();



          for (int y = 0; y < 20; y++)
            if (y < Math.Ceiling(yMax))
            {
              if (Util.Context.Pixels.GetCoordonnee(xx, 19 - y) is Pixel pixel)
                pixel.Set(y * 5, 0, (19 - y) * 5);
            }

          xx++;
        }

        //if (Util.Context.Pixels.Any(p => p.Couleur != Couleur.Noir))
        Util.SetLeds();
        Util.Context.Pixels.Reset();

        //using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        //waitHandle.Wait(TimeSpan.FromMilliseconds(4));
      }
    }
  }
}