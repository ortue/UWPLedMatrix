using Library.Collection;
using Library.Entity;
using Library.Util;
using Microsoft.AspNetCore.Components;

namespace LedMatrix.Components.Layout
{
  public partial class Nouvelles
  {
    [Parameter]
    public ImageClassList? Animations { get; set; }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(() =>
      {
        TaskGo.StopTask();
        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromMilliseconds(100));

        ExecNouvelles();
      });
    }

    /// <summary>
    /// Nouvelles
    /// </summary>
    private void ExecNouvelles()
    {
      int largeur = 0;
      int debut = ResetNouvelle();
      int task = TaskGo.StartTask("Nouvelles");
      DateTime update = DateTime.Now.AddMinutes(-60);
      CaractereList caracteres = new(20);

      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        //Reset après avoir défiler tout le texte
        if (!string.IsNullOrWhiteSpace(RadioCanada.NouvelleStr) && largeur < debut++)
          debut = ResetNouvelle();

        largeur = caracteres.SetText(RadioCanada.NouvelleStr);

        Pixels.Set(CaractereList.Print(caracteres.GetCaracteres(debut), 0, 1, Couleurs.Get("Nouvelles", "TexteCouleur", Couleur.Get(64, 0, 0))));
        Pixels.Set(CaractereList.Print(DateTime.Now.ToString("MM-dd"), 1, 7, Couleurs.Get("Nouvelles", "DateCouleur", Couleur.Bleu)));
        Pixels.Set(CaractereList.Print(CaractereList.Heure, 2, 13, Couleurs.Get("Nouvelles", "HeureCouleur", Couleur.Bleu)));

        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(50));

        //Mettre a jour les nouvelle aux heures
        if (update.AddMinutes(60) < DateTime.Now)
        {
          RadioCanada.Refresh();
          update = DateTime.Now;
        }
      }
    }

    /// <summary>
    /// ResetNouvelle
    /// </summary>
    private int ResetNouvelle()
    {
      RadioCanadaIcon("RadioCanada");

      return -20;
    }

    /// <summary>
    /// RadioCanadaIcon
    /// </summary>
    /// <param name="filename"></param>
    private void RadioCanadaIcon(string filename)
    {
      if (Animations?.Find(a => a.FileNameID == filename) is ImageClass imageClass)
      {
        using ManualResetEventSlim waitHandle = new(false);

        //Fade In
        for (int slide = imageClass.Width; slide >= 0; slide--)
        {
          SetAnimation(imageClass, 0, slide);
          waitHandle.Wait(TimeSpan.FromMilliseconds(20));
        }

        //Fade Out
        for (int slide = 0; slide < imageClass.Width; slide++)
        {
          SetAnimation(imageClass, 0, slide, true);
          waitHandle.Wait(TimeSpan.FromMilliseconds(20));
        }
      }
    }

    /// <summary>
    /// SetAnimation
    /// </summary>
    /// <param name="imageClass"></param>
    /// <param name="frame"></param>
    /// <param name="slide"></param>
    /// <param name="fadeOut"></param>
    private void SetAnimation(ImageClass imageClass, int frame, int slide, bool fadeOut = false)
    {
      imageClass.SetPixelFrame(frame++, Pixels, slide, fadeOut);
      Pixels.SendPixels();
      Pixels.Reset();
    }
  }
}