using Library.Collection;
using Library.Entity;

namespace BlazorAppMatrix.Components.Pages
{
  public partial class Animation
  {
    public string? FileNameID { get; set; }
    private ImageClassList? Animations { get; set; }
    private string? LastAutoRun { get; set; }
    public bool Transition { get; set; }

    protected override async Task OnInitializedAsync()
    {
      Animations = await ImageClassList.GetAsync($"{Directory.GetCurrentDirectory()}/wwwroot/Images/Animation", "Images/Animation");

      Demo();
    }

    /// <summary>
    /// Demo
    /// </summary>
    private void Demo()
    {
      TaskGo.Autorun = true;

      Task.Run(() =>
      {
        int i = 0;

        if (Animations != null)
          while (TaskGo.Autorun)
          {
            ExecAnimation(Animations[i++].FileNameID);

            using ManualResetEventSlim waitHandle = new(false);
            waitHandle.Wait(TimeSpan.FromSeconds(10));

            if (Animations.Count <= i)
              i = 0;
          }
      });
    }

    /// <summary>
    /// IsSelected
    /// </summary>
    /// <param name="fileNameID"></param>
    /// <returns></returns>
    private string IsSelected(string fileNameID)
    {
      if (FileNameID == fileNameID)
        return "btn btn-primary m-1 rounded-circle";

      return "btn btn-secondary m-1 rounded-circle";
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="id"></param>
    private void Set(string fileNameID)
    {
      if (!Transition)
      {
        TaskGo.Autorun = false;

        Task.Run(() => ExecAnimation(fileNameID));
        StateHasChanged();
      }
    }

    /// <summary>
    /// ShowAnimation
    /// </summary>
    /// <param name="fileNameID"></param>
    private void ExecAnimation(string fileNameID)
    {
      FileNameID = fileNameID;

      if (Animations?.Find(a => a.FileNameID == FileNameID) is ImageClass imageClass)
      {
        int task = TaskGo.StartTask();
        int frame = 0;
        Transition = true;

        //Fade Out
        if (Animations.Find(a => a.FileNameID == LastAutoRun) is ImageClass lastAutoRun)
          for (int slide = 0; slide < lastAutoRun.Width; slide++)
            SetAnimation(lastAutoRun, frame++, slide, true);

        //Fade In
        for (int slide = imageClass.Width; slide >= 0; slide--)
          SetAnimation(imageClass, frame++, slide);

        LastAutoRun = imageClass.FileNameID;
        Transition = false;

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
      imageClass.SetPixelFrame(frame++, Pixels, slide, fadeOut);
      Pixels.SendPixels();
      Pixels.Reset();

      double temps = 100;

      if (slide != 0)
        temps = 50;

      if (slide == 0 && imageClass.FrameCount >= 8)
        temps = 70;

      using ManualResetEventSlim waitHandle = new(false);
      waitHandle.Wait(TimeSpan.FromMilliseconds(temps));
    }

    public void Dispose()
    {
      TaskGo.Autorun = false;
    }
  }
}