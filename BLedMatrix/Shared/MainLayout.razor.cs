namespace BLedMatrix.Shared
{
  public partial class MainLayout
  {
    private ErrorBoundaryOverride? errorBoundary;
    protected override void OnParametersSet()
    {
      errorBoundary?.Recover();
    }
  }
}