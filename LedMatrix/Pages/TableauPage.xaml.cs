using LedMatrix.Context;
using Library.Collection;
using Library.Entities;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LedMatrix.Pages
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class TableauPage : Page
  {
    public PixelList Pixels
    {
      get { return Util.Context.Pixels; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public TableauPage()
    {
      InitializeComponent();
    }

    private void GridLed_ItemClick(object sender, ItemClickEventArgs e)
    {
      Pixel pixel = (Pixel)e.ClickedItem;

      if (pixel.Couleur == new Color { B = 25 })
        pixel.Couleur = new Color { R = 25 };
      else
        pixel.Couleur = new Color { B = 25 };

      Util.Context.Pixels.SetCouleur(pixel);
      Util.SetLeds();
    }
  }
}