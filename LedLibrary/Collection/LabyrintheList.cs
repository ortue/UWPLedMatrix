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
    public void AddNew(int x, int y)
    {
      if (!this.Any(l => l.X == x && l.Y == y))
        Add(new Labyrinthe(x, y));
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    public void Mouvement()
    {
      if (DX == 0 && DY == 0)
        Direction();
    }

    /// <summary>
    /// Direction
    /// </summary>
    private void Direction()
    {
      //DX = 0;
      //DY = 0;

      Random r = new Random();

      switch (r.Next(0, 3))
      {
        case 0:
          DX = -1;
          break;

        case 1:
          DX = 1;
          break;

        case 2:
          DY = -1;
          break;

        case 3:
          DY = 1;
          break;
      }


    }
  }
}