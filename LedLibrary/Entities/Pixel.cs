using System;

namespace LedLibrary.Entities
{
  public class Pixel
  {
    public int Numero { get; set; }
    public int Position { get; set; }
    public Coordonnee Coord { get; set; }
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }

    public Couleur Couleur
    {
      get { return Couleur.FromArgb(Red, Green, Blue); }
      set
      {
        Red = value.R;
        Green = value.G;
        Blue = value.B;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="position"></param>
    /// <param name="numero"></param>
    /// <param name="coord"></param>
    public Pixel(int position, int numero, Coordonnee coord)
    {
      Mapping(position, numero, coord);
    }

    /// <summary>
    /// Mapping
    /// </summary>
    /// <param name="position"></param>
    private void Mapping(int position, int numero, Coordonnee coord)
    {
      Position = position;
      Numero = numero;
      Coord = coord;
    }

    /// <summary>
    /// SetColor rien
    /// </summary>
    public void SetColor()
    {
      Couleur = new Couleur();
    }

    /// <summary>
    /// SetColor
    /// </summary>
    /// <param name="couleur"></param>
    public void SetColor(Couleur couleur)
    {
      Couleur = couleur;
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    public void Set(int r, int g, int b)
    {
      Couleur = new Couleur { R = (byte)r, G = (byte)g, B = (byte)b };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fade"></param>
    public void Fade(int fade)
    {
      Red = (byte)(Red / (byte)fade);
      Green = (byte)(Green / (byte)fade);
      Blue = (byte)(Blue / (byte)fade);
    }
  }
}