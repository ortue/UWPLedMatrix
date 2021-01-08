using LedLibrary.Collection;
using System;
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
      Random r = new Random();

      int i = 0;

      X = r.Next(1, Largeur - 1);
      Y = r.Next(1, Hauteur - 1);

      while (Serpents.Any(s => s.X == X && s.Y == Y) && i++ < 5000)
      {
        X = r.Next(1, Largeur - 1);
        Y = r.Next(1, Hauteur - 1);
      }
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    public void Mouvement()
    {
      Direction();

      Serpents.Mouvement();
    }

    /// <summary>
    /// Direction
    /// </summary>
    public void Direction()
    {
      if (Serpents.DX == 0 && Serpents.DY == 0)
      {
      
      
      }




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
    public bool Mort()
    {
      if (Serpents.Mort())
      {
        Score = 0;
        SetBalle();
        Serpents = new SerpentList(Largeur, Hauteur);

        return true;
      }

      return false;
    }
  }
}