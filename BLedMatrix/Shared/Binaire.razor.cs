using Library.Collection;
using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class Binaire
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecBinaire);
    }

    /// <summary>
    /// Binaire
    /// </summary>
    private void ExecBinaire()
    {
      //decimal cycle = 0;
      //Random r = new();
      //int bg = r.Next(1, Util.Context.Pixels.NbrBackground);

      //Aime pas beaucoup le 3 et le 4 ici
      //if (bg == 3 || bg == 4)
      //  bg = 2;

      int task = TaskGo.StartTask();
      //taskCaractereList caracteres = new(20);

      while (TaskGo.TaskWork(task))
      {
        SetBinaire();

        //caracteres.SetText(Heure);
        //Util.Context.Pixels.SetBinaire(caracteres.GetCaracteres());
        //cycle = Util.Context.Pixels.BackGround(bg, cycle);
        Pixels.SendPixels();
        Pixels.Reset();
      }
    }

    /// <summary>
    /// SetBinaire
    /// </summary>
    /// <param name="caracteres"></param>
    public void SetBinaire()
    {
      Couleur couleur = Couleur.Get(31, 64, 127);
      Couleur noir = Couleur.Get(1, 1, 1);

      string heure = Convert.ToString(DateTime.Now.Hour, 2).PadLeft(6, '0');
      string minute = Convert.ToString(DateTime.Now.Minute, 2).PadLeft(6, '0');
      string seconde = Convert.ToString(DateTime.Now.Second, 2).PadLeft(6, '0');

      for (int h = 0; h < 6; h++)
        if (heure[h] == '1')
        {
          Pixels.Get(2, h * 2 + 1).SetColor(couleur);
          Pixels.Get(3, h * 2 + 1).SetColor(couleur);
          Pixels.Get(4, h * 2 + 1).SetColor(couleur);
        }
        else
        {
          Pixels.Get(2, h * 2 + 1).SetColor(noir);
          Pixels.Get(3, h * 2 + 1).SetColor(noir);
          Pixels.Get(4, h * 2 + 1).SetColor(noir);
        }

      for (int m = 0; m < 6; m++)
        if (minute[m] == '1')
        {
          Pixels.Get(9, m * 2 + 1).SetColor(couleur);
          Pixels.Get(10, m * 2 + 1).SetColor(couleur);
          Pixels.Get(11, m * 2 + 1).SetColor(couleur);
        }
        else
        {
          Pixels.Get(9, m * 2 + 1).SetColor(noir);
          Pixels.Get(10, m * 2 + 1).SetColor(noir);
          Pixels.Get(11, m * 2 + 1).SetColor(noir);
        }

      for (int s = 0; s < 6; s++)
        if (seconde[s] == '1')
        {
          Pixels.Get(16, s * 2 + 1).SetColor(couleur);
          Pixels.Get(17, s * 2 + 1).SetColor(couleur);
          Pixels.Get(18, s * 2 + 1).SetColor(couleur);
        }
        else
        {
          Pixels.Get(16, s * 2 + 1).SetColor(noir);
          Pixels.Get(17, s * 2 + 1).SetColor(noir);
          Pixels.Get(18, s * 2 + 1).SetColor(noir);
        }

      //Print(caracteres, 2, 14, couleur);

      Pixels.Set(CaractereList.Print(CaractereList.Heure, 2, 14, couleur));
    }
  }
}