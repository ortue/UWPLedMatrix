using Library.Collection;
using Library.Entity;

namespace BLedMatrix.Pages
{
  public partial class Animation
  {
    public string? FileNameID { get; set; }
    public ImageClassList? Animations { get; set; }
    public string? LastAutoRun { get; set; }


    //Animations = new ImageClassList("Images/Animation");

    protected override async Task OnInitializedAsync()
    {
      Animations = await ImageClassList.GetAsync($"{System.IO.Directory.GetCurrentDirectory()}/wwwroot/Images/Animation", "Images/Animation");
    }

    /// <summary>
    /// IsSelected
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    private string IsSelected(string fileNameID)
    {
      if(FileNameID==fileNameID )
        return "btn btn-primary m-1 rounded-circle";

      return "btn btn-secondary m-1 rounded-circle";
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="id"></param>
    private void Set(string fileNameID)
    {
      FileNameID = fileNameID;

      Task.Run(ExecAnimation);
    }

    /// <summary>
    /// ExecAnimation
    /// </summary>
    /// <param name="id"></param>
    private void ExecAnimation()
    {
      //if (string.IsNullOrWhiteSpace(id)) //&& !Util.Autorun
      //{
      //  Task.Run(() =>
      //  {
      //    //Util.Autorun = true;

      //    int i = 0;

      //    while (Util.Autorun)
      //    {
      //      FileNameID = Animations[i++].FileNameID;
      //      ShowAnimation();

      //      using (ManualResetEventSlim waitHandle = new(false))
      //        waitHandle.Wait(TimeSpan.FromSeconds(10));

      //      if (Animations.Count <= i)
      //        i = 0;
      //    }
      //  });
      //}
      //else 

      if (!string.IsNullOrWhiteSpace(FileNameID))
      {
        //Util.Autorun = false;

        ShowAnimation();
      }
    }

    /// <summary>
    /// ShowAnimation
    /// </summary>
    private void ShowAnimation()
    {
      if (Animations?.Find(a => a.FileNameID == FileNameID) is ImageClass imageClass)
      {
        int task = TaskGo.StartTask();
        int frame = 0;

        //Fade Out
        if (Animations.Find(a => a.FileNameID == LastAutoRun) is ImageClass lastAutoRun)
          for (int slide = 0; slide < lastAutoRun.Width; slide++)
            SetAnimation(lastAutoRun, frame++, slide, true);

        //Fade In
        for (int slide = imageClass.Width; slide >= 0; slide--)
          SetAnimation(imageClass, frame++, slide);

        LastAutoRun = imageClass.FileNameID;

        //Animation
        while (imageClass.Animation && TaskGo.TaskWork(task))
          SetAnimation(imageClass, frame++, 0);
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
      imageClass.SetÞixelFrame(frame++, Pixels, slide, fadeOut);
      Pixels.SendPixels();
      Pixels.Reset();

      double temps = 100;

      if (slide == 0 && imageClass.FrameCount >= 8)
        temps = 70;

      using ManualResetEventSlim waitHandle = new(false);
      waitHandle.Wait(TimeSpan.FromMilliseconds(temps));
    }
  }
}