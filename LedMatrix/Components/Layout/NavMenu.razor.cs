using System.Diagnostics;

namespace LedMatrix.Components.Layout
{
  public partial class NavMenu
  {
    /// <summary>
    /// Éteindre le raspberry linux
    /// </summary>
    private static void Eteindre()
    {
      try
      {
        Process.Start(new ProcessStartInfo()
        {
          FileName = "pkill",
          Arguments = "--oldest chromium"
        });

        using ManualResetEventSlim waitHandle = new(false);
        waitHandle.Wait(TimeSpan.FromSeconds(2));

        Process.Start(new ProcessStartInfo()
        {
          FileName = "sudo",
          Arguments = "shutdown -h now"
        });
      }
      catch (Exception)
      {

      }
    }
  }
}