using Library.Collection;
using Library.Entity;
using Library.Util;

namespace BLedMatrix.Shared
{
  public partial class Horloge
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecHorloge);
    }

    /// <summary>
    /// Horloge
    /// </summary>
    private void ExecHorloge()
    {
      double cycle = 0;
      //Random r = new();
      //int bg = r.Next(1, Util.Context.Pixels.NbrBackground);

      ////Aime pas beaucoup le 3 et le 4 ici
      //if (bg == 3 || bg == 4)
      //  bg = 2;

      int task = TaskGo.StartTask();
      //CaractereList caracteres = new(20);

      while (TaskGo.TaskWork(task))
      {
        //caracteres.SetText(Heure);
        SetHorloge(cycle);
        Pixels.SendPixels();
        Pixels.Reset();
      }
    }

    /// <summary>
    /// SetHorloge
    /// </summary>
    /// <returns></returns>
    public double SetHorloge(double cycle)
    {
      Couleur minuteCouleur = new() { R = 39 / 2, G = 144 / 2, B = 176 / 2 };//39,144,176
      Couleur heureCouleur = new() { R = 148 / 2, G = 200 / 2, B = 80 / 2 };//148,186,101
      Couleur pointCouleur = new() { R = 148 / 5, G = 200 / 5, B = 80 / 5 };//148,186,101

      //5 minutes
      for (int i = 0; i < 60; i += 5)
        Pixels.Get(PixelList.GetTempsCoord(i, 10)).SetColor(pointCouleur);

      Pixels.Get(9, 0).SetColor(pointCouleur);
      Pixels.Get(9, 19).SetColor(pointCouleur);

      //cycle = 
      //Background.Grichage(Pixels);
      Background.Bleu(Pixels,false);

      Pixels.Set(CaractereList.Print(CaractereList.Heure, 1, 13, Couleur.Noir));
      //Print(caracteres, 1, 13, Couleur.Noir);

      //Aiguille
      int minute = DateTime.Now.Minute;
      int heure = DateTime.Now.Hour;

      for (int i = 0; i < 9; i++)
        Pixels.Get(PixelList.GetTempsCoord(DateTime.Now.Second + DateTime.Now.Millisecond / (double)1000, i)).SetColor(new Couleur { R = 80 / 2, G = 78 / 2, B = 114 / 2 });//43,78,114

      for (int i = 0; i < 8; i++)
        Pixels.Get(PixelList.GetTempsCoord(minute + DateTime.Now.Second * 1.6 / 100, i)).SetColor(minuteCouleur);

      for (int i = 0; i < 6; i++)
        Pixels.Get(PixelList.GetHeureCoord(heure, minute, i)).SetColor(heureCouleur);

      for (int i = 0; i < 9; i++)
        Pixels.Get(PixelList.GetTempsCoord((DateTime.Now.Millisecond / (double)100 * 6) - i, 9)).SetColor(new Couleur { R = new List<byte> { 128, 64, 32, 16, 8, 8, 8, 4, 4 }[i], B = 5 });

      return cycle;
    }
  }
}