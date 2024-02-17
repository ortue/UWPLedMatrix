using BlazorAppMatrix.Class;

namespace BlazorAppMatrix.Components.Layout
{
  public partial class ErrorBoundaryOverride
  {
    private string ExceptionStr { get; set; } = string.Empty;

    protected override Task OnErrorAsync(Exception exception)
    {
      _ = LogToFile.Save(exception.ToString());

      return Task.CompletedTask;
    }
  }
}