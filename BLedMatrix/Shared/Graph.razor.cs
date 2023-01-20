﻿using Library.Collection;
using Library.Entity;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace BLedMatrix.Shared
{
  public partial class Graph
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecGraph);
    }

    /// <summary>
    /// Graph
    /// </summary>
    private void ExecGraph()
    {
      int task = TaskGo.StartTask();
      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();
      int cycle = 0;
      int debut = -20;

      while (TaskGo.TaskWork(task))
      {
        double[] fft = Capture(audioCapture, audioBuffer);
        double amplitude = GetAmplitudeGraph(fft);

        GetGraph(fft, amplitude);
        AffHeure();
        debut = AffTitre(cycle++, debut);

        Pixels.SendPixels();
        Pixels.Reset();
      }
    }

    /// <summary>
    /// Capture
    /// </summary>
    /// <param name="audioCapture"></param>
    /// <param name="audioBuffer"></param>
    /// <returns></returns>
    private static double[] Capture(AudioCapture audioCapture, byte[] audioBuffer)
    {
      audioCapture.ReadSamples(audioBuffer, audioBuffer.Length);
      double[] fft = new double[audioBuffer.Length];

      for (int i = 0; i < audioBuffer.Length; i++)
        fft[i] = audioBuffer[i] - 128;

      return fft;
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
    private void GetGraph(double[] fft, double amplitude)
    {
      for (int x = 0; x < PixelList.Largeur; x++)
      {
        int y = (int)(fft[x * 2] * amplitude) + 10;
        byte red = (byte)Math.Abs(fft[x * 2] * amplitude * 11);

        if (Pixels.Get(x, y) is Pixel pixel)
          pixel.SetColor(red, 0, 127 - red);
      }
    }

    /// <summary>
    /// AffHeure
    /// </summary>
    private void AffHeure()
    {
      if (TaskGo.HeureMusique)
      {
        foreach (Pixel pixel in Pixels.Where(p => p.Couleur.IsRouge && p.Y > 12))
          pixel.Couleur = Couleur.Noir;

        CaractereList textes = new(PixelList.Largeur);
        textes.SetText(CaractereList.Heure);

        foreach (Police lettre in textes.GetCaracteres().Where(c => c.Point))
          if (Pixels.Get(lettre.X + 1, lettre.Y + 13) is Pixel pixel)
            if (pixel.Couleur.IsNoir)// || pixel.Couleur.R == 127)
              pixel.SetColor(Couleur.RougePale);
            else
              pixel.SetColor(Couleur.Rouge);
      }
    }

    /// <summary>
    /// Afficher titre
    /// </summary>
    private int AffTitre(int cycle, int debut)
    {
      if (TaskGo.TitreKodi)
      {
        foreach (Pixel pixel in Pixels.Where(p => p.Couleur.IsRouge && p.Y < 8))
          pixel.Couleur = Couleur.Noir;

        CaractereList textes = new(PixelList.Largeur);
        int largeur = textes.SetText(Titre.Musique);

        foreach (Police lettre in textes.GetCaracteres(debut).Where(c => c.Point))
          if (Pixels.Get(lettre.X, lettre.Y + 1) is Pixel pixel)
            if (pixel.Couleur.IsNoir)//|| pixel.Couleur.IsRouge
              pixel.SetColor(Couleur.Rouge);
            else
              pixel.SetColor(Couleur.RougePale);

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
  }
}