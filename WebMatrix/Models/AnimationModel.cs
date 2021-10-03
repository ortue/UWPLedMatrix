using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebMatrix.Context;

namespace WebMatrix.Models
{
  public class AnimationModel
  {
    public string FileNameID { get; set; }
    public ImageClassList Animations { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public AnimationModel(string id)
    {
      FileNameID = id;
      Animations = new ImageClassList("Images/Animation");

      if (string.IsNullOrWhiteSpace(FileNameID) && !Util.Autorun)
      {
        Task.Run(() =>
        {
          Util.Autorun = true;

          int i = 0;

          while (Util.Autorun)
          {
            FileNameID = Animations[i++].FileNameID;
            ShowAnimation();

            using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
              waitHandle.Wait(TimeSpan.FromSeconds(10));

            if (Animations.Count <= i)
              i = 0;
          }
        });
      }
      else if (!string.IsNullOrWhiteSpace(FileNameID))
      {
        Util.Autorun = false;

        ShowAnimation();
      }
    }

    /// <summary>
    /// ShowAnimation
    /// </summary>
    /// <param name="imageClass"></param>
    private void ShowAnimation()
    {
      if (Animations.Find(a => a.FileNameID == FileNameID) is ImageClass imageClass)
      {
        Util.Setup();

        Task.Run(() =>
        {
          int task = Util.StartTask();
          int frame = 0;

          //Fade Out
          if (Animations.Find(a => a.FileNameID == Util.LastAutoRun) is ImageClass lastAutoRun)
            for (int slide = 0; slide < lastAutoRun.Width; slide++)
              SetAnimation(lastAutoRun, frame++, slide, true);

          //Fade In
          for (int slide = imageClass.Width; slide >= 0; slide--)
            SetAnimation(imageClass, frame++, slide);

          Util.LastAutoRun = imageClass.FileNameID;

          //Animation
          while (imageClass.Animation && Util.TaskWork(task))
            SetAnimation(imageClass, frame++, 0);
        });
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
      imageClass.SetÞixelFrame(frame++, Util.Context.Pixels, slide, fadeOut);
      Util.SetLeds();
      Util.Context.Pixels.Reset();

      if (slide == 0 && imageClass.FrameCount < 8)
        using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
          waitHandle.Wait(TimeSpan.FromMilliseconds(100));

      if (slide == 0 && imageClass.FrameCount == 12)
        using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
          waitHandle.Wait(TimeSpan.FromMilliseconds(20));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string IsSelected(string id)
    {
      if (FileNameID == id)
        return "btn-primary";

      return "btn-secondary";
    }
  }
}