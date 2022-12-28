using System.Net.Mail;

namespace BLedMatrix.Shared
{
  public partial class ErrorBoundaryOverride
  {
    private string ExceptionStr { get; set; } = string.Empty;

    protected override Task OnErrorAsync(Exception exception)
    {
      ExceptionStr = exception.ToString();

      return Task.CompletedTask;
    }
  }
}