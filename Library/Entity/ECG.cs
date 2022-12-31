namespace Library.Entity
{
  public class ECG  
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Compteur { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public bool Premier { get; set; }
    public Couleur Couleur { get; set; } = new Couleur();

    /// <summary>
    /// Constructeur
    /// </summary>
    public ECG(ECG egc)
    {
      Mapping(egc);
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public ECG(bool premier, int largeur, int hauteur)
    {
      X = 0;
      Y = 14;
      Compteur = 0;
      Largeur = largeur;
      Hauteur = hauteur;
      Premier = premier;

      if (Premier)
        Couleur = Couleur.Get(32, 127, 32);
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

    /// <summary>
    /// Mapping
    /// </summary>
    /// <param name="egc"></param>
    private void Mapping(ECG egc)
    {
      X = egc.X;
      Y = egc.Y;
      Compteur = egc.Compteur;
    }
  }
}