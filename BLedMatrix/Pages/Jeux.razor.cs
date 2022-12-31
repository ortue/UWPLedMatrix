using Library.Entity;

namespace BLedMatrix.Pages
{
  public partial class Jeux
  {
    private void Set(int x, int y)
    {
      Pixels.Get(x, y).SetColor(Couleur.Rouge);

      Pixels.SendPixels();
    }

  }
}