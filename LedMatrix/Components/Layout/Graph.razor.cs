﻿using Library.Collection;
using Library.Entity;
using Library.Util;
using System.Data;

namespace LedMatrix.Components.Layout
{
  public partial class Graph
  {
    private PixelList TabSpec { get; set; } = new PixelList(false);

    protected override async Task OnInitializedAsync()
    {
      await Task.CompletedTask;
    }

    /// <summary>
    /// Set
    /// </summary>
    private void Set(int option)
    {
      Task.Run(() =>
      {
        int i = 0;
        TaskGo.StopTask();

        using ManualResetEventSlim waitHandle = new(false);

        while (ARecord.IsBusy || i++ > 100)
          waitHandle.Wait(TimeSpan.FromMilliseconds(100));

        ExecGraph(option);
      });
    }

    /// <summary>
    /// Graph
    /// </summary>
    private void ExecGraph(int option)
    {
      int task = TaskGo.StartTask("Graph");
      int cycle = 0;
      int debut = -20;

      using ARecord aRecord = new(256);

      while (TaskGo.TaskWork(task))
      {
        double[] samples = aRecord.Read();

        //facteur de volume a améliorer
        double amplitude = GetAmplitudeGraph(samples);

        GetGraph(samples, amplitude, option);
        AffHeure();
        debut = AffTitre(cycle++, debut);

        foreach (Pixel spec in TabSpec)
        {
          Couleur couleur = spec.Position switch
          {
            1 => Couleurs.Get("Graph", "HeureCouleur", Couleur.RougePale),
            2 => Couleurs.Get("Graph", "HeureAltCouleur", Couleur.Rouge),
            3 => Couleurs.Get("Graph", "TitreCouleur", Couleur.RougePale),
            4 => Couleurs.Get("Graph", "TitreAltCouleur", Couleur.Rouge),
            10 => Couleur.Noir,
            _ => spec.Couleur,
          };

          Pixels.Get(spec).SetColor(couleur);
        }

        Pixels.SendPixels();

        if (option == 0)
          TabSpec.Reset();
        else
          Fade();
      }
    }

    /// <summary>
    /// Fade
    /// </summary>
    private void Fade()
    {
      foreach (Pixel pixel in TabSpec.Where(p => !p.Couleur.IsNoir))
      {
        int r = pixel.Couleur.R - 1;
        int g = pixel.Couleur.G - 1;
        int b = pixel.Couleur.B - 1;

        if (r < 0)
          r = 0;

        if (g < 0)
          g = 0;

        if (b < 0)
          b = 0;

        pixel.SetColor(Couleur.Get(r, g, b));
      }
    }

    /// <summary>
    /// GetAmplitudeGraph
    /// </summary>
    /// <param name="fft"></param>
    /// <returns></returns>
    private static double GetAmplitudeGraph(double[] fft)
    {
      double max = fft.Max(Math.Abs);

      return max switch
      {
        > 75 => 0.1,
        > 50 => 0.2,
        > 25 => 0.25,
        > 15 => 0.5,
        > 10 => 0.7,
        > 5 => 0.6,
        > 4 => 0.7,
        > 3 => 0.8,
        > 1 => 0.9,
        _ => 0.5
      };
    }

    /// <summary>
    /// Graph
    /// </summary>
    /// <param name="audioBuffer"></param>
    /// <param name="fft"></param>
    private void GetGraph(double[] fft, double amplitude, int option)
    {
      //elargir les vagues en sautant des elements
      int facteur = 4;

      for (int x = 0; x < PixelList.Largeur; x++)
      {
        int y = (int)(fft[x * facteur] * amplitude) + 10;
        double distance = Math.Abs(fft[x * facteur] * amplitude);

        if (option == 1)
        {
          int yy = (int)(-fft[x * facteur] * amplitude) + 10;
          int minY = Math.Min(y, yy);
          int maxY = Math.Max(y, yy);

          for (int yyy = minY; yyy <= maxY; yyy++)
            if (TabSpec.Get(x, yyy) is Pixel pixel)
              pixel.SetColor(ProportionCouleur(Math.Abs(yyy - 10)));

          if (TabSpec.Get(x, y) is Pixel p)
            p.SetColor(ProportionCouleur(Math.Floor(distance)));
        }
        else if (TabSpec.Get(x, y) is Pixel pixel)
          pixel.SetColor(ProportionCouleur(Math.Floor(distance)));
      }
    }

