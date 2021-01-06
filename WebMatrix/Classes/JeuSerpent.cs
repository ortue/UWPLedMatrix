using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMatrix.Classes
{
  public class JeuSerpent
  {
    public decimal X { get; set; }
    public decimal Y { get; set; }
    public int Score { get; set; }
    public int Vitesse { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public JeuSerpent(int largeur, int hauteur)
    {
      Random r = new Random();

      X = r.Next(1, largeur - 2);
      Y = r.Next(1, hauteur - 2);

    }


  }
}
