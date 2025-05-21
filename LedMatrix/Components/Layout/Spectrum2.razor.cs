using Library.Collection;
using Library.Entity;
using Library.Util;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;

namespace LedMatrix.Components.Layout
{
  public partial class Spectrum2
  {
    private PixelList TabSpec { get; set; } = new PixelList(false);

    private const int ScalingWindowSize = 1000;
    private readonly double[] _scalingHistory = new double[ScalingWindowSize];
    private int _scalingIndex = 0;
    public float ScalingFactor { get; private set; } = 1.0f;

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      _ = Task.Run(() =>
      {
        if (TaskGo.AudioCaptureConcurence)
        {
          TaskGo.StopTask();
          using ManualResetEventSlim waitHandle = new(false);
          waitHandle.Wait(TimeSpan.FromMilliseconds(100));
        }

        try
        {
          ExecSpectrum();
        }
        catch (Exception ex)
        {
          var a = ex;
        }
      });
    }

    private void ExecSpectrum()
    {
      TaskGo.AudioCaptureConcurence = true;
      int task = TaskGo.StartTask("Spectrum");

      int cycle = 0;
      int debut = -20;

      using ARecord aRecord = new(256);

      while (TaskGo.TaskWork(task))
      {
        short[] buffer = aRecord.GetBuffer();

        Complex[] samples = buffer.Select(b => new Complex((b - 128.0) / 128.0, 0)).ToArray();
        Fourier.Forward(samples);

        double[] magnitudes = samples.Take(samples.Length / 2).Select(c => c.Magnitude).ToArray();
        int bandWidth = magnitudes.Length / PixelList.Largeur;
        double[] bandLevels = new double[PixelList.Largeur];

        for (int i = 0; i < PixelList.Largeur; i++)
          bandLevels[i] = magnitudes.Skip(i * bandWidth).Take(bandWidth).Average();

        // Estimate scaling factor (adaptive to average volume)
        double averageLevel = bandLevels.Average();

        _scalingHistory[_scalingIndex++ % ScalingWindowSize] = averageLevel;
        double scalingFactor = PixelList.Hauteur / (_scalingHistory.Max() + 1e-6);

        AffHeure(cycle);
        debut = AffTitre(cycle, debut);

        // Convert to bar heights
        int[] heights = bandLevels.Select(level => Math.Min(PixelList.Hauteur, (int)(level * scalingFactor))).ToArray();

        for (int x = 0; x < PixelList.Largeur; x++)
        {
          int h = heights[x];

          for (int y = 0; y < PixelList.Hauteur; y++)
            if (y >= PixelList.Hauteur - h)
              TabSpec.Get(x, y).SetColor(ProportionCouleur(y));
        }

        foreach (Pixel spec in TabSpec)
        {
          Couleur couleur = spec.Position switch
          {
            1 => Couleurs.Get("Spectrum", "HeureCouleur", Couleur.RougePale),
            2 => Couleurs.Get("Spectrum", "HeureAltCouleur", Couleur.Rouge),
            3 => Couleurs.Get("Spectrum", "TitreCouleur", Couleur.RougePale),
            4 => Couleurs.Get("Spectrum", "TitreAltCouleur", Couleur.Rouge),
            _ => spec.Couleur,
          };

          Pixels.Get(spec).SetColor(couleur);
        }

        Pixels.SendPixels();
        SetSpectrum(cycle++);
      }

      TaskGo.AudioCaptureConcurence = false;
    }

    /// <summary>
    /// ProportionCouleur
    /// </summary>
    /// <returns></returns>
    private Couleur ProportionCouleur(int y)
    {
      double bas = (19d - y) / 19d;
      double haut = y / 19d;

      int r = (int)Math.Floor((Couleurs.Get("Spectrum", "BasCouleur", Couleur.Bleu).R * bas) + (Couleurs.Get("Spectrum", "HautCouleur", Couleur.Rouge).R * haut));
      int g = (int)Math.Floor((Couleurs.Get("Spectrum", "BasCouleur", Couleur.Bleu).G * bas) + (Couleurs.Get("Spectrum", "HautCouleur", Couleur.Rouge).G * haut));
      int b = (int)Math.Floor((Couleurs.Get("Spectrum", "BasCouleur", Couleur.Bleu).B * bas) + (Couleurs.Get("Spectrum", "HautCouleur", Couleur.Rouge).B * haut));

      return Couleur.Get(r, g, b);
    }

    /// <summary>
    /// Spectrum trainée
    /// </summary>
    private void SetSpectrum(int cycle)
    {
      //Plus le chffre du mod est grand, plus la trainer sera ralenti
      if (cycle % 4 == 0)
        for (int x = 0; x < PixelList.Largeur; x++)
          if (TabSpec.Where(p => p.X == x && !p.Couleur.IsNoir).OrderBy(p => p.Y).FirstOrDefault() is Pixel pixel)
            pixel.Couleur = Couleur.Noir;
    }

    /// <summary>
    /// AffHeure
    /// </summary>
    private void AffHeure(int cycle)
    {
      if (cycle % 8 == 0)
      {
        foreach (Pixel pixel in TabSpec.Where(p => (p.Position == 1 || p.Position == 2) && p.Y > 12))
          pixel.Position = 0;

        if (TaskGo.HeureMusique)
        {
          CaractereList textes = new(PixelList.Largeur);
          textes.SetText(CaractereList.Heure);

          foreach (Police lettre in textes.GetCaracteres().Where(c => c.Point))
            if (TabSpec.Get(lettre.X + 1, lettre.Y + 13) is Pixel pixel)
              if (TabSpec.Any(p => p.X == pixel.X && p.Y < pixel.Y))
                if (pixel.Couleur.IsNoir)
                  pixel.Position = 1;
                else
                  pixel.Position = 2;
        }
      }
    }

    /// <summary>
    /// Afficher titre
    /// </summary>
    private int AffTitre(int cycle, int debut)
    {
      if (cycle % 16 == 0)
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

          debut++;

          //Reset après avoir défiler tout le texte
          if (cycle % 100000 == 0 || largeur < debut)
          {
            debut = -20;
            Titre.Refresh();
          }
        }
      }

      return debut;
    }
  }
}