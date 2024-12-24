using Library.Collection;

namespace LedMatrix.Components.Pages
{
  public partial class Temps
  {
    private ImageClassList? RadioCanadaIcon { get; set; }
    private ImageClassList? MeteoIcon { get; set; }

    protected override async Task OnInitializedAsync()
    {
      RadioCanadaIcon = await ImageClassList.GetAsync($"{Directory.GetCurrentDirectory()}/wwwroot/Images", "Images");
      MeteoIcon = await ImageClassList.GetAsync($"{Directory.GetCurrentDirectory()}/wwwroot/Images/Meteo", "Images/Meteo");
    }
  }
}