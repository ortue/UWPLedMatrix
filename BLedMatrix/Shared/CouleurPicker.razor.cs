namespace BLedMatrix.Shared
{
  public partial class CouleurPicker
  {
    private bool BoutonActif { get; set; }

    private string Actif
    {
      get
      {
        if (BoutonActif)
          return "btn btn-primary btn-lg";

        return "btn btn-secondary btn-lg";
      }
    }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      BoutonActif = !BoutonActif;


    }
  }
}