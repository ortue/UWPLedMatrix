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

    public static bool Spectrum { get; set; }
    public static bool AffHeure { get; set; }
    public static bool AffTitre { get; set; }
    public string BtnHeure { get; set; }
    public string BtnTitre { get; set; }

    public static int CycleMod
    {
      get
      {
        int cycleMod = 12;

        if (AffHeure)
          cycleMod -= 2;

        if (Spectrum)
          cycleMod -= 3;

        return cycleMod;
      }
    }

    public void SetBtn()
    {
      Spectrum = false;

      if (BtnHeure == "Heure")
        AffHeure = !AffHeure;

      if (BtnTitre == "Titre")
        AffTitre = !AffTitre;
    }
  }
}