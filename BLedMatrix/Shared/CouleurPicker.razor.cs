using Library.Util;
using Microsoft.AspNetCore.Components;

namespace BLedMatrix.Shared
{
  public partial class CouleurPicker
  {
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }

    private int R { get; set; }
    private int G { get; set; }
    private int B { get; set; }

    private string BGColor
    {
      get { return "background-color: #" + (R * 2).ToString("X2") + (G * 2).ToString("X2") + (B * 2).ToString("X2") + ";"; }
    }


    private bool BoutonActif { get; set; }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      BoutonActif = !BoutonActif;
    }

    private void ModalOk()
    {
      BoutonActif = false;
    }
  }
}