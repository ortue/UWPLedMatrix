using Library.Collection;
using Library.Entity;

namespace BLedMatrix.Shared
{
  public partial class Chute
  {
    private void SetChute()
    {
      int task = TaskGo.StartTask();
      int[] bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        if (bot.All(bo => bo > 20))
        {
          Pixels.Reset();
          bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        Random random = new();
        int x = random.Next(0, PixelList.Largeur);
        int y = 0;

        Couleur couleur = Couleur.Rnd;

        while (y < PixelList.Hauteur - bot[x])
        {
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

          if (y >= PixelList.Hauteur - bot[x])
          {
            if (x > 0 && y < PixelList.Hauteur - bot[x - 1])
              EffacerDernier(x--, y);

            if (x < PixelList.Largeur - 1 && y < PixelList.Hauteur - bot[x + 1])
              EffacerDernier(x++, y);

            waitHandle.Wait(TimeSpan.FromMilliseconds(100));
          }
        }

        bot[x]++;
      }
    }


    /// <summary>
    /// EffacerDernier
    /// </summary>
    private void EffacerDernier(int x, int y)
    {
      if (y > 0)
        Pixels.Get(x, y - 1).SetColor(new Couleur());
    }
  }
}