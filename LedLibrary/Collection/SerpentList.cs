using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class SerpentList : List<Serpent>
  {
    public int DX { get; set; }
    public int DY { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }

    public Serpent Tete
    {
      get { return this.FirstOrDefault(); }
    }

    public Serpent Queue
    {
      get { return this.LastOrDefault(); }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public SerpentList(int largeur, int hauteur)
    {
      Largeur = largeur;
      Hauteur = hauteur;

      Random r = new Random();

      Add(new Serpent(r.Next(1, Largeur - 2), r.Next(1, Hauteur - 2)));
      Tete.Tete = true;
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    public void Mouvement()
    {
      Serpent tmpSerpent = null;

      foreach (Serpent serpent in this)
      {
        Serpent suivant = new Serpent(serpent);
        serpent.SetSerpent(tmpSerpent);
        tmpSerpent = new Serpent(suivant);
      }

      Tete.X += DX;
      Tete.Y += DY;

      if (Tete.X < 1)
        Tete.X = 1;

      if (Tete.X > Largeur - 1)
        Tete.X = Largeur - 1;

      if (Tete.Y < 1)
        Tete.Y = 1;

      if (Tete.Y > Largeur - 1)
        Tete.Y = Largeur - 1;
    }

    /// <summary>
    /// Mange
    /// </summary>
    public bool Mange()
    {
      Add(new Serpent(Queue.X, Queue.Y));

      return true;
    }

    /// <summary>
    /// Mort
    /// </summary>
    /// <returns></returns>
    public bool Mort()
    {
      return this.Any(s => s.X == Tete.X && s.Y == Tete.Y);
    }

    /// <summary>
    /// Obstable
    /// </summary>
    /// <returns></returns>
    public bool Obstable()
    {
      if (Tete.X + DX == 0 || Tete.X + DX >= Largeur - 1)
        return true;

      if (Tete.Y + DY == 0 || Tete.Y + DY >= Hauteur - 1)
        return true;

      return this.Any(s => s.X == Tete.X + DX && s.Y == Tete.Y + DY);
    }

    /// <summary>
    /// Obstable
    /// </summary>
    /// <param name="dx"></param>
    /// <param name="dy"></param>
    /// <returns></returns>
    public bool Obstable(int dx, int dy)
    {
      if (Tete.X + dx == 0 || Tete.X + dx >= Largeur - 1)
        return true;

      if (Tete.Y + dy == 0 || Tete.Y + dy >= Hauteur - 1)
        return true;

      return this.Any(s => s.X == Tete.X + dx && s.Y == Tete.Y + dy);
    }

    /// <summary>
    /// Possibilite
    /// </summary>
    /// <returns></returns>
    public List<KeyValuePair<int, int>> Possibilite()
    {
      List<KeyValuePair<int, int>> possibilite = new List<KeyValuePair<int, int>>();

      if (!Obstable(0, 1))
        possibilite.Add(new KeyValuePair<int, int>(0, 1));

      if (!Obstable(1, 0))
        possibilite.Add(new KeyValuePair<int, int>(1, 0));

      if (!Obstable(0, -1))
        possibilite.Add(new KeyValuePair<int, int>(0, -1));

      if (!Obstable(-1, 0))
        possibilite.Add(new KeyValuePair<int, int>(-1, 0));

      return possibilite;
    }
  }
}