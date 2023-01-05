using Library.Collection;
using Library.Entity;
using Library.Util;
using Nfw.Linux.Joystick.Simple;

namespace BLedMatrix.Shared
{
  public partial class Serpent
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecSerpent);
    }

    /// <summary>
    /// Serpent
    /// </summary>
    private void ExecSerpent()
    {
      int task = TaskGo.StartTask();
      int cycle = 0;
      Random r = new();
      JeuSerpent jeuSerpent = new();

      using ManualResetEventSlim waitHandle = new(false);

      using Joystick joystick = new("/dev/input/js0");
      var manette = new Library.Util.Manette(10, 10);
      joystick.AxisCallback = (j, axis, value) => manette.Set(axis, value / 32767d);
      joystick.ButtonCallback = (j, button, pressed) => manette.Set(button, pressed);

      while (TaskGo.TaskWork(task))
      {
        //Pointage
        Pixels.Set(CaractereList.Print(jeuSerpent.Score.ToString(), 2, 13, Couleur.Get(0, 0, 127)));

        //La balle       
        Pixels.Get(jeuSerpent.X, jeuSerpent.Y).SetColor(r.Next(127), r.Next(127), r.Next(127));

        //Bordure
        for (int i = 0; i < PixelList.Largeur; i++)
        {
          Pixels.Get(i, 0).SetColor(64, 64, 127);
          Pixels.Get(i, 19).SetColor(64, 64, 127);
        }

        for (int i = 0; i < PixelList.Hauteur; i++)
        {
          Pixels.Get(0, i).SetColor(64, 64, 127);
          Pixels.Get(19, i).SetColor(64, 64, 127);
        }

        //Mouvement
        bool mort = jeuSerpent.Mouvement(cycle++, manette);

        int degrade = 1;

        //Serpent
        foreach (var serpent in jeuSerpent.Serpents)
          if (CouleurSerpent(degrade++) is Couleur couleurSerpent)
            Pixels.Get(serpent.X, serpent.Y).SetColor(couleurSerpent);

        //Le serpent mange une balle
        bool manger = jeuSerpent.Manger(manette);

        //Background
        Background.Bleu(Pixels);
        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(jeuSerpent.Vitesse));

        if (mort)
          waitHandle.Wait(TimeSpan.FromMilliseconds(1000));
      }
    }

    /// <summary>
    /// Couleur Serpent
    /// </summary>
    /// <param name="degrade"></param>
    /// <returns></returns>
    public static Couleur CouleurSerpent(int degrade)
    {
      if (degrade < 9)
        return Couleur.Get(31 / degrade, 127 / degrade, 31 / degrade);

      return Couleur.Get(31 / 9, 127 / 9, 31 / 9);
    }
  }
}