    /// <summary>
    /// ProportionCouleur
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    private Couleur ProportionCouleur(int distance)
    {
      double facteurCentre = (10d - distance) / 10d;
      double facteurExtremite = distance / 10d;

      int r = (int)Math.Floor(((Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).R * facteurCentre) + (Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Rouge).R * facteurExtremite)) * 0.5d);
      int g = (int)Math.Floor(((Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).G * facteurCentre) + (Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Rouge).G * facteurExtremite)) * 0.5d);
      int b = (int)Math.Floor(((Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).B * facteurCentre) + (Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Rouge).B * facteurExtremite)) * 0.5d);

      return Couleur.Get(r, g, b);
    }

    /// <summary>
    /// ProportionCouleur
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    private Couleur ProportionCouleur(double distance) //, int facteur = 0
    {
      double facteurCentre = (10d - distance) / 10d;
      double facteurExtremite = distance / 10d;

      //int rC = Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).R - (int)Math.Floor(facteur * facteurCentre);
      //int gC = Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).G - (int)Math.Floor(facteur * facteurCentre);
      //int bC = Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).B - (int)Math.Floor(facteur * facteurCentre);

      //int rE = Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Bleu).R - (int)Math.Floor(facteur * facteurExtremite);
      //int gE = Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Bleu).G - (int)Math.Floor(facteur * facteurExtremite);
      //int bE = Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Bleu).B - (int)Math.Floor(facteur * facteurExtremite);

      //int r = (int)Math.Floor((rC * facteurCentre) + (rE * facteurExtremite)) + facteur;
      //int g = (int)Math.Floor((gC * facteurCentre) + (gE * facteurExtremite)) + facteur;
      //int b = (int)Math.Floor((bC * facteurCentre) + (bE * facteurExtremite)) + facteur;


      int r = (int)Math.Floor((Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).R * facteurCentre) + (Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Rouge).R * facteurExtremite));// + facteur;
      int g = (int)Math.Floor((Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).G * facteurCentre) + (Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Rouge).G * facteurExtremite));// + facteur;
      int b = (int)Math.Floor((Couleurs.Get("Graph", "CentreCouleur", Couleur.Bleu).B * facteurCentre) + (Couleurs.Get("Graph", "ExtremiteCouleur", Couleur.Rouge).B * facteurExtremite));// + facteur;

      return Couleur.Get(r, g, b);
    }

    /// <summary>
    /// AffHeure
    /// </summary>
    private void AffHeure()
    {
      foreach (Pixel pixel in TabSpec.Where(p => (p.Position == 1 || p.Position == 2) && p.Y > 12))
        pixel.Position = 0;

      if (TaskGo.HeureMusique)
      {
        CaractereList textes = new(PixelList.Largeur);
        textes.SetText(CaractereList.Heure);

        foreach (Police lettre in textes.GetCaracteres().Where(c => c.Point))
          if (TabSpec.Get(lettre.X + 1, lettre.Y + 13) is Pixel pixel)
            if (pixel.Couleur.IsNoir)// || pixel.Couleur.R == 127)
              pixel.Position = 1;
            else
              pixel.Position = 2;
      }
    }

    /// <summary>
    /// Afficher titre
    /// </summary>
    private int AffTitre(int cycle, int debut)
    {
      foreach (Pixel pixel in TabSpec.Where(p => (p.Position == 3 || p.Position == 4) && p.Y < 8))
        pixel.Position = 0;

      if (TaskGo.TitreKodi)
      {
        CaractereList textes = new(PixelList.Largeur);
        int largeur = textes.SetText(Titre.Musique);

        foreach (Police lettre in textes.GetCaracteres(debut).Where(c => c.Point))
          if (TabSpec.Get(lettre.X, lettre.Y + 1) is Pixel pixel)
            if (pixel.Couleur.IsNoir)//|| pixel.Couleur.IsRouge
              pixel.Position = 3;
            else
              pixel.Position = 4;

        if (cycle % 16 == 0)
          debut++;

        //Reset après avoir défiler tout le texte
        if (cycle % 100000 == 0 || largeur < debut)
        {
          debut = -20;
          Titre.Refresh();
        }
      }

      return debut;
    }




    //private class Point3D
    //{
    //  public double X, Y, Z;
    //  public Couleur Couleur;
    //}

    //private List<Point3D> tunnelPoints = new();
    //private const double ZMax = 20;
    //private Random rand = new();

    //private void InitTunnel()
    //{
    //  tunnelPoints.Clear();

    //  for (int i = 0; i < 100; i++)
    //  {
    //    tunnelPoints.Add(new Point3D
    //    {
    //      X = rand.NextDouble() * 4 - 2,
    //      Y = rand.NextDouble() * 4 - 2,
    //      Z = rand.NextDouble() * ZMax,
    //      Couleur = Couleur.Get(0, 0, 255)
    //    });
    //  }
    //}

    //private void DrawTunnel(double[] fft)
    //{
    //  double amplitude = fft.Max(Math.Abs);

    //  TabSpec.Reset(); // nettoyer l’écran

    //  foreach (var p in tunnelPoints)
    //  {
    //    p.Z -= 0.1 + amplitude * 0.5; // avance selon le son

    //    if (p.Z <= 1)
    //    {
    //      p.X = rand.NextDouble() * 4 - 2;
    //      p.Y = rand.NextDouble() * 4 - 2;
    //      p.Z = ZMax;
    //    }

    //    int x = (int)(p.X / p.Z * 10 + 10);
    //    int y = (int)(p.Y / p.Z * 10 + 10);

    //    if (TabSpec.Get(x, y) is Pixel pixel)
    //      pixel.SetColor(p.Couleur);
    //  }
    //}
  }
}
