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
          EGC tmpEgc = new EGC(premier);

          premier.NextX(saut, i < Math.Abs(saut));

          foreach (EGC egc in this.Where(e => !e.Premier))
          {
            EGC dernierEgc = new EGC(egc);

            if (egc.X == premier.X - 1)
              egc.Couleur = Couleur.Get(32, 127, 32);
            else
            {
              int fade = 0;
              int distance = premier.Compteur - egc.Compteur;

              if (distance <= 5)
                fade = 3;

              if (distance > 5 && distance <= 10)
                fade = 2;

              if (distance > 10 && distance <= 15)
                fade = 1;

              egc.Couleur = Couleur.Get(fade, fade * 3, fade);
            }

            egc.X = tmpEgc.X;
            egc.Y = tmpEgc.Y;
            egc.Compteur = tmpEgc.Compteur;

            tmpEgc = new EGC(dernierEgc);
          }
        }
      }

      return battement;
    }
  }
}