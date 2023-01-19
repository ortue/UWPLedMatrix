using OpenTK.Audio.OpenAL;
using OpenTK.Audio;
using Library.Entity;
using Library.Collection;

namespace BLedMatrix.Shared
{
  public partial class VUMeter
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecVUMeter);
    }

    /// <summary>
    /// VUMeter
    /// </summary>
    private void ExecVUMeter()
    {
      int task = TaskGo.StartTask();

      Couleur couleur = Couleur.Get(0, 0, 8);
      Random ra = new();
      bool whiteBgColor = true;

      if (ra.Next(1, 3) == 1)
      {
        couleur = Couleur.Get(63, 63, 127);
        whiteBgColor = false;
      }

      double max = 0;
      //CaractereList caracteres = new(Util.Context.Largeur);

      byte[] audioBuffer = new byte[256];
      using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      audioCapture.Start();

      while (TaskGo.TaskWork(task))
      {
        max -= 0.3;

        //double[] fft = Capture(audioCapture, audioBuffer);

        //if (fft.Max(a => Math.Abs(a)) > max)
        //  max = fft.Max(a => Math.Abs(a));

        //if (whiteBgColor)
        //  foreach (Pixel pixel in Util.Context.Pixels)
        //    pixel.Set(127, 127, 127);

        //caracteres.SetText("VU");
        //Pixels.Print(caracteres.GetCaracteres(), 5, 12, couleur);

        Couleur couleurMax = couleur;

        //lumiere max
        if (max > 75)
          couleurMax = Couleur.Get(127, 0, 0);

        Pixels.Get(17, 13).SetColor(couleurMax);
        Pixels.Get(18, 13).SetColor(couleurMax);
        Pixels.Get(17, 14).SetColor(couleurMax);
        Pixels.Get(18, 14).SetColor(couleurMax);

        //dessin
        Pixels.Get(1, 10).SetColor(couleur);
        Pixels.Get(2, 10).SetColor(couleur);
        Pixels.Get(3, 10).SetColor(couleur);

        Pixels.Get(4, 9).SetColor(couleur);
        Pixels.Get(5, 9).SetColor(couleur);
        Pixels.Get(6, 9).SetColor(couleur);
        Pixels.Get(7, 9).SetColor(couleur);

        Pixels.Get(8, 8).SetColor(couleur);
        Pixels.Get(9, 8).SetColor(couleur);
        Pixels.Get(10, 8).SetColor(couleur);
        Pixels.Get(11, 8).SetColor(couleur);

        Pixels.Get(12, 9).SetColor(couleur);
        Pixels.Get(13, 9).SetColor(couleur);
        Pixels.Get(14, 9).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(15, 9).SetColor(Couleur.Get(127, 0, 0));

        Pixels.Get(16, 10).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(17, 10).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(18, 10).SetColor(Couleur.Get(127, 0, 0));

        //Moins
        Pixels.Get(1, 4).SetColor(couleur);
        Pixels.Get(2, 4).SetColor(couleur);
        Pixels.Get(3, 4).SetColor(couleur);

        Pixels.Get(2, 8).SetColor(couleur);
        Pixels.Get(2, 9).SetColor(couleur);

        Pixels.Get(6, 7).SetColor(couleur);
        Pixels.Get(6, 8).SetColor(couleur);

        Pixels.Get(9, 6).SetColor(couleur);
        Pixels.Get(9, 7).SetColor(couleur);

        Pixels.Get(11, 6).SetColor(couleur);
        Pixels.Get(11, 7).SetColor(couleur);

        Pixels.Get(13, 7).SetColor(couleur);
        Pixels.Get(13, 8).SetColor(couleur);

        Pixels.Get(15, 7).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(15, 8).SetColor(Couleur.Get(127, 0, 0));

        Pixels.Get(17, 8).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(17, 9).SetColor(Couleur.Get(127, 0, 0));

        //Plus
        Pixels.Get(17, 3).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(16, 4).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(17, 4).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(18, 4).SetColor(Couleur.Get(127, 0, 0));
        Pixels.Get(17, 5).SetColor(Couleur.Get(127, 0, 0));

        //base
        Pixels.Get(8, 18).SetColor(couleur);
        Pixels.Get(9, 18).SetColor(couleur);
        Pixels.Get(10, 18).SetColor(couleur);
        Pixels.Get(11, 18).SetColor(couleur);

        Pixels.Get(7, 19).SetColor(couleur);
        Pixels.Get(8, 19).SetColor(couleur);
        Pixels.Get(9, 19).SetColor(couleur);
        Pixels.Get(10, 19).SetColor(couleur);
        Pixels.Get(11, 19).SetColor(couleur);
        Pixels.Get(12, 19).SetColor(couleur);

        //aiguille
        //for (int r = 2; r < 18; r++)
        //  Pixels.Get(GetCercleCoord(max + 315, r)).SetColor(couleur);

        Pixels.SendPixels();
        Pixels.Reset();
      }
    }
  }
}