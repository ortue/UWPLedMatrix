using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class EGCList : List<EGC>
  {
    public int Largeur { get; set; }
    public int Hauteur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public EGCList(int nombre, int largeur, int hauteur)
    {
      Largeur = largeur;
      Hauteur = hauteur;

      for (int i = nombre; i > 0; i--)
        Add(new EGC(i == nombre, Largeur, Hauteur));
    }

    /// <summary>
    /// Next
    /// </summary>
    public bool Next(int rythm)
    {
      bool battement = false;

      if (rythm < 9)
        rythm = 9;

      if (this.FirstOrDefault() is EGC premier)
      {
        int saut = 0;

        if (premier.Compteur % rythm == 2)
          saut = 1;

        if (premier.Compteur % rythm == 3)
          saut = -1;

        if (premier.Compteur % rythm == 6)
        {
          battement = true;
          saut = 8;
        }

        if (premier.Compteur % rythm == 7)
        {
          battement = true;
          saut = -11;
        }

        if (premier.Compteur % rythm == 8)
          saut = 2;

        for (int i = 0; i <= Math.Abs(saut); i++)
        {
          int tmpX = premier.X;
          int tmpY = premier.Y;

          premier.NextX(saut, i < Math.Abs(saut));

          foreach (EGC egc in this.Where(e => !e.Premier))
          {
            int dernierX = egc.X;
            int dernierY = egc.Y;

            if (egc.X == premier.X - 1)
              egc.Couleur = Couleur.Get(32, 127, 32);
            else
              egc.Couleur = Couleur.Get(2, 4, 2);

            egc.X = tmpX;
            egc.Y = tmpY;

            tmpX = dernierX;
            tmpY = dernierY;
          }
        }
      }

      return battement;
    }
  }
}