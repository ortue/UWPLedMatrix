using Library.Entity;
using Microsoft.AspNetCore.Components;

namespace LedMatrix.Components.Layout
{
  public partial class CouleurPicker
  {
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }

    public string SelectedCouleurStr { get; set; } = string.Empty;
    private bool BoutonActif { get; set; }
    private string Module { get; set; } = string.Empty;

    private Couleur SelectedCouleur
    {
      get
      {
        if (Couleurs.Get(Module, SelectedCouleurStr) is Couleur couleur)
          return couleur;

        return new();
      }
    }

    private IEnumerable<Couleur>? ModuleCouleurs
    {
      get { return Couleurs.Get(Module); }
    }

    private string BGColor
    {
      get { return "background-color: #" + (SelectedCouleur.R * 2).ToString("X2") + (SelectedCouleur.G * 2).ToString("X2") + (SelectedCouleur.B * 2).ToString("X2") + ";"; }
    }

    public string BtnProfile1
    {
      get
      {
        if (Couleurs.SelectedPosition == 0)
          return "btn btn-primary";

        return "btn btn-secondary";
      }
    }

    public string BtnProfile2
    {
      get
      {
        if (Couleurs.SelectedPosition == 1)
          return "btn btn-primary";

        return "btn btn-secondary";
      }
    }

    public string BtnProfile3
    {
      get
      {
        if (Couleurs.SelectedPosition == 2)
          return "btn btn-primary";

        return "btn btn-secondary";
      }
    }

    public string BtnProfile4
    {
      get
      {
        if (Couleurs.SelectedPosition == 3)
          return "btn btn-primary";

        return "btn btn-secondary";
      }
    }

    /// <summary>
    /// SetProfile
    /// </summary>
    /// <param name="profile"></param>
    private void SetProfile(int profile)
    {
      Couleurs.SelectedPosition = profile;
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