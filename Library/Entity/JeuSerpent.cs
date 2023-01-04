using Library.Collection;

namespace Library.Entity
{
  public class JeuSerpent
  {

    public int X { get; set; }
    public int Y { get; set; }
    public int Score { get; set; }
    public SerpentList Serpents { get; set; }

    public double Vitesse
    {
      get { return 10 - Score / 6; }
    }

    public int DistanceX
    {
      get
      {
        if (Serpents.Tete != null)
          return Math.Abs(Serpents.Tete.X - X);

        return 0;
      }
    }

    public int DistanceY
    {
      get 
      {
        if (Serpents.Tete != null)
          return Math.Abs(Serpents.Tete.Y - Y);

        return 0;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public JeuSerpent()
    {
      Serpents = new SerpentList();
      SetBalle();
    }

    /// <summary>
    /// SetBalle
    /// </summary>
    public void SetBalle()
    {
      int i = 0;
      Random r = new();

      X = r.Next(1, PixelList.Largeur - 1);
      Y = r.Next(1, PixelList.Hauteur - 1);

      while (Serpents.Any(s => s.X == X && s.Y == Y) && i++ < 5000)
      {
        X = r.Next(1, PixelList.Largeur - 1);
        Y = r.Next(1, PixelList.Hauteur - 1);
      }

      Serpents.DX = 0;
      Serpents.DY = 0;
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    public bool Mouvement(int cycle)
    {
      if (cycle++ % Vitesse == 0)
      {
        //Depart du serpent
        if (Serpents.DX == 0 && Serpents.DY == 0)
          Direction();

        //Distance transversal atteint
        if (DistanceX == 0 || DistanceY == 0)
          Direction();

        //Eviter obstacle
        if (Serpents.Obstacle())
          if (Serpents.Possibilite() is List<KeyValuePair<int, int>> possibilites)
          {
            if (!possibilites.Any())
              return Mort();

            Random r = new();
            int choix = r.Next(0, possibilites.Count);
            Direction(possibilites[choix].Key, possibilites[choix].Value);
          }

        Serpents.Mouvement();
      }

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
        if (Serpents.Tete?.Y < Y)
          Serpents.DY = 1;

        if (Serpents.Tete?.Y > Y)
          Serpents.DY = -1;
      }
      else
      {
        if (Serpents.Tete?.X < X)
          Serpents.DX = 1;

        if (Serpents.Tete?.X > X)
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
      if (Serpents.Tete?.X == X && Serpents.Tete?.Y == Y)
      {
        Score++;
        SetBalle();

        return Serpents.Mange();
      }

      return false;
    }

    /// <summary>
    /// Mort
    /// </summary>
    /// <returns></returns>
    public bool Mort()
    {
      Score = 0;
      SetBalle();
      Serpents = new SerpentList();

      return true;
    }
  }
}