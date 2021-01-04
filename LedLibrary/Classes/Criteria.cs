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
  }
}