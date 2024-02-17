using Library.Entity;

namespace BlazorAppMatrix.Components.Layout
{
  public partial class LedDebug
  {
    private bool IsDebug { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await Task.Run(DebugLed);
    }

    private static string CouleurBG(Pixel pixel)
    {
      return "background:" + pixel.Couleur.Hexa + ";";
    }

    private async Task Refresh()
    {
      IsDebug = !IsDebug;

      await Task.Run(DebugLed);
    }

    private void DebugLed()
    {
      using ManualResetEventSlim waitHandle = new(false);

      while (IsDebug)
      {
        InvokeAsync(StateHasChanged);

        waitHandle.Wait(TimeSpan.FromMilliseconds(1));
      }
    }
  }
}