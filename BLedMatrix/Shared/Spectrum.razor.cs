using Library.Entity;
using Library.Collection;
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
      Task.Run(ExecSpectrum);
    }

    /// <summary>
    /// Spectrum
    /// </summary>
    private void ExecSpectrum()
    {
      int task = TaskGo.StartTask();

      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;
      int debut = -20;

      while (TaskGo.TaskWork(task))
      {
        //double[] fft = Capture(audioCapture, audioBuffer);
        //double amplitude = GetAmplitudeSpectrum(fft);
        //float[] fftData = SetFFT(audioBuffer, fft);

        ////AffHeure(cycle);
        //debut = AffTitre(cycle, debut);

        //Spectrum(fftData, amplitude);
        //SetLeds();
        //Spectrum(cycle++);
      }
    }
  }
}