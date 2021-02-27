using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace AudioSpectrum
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      var audioBuffer = new byte[256];
      var fftData = new byte[256];
      var fft = new double[256];
      double fftavg = 0;
      float amplitude = 10.0f;
      var fftTransoformer = new LomontFFT();



      var audioCapture = new AudioCapture(AudioCapture.DefaultDevice, 8000, OpenTK.Audio.OpenAL.ALFormat.Mono8, 256);
      audioCapture.Start();
      audioCapture.ReadSamples(audioBuffer, 256);

      while (true)
      {
        for (int j = 0; j < 92; j++)
        {
          // reset mem
          for (int i = 0; i < 256; i++)
          {
            audioBuffer[i] = 0;
            fftData[i] = 0;
            fft[i] = 0;
          }

          audioCapture.ReadSamples(audioBuffer, 256);

          for (int i = 0; i < 256; i++)
          {
            fft[i] = (audioBuffer[i] - 128) * amplitude;
          }

          fftTransoformer.TableFFT(fft, true);

          for (int i = 0; i < 256; i += 2)
          {
            double fftmag = Math.Sqrt((fft[i] * fft[i]) + (fft[i + 1] * fft[i + 1]));
            fftavg += fftmag;
            fftData[i] = (byte)fftmag;
            fftData[i + 1] = fftData[i];
          }

          fftavg /= 10;

          //writers.ForEach(x => x.Write(j, fftData));

          //Thread.Sleep(15);
          Thread.Sleep(20);
        }
      }
    }




  
  }
}
