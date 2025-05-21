using Library.Collection;

namespace Library.Util
{
  public class AudioAnalyzer
  {
    //private readonly int _bandCount;
    //private readonly int _sampleRate;
    //private readonly int _fftSize;
    //private readonly float[] _previousValues;
    //private readonly float _decayFactor = 0.05f;

    public float ScalingFactor { get; private set; } = 1.0f;

    public AudioAnalyzer()
    {
      //_bandCount = bandCount;
      //_sampleRate = sampleRate;
      //_fftSize = fftSize;
      //_previousValues = new float[PixelList.Largeur];
    }

    public float[] Analyze(float[] fftMagnitude)
    {
      var bandAmplitudes = new float[PixelList.Largeur];
      int binPerBand = fftMagnitude.Length / PixelList.Largeur;

      for (int i = 0; i < PixelList.Largeur; i++)
      {
        float sum = 0;

        for (int j = i * binPerBand; j < (i + 1) * binPerBand && j < fftMagnitude.Length; j++)
        {
          sum += fftMagnitude[j];
        }

        float avg = sum / binPerBand;

        // Scaling factor auto-ajusté
        ScalingFactor = Math.Max(ScalingFactor * 0.99f, avg);
        //float scaled = avg / (ScalingFactor + 1e-6f);

        // Appliquer un effet de décroissance douce
        //if (scaled < _previousValues[i])
        //  bandAmplitudes[i] = _previousValues[i] - _decayFactor;
        //else


        bandAmplitudes[i] = avg / (ScalingFactor + 1e-6f);

        // Clamp à [0,1]
        bandAmplitudes[i] = Math.Max(0, Math.Min(1, bandAmplitudes[i]));
        //_previousValues[i] = bandAmplitudes[i];
      }

      return bandAmplitudes;
    }
  }
}