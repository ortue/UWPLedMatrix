using Library.Entities;
using System.Collections.Generic;
using Windows.UI;

namespace Library.Collection
{
  public class CouleurList : List<Couleur>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="pixel"></param>
    public CouleurList(byte[] pixels)
    {

      int position = 0;

      for (int i = 0; i < pixels.Length; i = i + 4)
        if (pixels[i] == 255 && pixels[i + 1] == 255 && pixels[i + 2] == 255)
          Add(new Couleur(position++, Color.FromArgb(0, 0, 0, 0)));
        else
          Add(new Couleur(position++, Color.FromArgb(0, (byte)(pixels[i] / 2), (byte)(pixels[i + 1] / 2), (byte)(pixels[i + 2] / 2))));
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="result"></param>
    public CouleurList(List<byte[]> frames)
    {
      int frameCompteur = 0;

      foreach (byte[] frame in frames)
      {
        int position = 0;

        for (int i = 0; i < frame.Length; i = i + 4)
          if (frame[i] == 255 && frame[i + 1] == 255 && frame[i + 2] == 255)
            Add(new Couleur(frameCompteur, position++, Color.FromArgb(0, 0, 0, 0)));
          else
            Add(new Couleur(frameCompteur, position++, Color.FromArgb(0, (byte)(frame[i] / 2), (byte)(frame[i + 1] / 2), (byte)(frame[i + 2] / 2))));

        frameCompteur++;
      }
    }
  }
}