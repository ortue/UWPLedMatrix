using LedLibrary.Collection;
using System;
using WebMatrix.Classes;

namespace WebMatrix.Context
{
  public class LedMatrixContext
  {
    public PixelList Pixels { get; set; }
    public DotStarStrip PixelStrip { get; set; }
    public bool Autorun { get; set; }

    /// <summary>
    /// Nombre de Led de Largeur par defaut
    /// </summary>
    public int Largeur
    {
      get { return 20; }
    }

    /// <summary>
    /// Nombre de Led de Longeur par defaut
    /// </summary>
    public int Hauteur
    {
      get { return 20; }
    }

    /// <summary>
    /// Nombre de Led par defaut
    /// </summary>
    public int NbrLed
    {
      get { return Largeur * Hauteur; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public LedMatrixContext()
    {
      if (Environment.MachineName != "PC-BENOIT")
        PixelStrip = new DotStarStrip(NbrLed);

      Pixels = new PixelList(Largeur, Hauteur);
    }
  }
}