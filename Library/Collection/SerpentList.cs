using Library.Entity;

namespace Library.Collection
{
  public class SerpentList : List<Serpent>
  {

    public int DX { get; set; }
    public int DY { get; set; }

    public Serpent? Tete
    {
      get { return this.FirstOrDefault(); }
    }

    public Serpent? Queue
    {
      get { return this.LastOrDefault(); }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public SerpentList()
    {
      Random r = new();

      Add(new Serpent(r.Next(1, PixelList.Largeur - 2), r.Next(1, PixelList.Hauteur - 2)));

      if (Tete is Serpent tete)
        tete.Tete = true;
    }

    /// <summary>
    /// Mouvement
    /// </summary>
    public void Mouvement()
    {
      Serpent? tmpSerpent = null;

      foreach (Serpent serpent in this)
      {
        Serpent suivant = new(serpent);
        serpent.SetSerpent(tmpSerpent);
        tmpSerpent = new Serpent(suivant);
      }

      if (Tete is Serpent tete)
      {
        tete.X += DX;
        tete.Y += DY;

        if (tete.X < 1)
          tete.X = 1;

        if (tete.X > PixelList.Largeur - 1)
          tete.X = PixelList.Largeur - 1;

        if (tete.Y < 1)
          tete.Y = 1;

        if (tete.Y > PixelList.Largeur - 1)
          tete.Y = PixelList.Largeur - 1;
      }
    }

    /// <summary>
    /// Mange
    /// </summary>
    public bool Mange()
    {
      Add(new Serpent(Queue));

      return true;
    }

    /// <summary>
    /// Mort
    /// </summary>
    /// <returns></returns>
    public bool Mort()
    {
      return this.Any(s => s.X == Tete?.X && s.Y == Tete.Y);
    }

    /// <summary>
    /// Obstable
    /// </summary>
    /// <returns></returns>
    public bool Obstacle()
    {
      if (Tete?.X + DX == 0 || Tete?.X + DX >= PixelList.Largeur - 1)
        return true;

      if (Tete?.Y + DY == 0 || Tete?.Y + DY >= PixelList.Hauteur - 1)
        return true;

      return this.Any(s => s.X == Tete?.X + DX && s.Y == Tete?.Y + DY);
    }

    /// <summary>
    /// Obstacle
    /// </summary>
    /// <param name="dx"></param>
    /// <param name="dy"></param>
    /// <returns></returns>
    public bool Obstacle(int dx, int dy)
    {
      if (Tete?.X + dx == 0 || Tete?.X + dx >= PixelList.Largeur - 1)
        return true;

      if (Tete?.Y + dy == 0 || Tete?.Y + dy >= PixelList.Hauteur - 1)
        return true;

      return this.Any(s => s.X == Tete?.X + dx && s.Y == Tete?.Y + dy);
    }

    /// <summary>
    /// Possibilite
    /// </summary>
    /// <returns></returns>
    public List<KeyValuePair<int, int>> Possibilite()
    {
      List<KeyValuePair<int, int>> possibilite = new();

      if (!Obstacle(0, 1))
        possibilite.Add(new KeyValuePair<int, int>(0, 1));

      if (!Obstacle(1, 0))
        possibilite.Add(new KeyValuePair<int, int>(1, 0));

      if (!Obstacle(0, -1))
        possibilite.Add(new KeyValuePair<int, int>(0, -1));

      if (!Obstacle(-1, 0))
        possibilite.Add(new KeyValuePair<int, int>(-1, 0));

      return possibilite;
    }
  }
}