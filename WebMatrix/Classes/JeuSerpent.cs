using LedLibrary.Collection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMatrix.Classes
{
  public class JeuSerpent
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public int Score { get; set; }
    public int Vitesse { get; set; }
    public SerpentList Serpents { get; set; }

    public int DistanceX
    {
      get { return Math.Abs(Serpents.Tete.X - X); }
    }

    public int DistanceY
    {
      get { return Math.Abs(Serpents.Tete.Y - Y); }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public JeuSerpent(int largeur, int hauteur)
    {
      Largeur = largeur;
      Hauteur = hauteur;

      Serpents = new SerpentList(Largeur, Hauteur);
      SetBalle();
    }

    /// <summary>
    /// SetBalle
    /// </summary>
    public void SetBalle()
    {
      int i = 0;
      Random r = new Random();

      X = r.Next(1, Largeur - 1);
      Y = r.Next(1, Hauteur - 1);

      while (Serpents.Any(s => s.X == X && s.Y == Y) && i++ < 5000)
      {
        X = r.Next(1, Largeur - 1);
        Y = r.Next(1, Hauteur - 1);
      }

      Serpents.DX = 0;
      Serpents.DY = 0;
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    public bool Mouvement()
    {
      //Depart du serpent
      if (Serpents.DX == 0 && Serpents.DY == 0)
        Direction();

      //Distance transversal atteint
      if (DistanceX == 0 || DistanceY == 0)
        Direction();

      //Eviter obstacle
      if (Serpents.Obstable())
        if (Serpents.Possibilite() is List<KeyValuePair<int, int>> possibilites)
        {
          if (!possibilites.Any())
          {
            Mort();
            return true;
          }

          Random r = new Random();
          int choix = r.Next(0, possibilites.Count - 1);
          Direction(possibilites[choix].Key, possibilites[choix].Value);
        }

      Serpents.Mouvement();

      return false;
    }

    /// <summary>
    /// Direction
    /// </summary>
    private void Direction()
    {
      Serpents.DX = 0;
      Serpents.DY = 0;

      if (DistanceX <= DistanceY)
      {
        if (Serpents.Tete.Y < Y)
          Serpents.DY = 1;

        if (Serpents.Tete.Y > Y)
          Serpents.DY = -1;
      }
      else
      {
        if (Serpents.Tete.X < X)
          Serpents.DX = 1;

        if (Serpents.Tete.X > X)
          Serpents.DX = -1;
      }
    }

    /// <summary>
    /// Direction
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void Direction(int x, int y)
    {
      Serpents.DX = x;
      Serpents.DY = y;
    }

    /// <summary>
    /// Manger
    /// </summary>
    public bool Manger()
    {
      if (Serpents.Tete.X == X && Serpents.Tete.Y == Y)
      {
        Score++;
        SetBalle();
        Serpents.Mange();

        return true;
      }

      return false;
    }

    /// <summary>
    /// Mort
    /// </summary>
    /// <returns></returns>
    public void Mort()
    {
      Score = 0;
      SetBalle();
      Serpents = new SerpentList(Largeur, Hauteur);
    }
  }
}