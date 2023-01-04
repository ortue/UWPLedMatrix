namespace Library.Entity
{
  public class Brique
  {
    public int X { get; set; }
    public int XX { get; set; }
    public int Y { get; set; }
    public bool Visible { get; set; }

    public Couleur Couleur { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="i"></param>
    /// <param name="ranger"></param>
    public Brique(int i, int ranger)
    {
      X = i + 1;
      XX = i + 2;
      Y = ranger + 3;
      Visible = true;

      Couleur = ranger switch
      {
        0 => Couleur = Couleur.Get(63, 63, 127),
        1 => Couleur = Couleur.Get(63, 63, 127),
        2 => Couleur = Couleur.Get(63, 127, 63),
        3 => Couleur = Couleur.Get(63, 127, 63),
        4 => Couleur = Couleur.Get(127, 63, 127),
        5 => Couleur = Couleur.Get(127, 63, 127),
        _ => Couleur = Couleur.Get(127, 63, 127)
      };
    }
  }
}