using Library.Collection;
using Library.Entity;
using Library.Util;

namespace BLedMatrix.Shared
{
  public partial class Horloge
  {
    public Couleur DixiemeSecondeCouleur { get; set; } = Couleur.Rouge;
    public Couleur SecondeCouleur { get; set; } = Couleur.Get(40, 40, 60);
    public Couleur MinuteCouleur { get; set; } = Couleur.Get(20, 74, 74);
    public Couleur HeureCouleur { get; set; } = Couleur.Get(74, 100, 40);
    public Couleur CadranCouleur { get; set; } = Couleur.Get(40, 40, 60);
    public Couleur PointCouleur { get; set; } = Couleur.Get(30, 40, 20);

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      DixiemeSecondeCouleur = Couleurs.Get("Horloge", "DixiemeSecondeCouleur", Couleur.Rouge);
      SecondeCouleur = Couleurs.Get("Horloge", "SecondeCouleur", Couleur.Get(40, 40, 60));
      PointCouleur = Couleurs.Get("Horloge", "PointCouleur", Couleur.Get(30, 40, 20));
      MinuteCouleur = Couleurs.Get("Horloge", "MinuteCouleur", Couleur.Get(20, 74, 74));
      HeureCouleur = Couleurs.Get("Horloge", "HeureCouleur", Couleur.Get(74, 100, 40));
      CadranCouleur = Couleurs.Get("Horloge", "CadranCouleur", Couleur.Noir);

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

      int task = TaskGo.StartTask("Horloge");
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
      //5 minutes
      for (int i = 0; i < 60; i += 5)
        Pixels.Get(PixelList.GetTempsCoord(i, 10)).SetColor(PointCouleur);

      Pixels.Get(9, 0).SetColor(PointCouleur);
      Pixels.Get(9, 19).SetColor(PointCouleur);

      //cycle = 
      //Background.Grichage(Pixels);
      Background.Bleu(Pixels, false);

      Pixels.Set(CaractereList.Print(CaractereList.Heure, 1, 13, CadranCouleur));

      //Aiguille
      int minute = DateTime.Now.Minute;
      int heure = DateTime.Now.Hour;

      for (int i = 0; i < 9; i++)
        Pixels.Get(PixelList.GetTempsCoord(DateTime.Now.Second + DateTime.Now.Millisecond / (double)1000, i)).SetColor(SecondeCouleur);

      for (int i = 0; i < 8; i++)
        Pixels.Get(PixelList.GetTempsCoord(minute + DateTime.Now.Second * 1.6 / 100, i)).SetColor(MinuteCouleur);

      for (int i = 0; i < 6; i++)
        Pixels.Get(PixelList.GetHeureCoord(heure, minute, i)).SetColor(HeureCouleur);

      try
      {
        for (int i = 0; i < 9; i++)
          Pixels.Get(PixelList.GetTempsCoord((DateTime.Now.Millisecond / (double)100 * 6) - i, 9)).SetColor(Couleur.Get(DixiemeSecondeCouleur.R / (i + 1), DixiemeSecondeCouleur.G / (i + 1), DixiemeSecondeCouleur.B / (i + 1)));
      }
      catch (Exception ex)
      {
        var a = ex.ToString();
      }

      return cycle;
    }
  }
}