using LedMatrix.Class;
using Library.Collection;
using Library.Entity;
using Library.Util;
using Nfw.Linux.Joystick.Simple;

namespace LedMatrix.Components.Layout
{
  public partial class Chute
  {
    private int[] Bot { get; set; } = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

    private PixelList TabGoutes { get; set; } = new PixelList(false);

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecChute);
    }

    /// <summary>
    /// ExecChute
    /// </summary>
    private void ExecChute()
    {
      int task = TaskGo.StartTask();

      using Joystick joystick = new("/dev/input/js0");
      Library.Util.Manette manette = new(0, 0);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 100000d);

      Random random = new();

      int yy = 0;
      int cycle = 0;

      foreach (Pixel pixel in TabGoutes)
      {
        pixel.SetColor(Couleur.Rnd);
        pixel.X = random.Next(0, PixelList.Largeur);

        yy -= 5;

        pixel.Y = yy;
      }


      while (TaskGo.TaskWork(task))
      {

        if (cycle++ % 100 == 0)
        {

          foreach (Pixel goute in TabGoutes)
          {
            goute.Y++;



            if (goute.Y >= 0)
              if (Pixels.Get(goute.X, goute.Y) is Pixel pixel)
                pixel.SetColor(goute.Couleur);
          }


        }


        Background.Bleu(Pixels);
        Pixels.SendPixels();
        Pixels.Reset();
      }
    }























    /// <summary>
    /// ExecChute
    /// </summary>
    private async void ExecChutes()
    {
      int task = TaskGo.StartTask();

      using Joystick joystick = new("/dev/input/js0");
      Library.Util.Manette manette = new(0, 0);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 100000d);

      Random random = new();
      double y = 0;

      try
      {
        while (TaskGo.TaskWork(task))
        {
          if (Bot.All(bo => bo > 20))
          {
            Pixels.Reset();
            Bot = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
          }


          y = Descente(random.Next(0, PixelList.Largeur), y, Couleur.Rnd, manette);
        }
      }
      catch (Exception ex)
      {
        await LogToFile.Save(ex.ToString());

        throw new Exception(ex.ToString());
      }
    }

    /// <summary>
    /// Descente
    /// </summary>
    private double Descente(double x, double y, Couleur couleur, Library.Util.Manette manette)
    {
      using ManualResetEventSlim waitHandle = new(false);

      if (y < PixelList.Hauteur - Bot[(int)Math.Round(x, 0)])
      {
        x += manette.AxisAX;

        if (x > 19)
          x = 19;

        if (x < 0)
          x = 0;

        EffacerDernier(x, y);
        Pixels.Get(x, y).SetColor(couleur);
        Pixels.SendPixels();

        waitHandle.Wait(TimeSpan.FromMilliseconds(Temp(y)));

        y++;

        if (y >= PixelList.Hauteur - Bot[(int)Math.Round(x, 0)])
        {
          if (x > 0 && y < PixelList.Hauteur - Bot[(int)Math.Round(x, 0) - 1])
            EffacerDernier(x--, y);

          if (x < PixelList.Largeur - 1 && y < PixelList.Hauteur - Bot[(int)Math.Round(x, 0) + 1])
            EffacerDernier(x++, y);

          waitHandle.Wait(TimeSpan.FromMilliseconds(100));
        }
      }
      else
      {
        Bot[(int)Math.Round(x, 0)]++;
        y = 0;
      }

      return y;
    }

    /// <summary>
    /// Temp
    /// </summary>
    /// <returns></returns>
    private static int Temp(double y)
    {
      if (y < 3)
        return 75;

      if (y < 6)
        return 50;

      if (y < 9)
        return 25;

      return 1;
    }

    /// <summary>
    /// EffacerDernier
    /// </summary>
    private void EffacerDernier(double x, double y)
    {
      if (y > 0)
        Pixels.Get(x, y - 1).SetColor(new Couleur());
    }
  }
}