using Library.Collection;

namespace Library.Entity
{
  public class Pixel
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int Numero { get; set; }
    public int Position { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="position">Position qu'on affiche une image a partir du coin haut droit vers le coin bas gauche</param>
    /// <param name="numero">Ordre qu'on envoit les pixels au rubans, du coin bas gauche en serpent vers le haut</param>
    /// <param name="i"></param>
    public Pixel(int position, int numero)
    {
      Position = position + 1;
      Numero = numero;

      X = position % PixelList.Largeur;
      Y = Convert.ToInt32(Math.Floor((decimal)(position / PixelList.Hauteur)));
    }
  }
}