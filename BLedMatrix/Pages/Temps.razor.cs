using Library.Collection;

namespace BLedMatrix.Pages
{
  public partial class Temps
  {
    private ImageClassList? Animations { get; set; }

    protected override async Task OnInitializedAsync()
    {
      Animations = await ImageClassList.GetAsync($"{Directory.GetCurrentDirectory()}/wwwroot/Images", "Images");
    }
  }
}