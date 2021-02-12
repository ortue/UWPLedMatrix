using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class LabyrintheList : List<Labyrinthe>
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int DX { get; set; }
    public int DY { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public Labyrinthe Origine { get; set; }

    public bool Complet
    {
      get { return !this.Any(l => l.Chemin && l.CheminParcouru == 0); }
    }

    private bool Obstacle
    {
      get { return this.SingleOrDefault(l => l.X == X + DX && l.Y == Y + DY && l.Mur) != null; }
    }

    private bool Intersection
    {
      get { return this.SingleOrDefault(l => l.X == X && l.Y == Y && l.Intersection) != null; }
    }

    /// <summary>
    /// LabyrintheList
    /// </summary>
    public LabyrintheList()
    {

    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    /// <param name="hauteur"></param>
    public LabyrintheList(int largeur, int hauteur)
    {
      X = 2;
      Y = 2;
      Largeur = largeur;
      Hauteur = hauteur;
    }

    /// <summary>
    /// AddNew
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void AddNew(int x, int y, bool mur)
    {
      if (!this.Any(l => l.X == x && l.Y == y))
        Add(new Labyrinthe(x, y, mur));
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    public void Mouvement()
    {
      if (DX == 0 && DY == 0)
        Direction();

      if (Intersection)
        Direction();

      if (Obstacle)
        Direction();
      else
      {
        X += DX;
        Y += DY;

        SetCheminParcouru();
      }

      SetMurCouleur();
    }

    /// <summary>
    /// SetCheminParcouru
    /// </summary>
    public void SetCheminParcouru()
    {
      if (this.SingleOrDefault(l => l.X == X && l.Y == Y) is Labyrinthe labyrinthe)
        labyrinthe.CheminParcouru++;
    }

    /// <summary>
    /// SetMurCouleur
    /// </summary>
    private void SetMurCouleur()
    {
      for (int y = -2; y < 3; y++)
        for (int x = -2; x < 3; x++)
          foreach (Labyrinthe labyrinthe in this.Where(l => l.X == X + x && l.Y == Y + y && l.Mur))
          {
            int xx = Math.Abs(x);
            int yy = Math.Abs(y);

            if (xx == 0)
              xx = 1;

            if (yy == 0)
              yy = 1;

            labyrinthe.Couleur = Couleur.Get(127 / (xx * yy), 127 / (xx * yy), 127 / (xx * yy));
          }
    }

    /// <summary>
    /// Direction
    /// </summary>
    private void Direction()
    {
      DX = 0;
      DY = 0;

      LabyrintheList labyrinthes = new LabyrintheList();

      if (this.SingleOrDefault(l => l.X == X - 1 && l.Y == Y && l.Chemin) is Labyrinthe l1)
      {
        l1.DX = -1;
        l1.DY = 0;
        labyrinthes.Add(l1);
      }

      if (this.SingleOrDefault(l => l.X == X + 1 && l.Y == Y && l.Chemin) is Labyrinthe l2)
      {
        l2.DX = 1;
        l2.DY = 0;
        labyrinthes.Add(l2);
      }

      if (this.SingleOrDefault(l => l.X == X && l.Y == Y - 1 && l.Chemin) is Labyrinthe l3)
      {
        l3.DX = 0;
        l3.DY = -1;
        labyrinthes.Add(l3);
      }

      if (this.SingleOrDefault(l => l.X == X && l.Y == Y + 1 && l.Chemin) is Labyrinthe l4)
      {
        l4.DX = 0;
        l4.DY = 1;
        labyrinthes.Add(l4);
      }

      if (labyrinthes.OrderBy(l => l.CheminParcouru).FirstOrDefault() is Labyrinthe labyrinthe)
      {
        DX = labyrinthe.DX;
        DY = labyrinthe.DY;
      }
    }

    /// <summary>
    /// SetChemin
    /// </summary>
    public void SetChemin()
    {
      for (int y = 2; y < Hauteur; y++)
        for (int x = 2; x < Largeur; x++)
          if (this.SingleOrDefault(l => l.X == x && l.Y == y) == null)
            AddNew(x, y, false);

      foreach (Labyrinthe labyrinthe in this.Where(l => l.Chemin))
        labyrinthe.Intersection = GetIntersection(labyrinthe);
    }

    /// <summary>
    /// GetIntersection
    /// </summary>
    /// <param name="labyrinthe"></param>
    /// <returns></returns>
    private bool GetIntersection(Labyrinthe labyrinthe)
    {
      int intersection = 0;

      if (this.Any(l => l.X == labyrinthe.X + 1 && l.Y == labyrinthe.Y && l.Chemin))
        intersection++;

      if (this.Any(l => l.X == labyrinthe.X - 1 && l.Y == labyrinthe.Y && l.Chemin))
        intersection++;

      if (this.Any(l => l.X == labyrinthe.X && l.Y == labyrinthe.Y + 1 && l.Chemin))
        intersection++;

      if (this.Any(l => l.X == labyrinthe.X && l.Y == labyrinthe.Y - 1 && l.Chemin))
        intersection++;

      return intersection > 2;
    }
  }
}