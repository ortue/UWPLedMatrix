using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LedLibrary.Collection
{
  public class LabyrintheList : List<Labyrinthe>
  {
    public LabyrintheList(PixelList pixels)
    {
      int i = 0;

      for (int y = 0; y < pixels.Hauteur; y++)
        for (int x = 0; x < pixels.Largeur; y++)
        {
          Add(new Labyrinthe(i++));
        }
    }
  }
}