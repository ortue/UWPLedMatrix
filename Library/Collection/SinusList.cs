﻿using Library.Entity;

namespace Library.Collection
{
  public class SinusList : List<Sinus>
  {
    public bool Manette { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="qty"></param>
    public SinusList(int qty)
    {
      for (int i = 0; i < qty; i++)
        Add(new Sinus());
    }

    /// <summary>
    /// Next
    /// </summary>
    public void Next()
    {
      foreach (Sinus sin in this)
        sin.Next();
    }
  }
}