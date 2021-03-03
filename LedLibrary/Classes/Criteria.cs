namespace LedLibrary.Classes
{
  public class Criteria
  {
    private int _cmbStroboscope;
    public int CmbStroboscope
    {
      get
      {
        if (_cmbStroboscope == 0)
          return 50;

        return _cmbStroboscope;
      }
      set { _cmbStroboscope = value; }
    }

    private float _cmbAmplitude;
    public float CmbAmplitude
    {
      get
      {
        if (_cmbAmplitude == 0)
          return 1f;

        return _cmbAmplitude;
      }
      set { _cmbAmplitude = value; }
    }

    public float AmplitudeSpectrum
    {
      get { return CmbAmplitude; }
    }

    public float AmplitudeGraph
    {
      get
      {
        if (CmbAmplitude > 2)
          return 2f / 3f;

        return CmbAmplitude / 3f;
      }
    }
  }
}