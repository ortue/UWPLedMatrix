using Library.Collection;
using Library.Entity;
using Nfw.Linux.Joystick.Simple;

namespace BLedMatrix.Shared
{
  public partial class Chute
  {
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
      int[] bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

      using ManualResetEventSlim waitHandle = new(false);

      using Joystick joystick = new("/dev/input/js0");
      var manette = new Library.Util.Manette();
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / (decimal)100000);

      while (TaskGo.TaskWork(task))
      {
        if (bot.All(bo => bo > 20))
        {
          Pixels.Reset();
          bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        Random random = new();
        decimal x = random.Next(0, PixelList.Largeur);
        decimal y = 0;

        Couleur couleur = Couleur.Rnd;

        while (y < PixelList.Hauteur - bot[(int)Math.Round(x, 0)])
        {
          x += manette.AxisAX;

          if (x > 19)
            x = 19;

          if (x < 0)
            x = 0;

          EffacerDernier(x, y);
          Pixels.Get(x, y).SetColor(couleur);
          Pixels.SendPixels();

          int temp = 100;

          if (y > 2)
            temp = 50;

          if (y > 4)
            temp = 25;

          if (y > 6)
            temp = 1;

          waitHandle.Wait(TimeSpan.FromMilliseconds(temp));

          y++;

          if (y >= PixelList.Hauteur - bot[(int)Math.Round(x, 0)])
          {
            if (x > 0 && y < PixelList.Hauteur - bot[(int)Math.Round(x, 0) - 1])
              EffacerDernier(x--, y);

            if (x < PixelList.Largeur - 1 && y < PixelList.Hauteur - bot[(int)Math.Round(x, 0) + 1])
              EffacerDernier(x++, y);

            waitHandle.Wait(TimeSpan.FromMilliseconds(100));
          }
        }

        bot[(int)Math.Round(x, 0)]++;
      }
    }

    /// <summary>
    /// EffacerDernier
    /// </summary>
    private void EffacerDernier(decimal x, decimal y)
    {
      if (y > 0)
        Pixels.Get(x, y - 1).SetColor(new Couleur());
    }
  }
}