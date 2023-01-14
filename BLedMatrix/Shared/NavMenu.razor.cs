using System.Diagnostics;

namespace BLedMatrix.Shared
{
  public partial class NavMenu
  {
    private bool collapseNavMenu = true;

    private string? NavMenuCssClass
    {
      get { return collapseNavMenu ? "collapse" : null; }
    }

    /// <summary>
    /// ToggleNavMenu
    /// </summary>
    private void ToggleNavMenu()
    {
      collapseNavMenu = !collapseNavMenu;
    }

    /// <summary>
    /// Éteindre le raspberry linux
    /// </summary>
    private void Eteindre()
    {
      Process.Start(new ProcessStartInfo()
      {
        FileName = "pkill",
        Arguments = "--oldest chromium"
      });

      using ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
      waitHandle.Wait(TimeSpan.FromSeconds(2));

      Process.Start(new ProcessStartInfo()
      {
        FileName = "sudo",
        Arguments = "shutdown -h now"
      });
    }
  }
}