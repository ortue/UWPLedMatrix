﻿namespace BLedMatrix.Shared
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
      StateHasChanged();
    }
  }
}