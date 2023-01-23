using Library.Entity;
using Microsoft.AspNetCore.Components;

namespace BLedMatrix.Shared
{
  public partial class CouleurPicker
  {
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }

    public string SelectedCouleurStr { get; set; } = string.Empty;

    private Couleur SelectedCouleur
    {
      get
      {
        if (Couleurs.Get(Module, SelectedCouleurStr) is Couleur couleur)
          return couleur;

        return new();
      }
    }

    private bool BoutonActif { get; set; }
    private string Module { get; set; } = string.Empty;

    private IEnumerable<Couleur>? ModuleCouleurs
    {
      get { return Couleurs.Get(Module); }
    }

    private string BGColor
    {
      get { return "background-color: #" + (SelectedCouleur.R * 2).ToString("X2") + (SelectedCouleur.G * 2).ToString("X2") + (SelectedCouleur.B * 2).ToString("X2") + ";"; }
    }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      BoutonActif = !BoutonActif;
      Module = TaskGo.SelectedTaskModule;
    }

    private void ModalOk()
    {
      BoutonActif = false;

      Couleurs.Save();
    }
  }
}