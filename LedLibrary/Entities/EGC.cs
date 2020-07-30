namespace LedLibrary.Entities
{
  public class EGC
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Compteur { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public bool Premier { get; set; }
    public Couleur Couleur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public EGC(bool premier, int largeur, int hauteur)
    {
      X = 0;
      Y = 14;
      Compteur = 0;
      Largeur = largeur;
      Hauteur = hauteur;
      Premier = premier;

      if (Premier)
        Couleur = Couleur.Get(32, 127, 32);
      else
        Couleur = Couleur.Get(2, 4, 2);
    }

    /// <summary>
    /// NextX
    /// </summary>
    /// <param name="stop"></param>
    public void NextX(int saut, bool stop)
    {
      if (saut > 0 && Y > 0)
        Y--;

      if (saut < 0 && Y < Hauteur - 1)
        Y++;

      if (!stop)
        X = Compteur++ % Largeur;
    }
  }
}