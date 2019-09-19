using System.Collections.Generic;

namespace Library.Classes
{
  public class Emplacement : List<int>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    /// <param name="hauteur"></param>
    public Emplacement(int largeur, int hauteur)
    {
      for (int j = 0; j < hauteur; j++)
        for (int i = 1; i <= largeur; i++)
        {
          if (i % 2 == 0)
            Add(largeur * (i - 1) + 1 + j);
          else
            Add(largeur * i - j);
        }
    }

    /// <summary>
    /// Convertion
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public int Convertion(int position)
    {
      return this[position];
    }
  }
}