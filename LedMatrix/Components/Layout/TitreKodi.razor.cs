namespace LedMatrix.Components.Layout
{
  public partial class TitreKodi
  {
    private string Bouton
    {
      get
      {
        if (TaskGo.TitreKodi)
          return "btn btn-primary btn-lg";

        return "btn btn-secondary btn-lg";
      }
    }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      TaskGo.TitreKodi = !TaskGo.TitreKodi;

      if (!TaskGo.TitreKodi)
        Pixels.Reset();
    }
  }
}