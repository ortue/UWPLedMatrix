using OpenTK.Audio.OpenAL;
using OpenTK.Audio;

namespace BLedMatrix.Shared
{
  public partial class Spectrograph
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecSpectrograph);
    }

    /// <summary>
    /// Spectrograph
    /// </summary>
    private void ExecSpectrograph()
    {
      int task = TaskGo.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;

      while (TaskGo.TaskWork(task))
      {
        //double[] fft = Capture(audioCapture, audioBuffer);
        //double amplitude = GetAmplitudeSpectroGraph(fft);
        //float[] fftData = SetFFT(audioBuffer, fft);

        //Spectrograph(fftData, amplitude);
        //Spectrograph(cycle++);
        Pixels.SendPixels();
      }
    }
  }
}