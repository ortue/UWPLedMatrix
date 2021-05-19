using System;

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

    public static bool AffHeure { get; set; }
    public static bool AffTitre { get; set; }
    public string BtnHeure { get; set; }
    public string BtnTitre { get; set; }

    public void SetBtn()
    {
      if (BtnHeure == "Heure")
        AffHeure = !AffHeure;

      if (BtnTitre == "Titre")
        AffTitre = !AffTitre;
    }
  }
}