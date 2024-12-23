namespace LedMatrix.Components.Layout
{
  public partial class HeureMusique
  {
    private string Bouton
    {
      get
      {
        if (TaskGo.HeureMusique)
          return "btn btn-primary btn-lg";

        return "btn btn-secondary btn-lg";
      }
    }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      TaskGo.HeureMusique = !TaskGo.HeureMusique;

      if (!TaskGo.HeureMusique)
        Pixels.Reset();
    }
  }
}