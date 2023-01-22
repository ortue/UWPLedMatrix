using Library.Entity;
using System.Xml.Serialization;

namespace Library.Util
{
  public class OpenWeather
  {
    public current? Meteo { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public OpenWeather()
    {
      Refresh();
    }

    /// <summary>
    /// Refresh
    /// </summary>
    public void Refresh()
    {
      Task.Run(() => Meteo = GetMeteo());
    }

    /// <summary>
    /// GetMeteo
    /// </summary>
    /// <returns></returns>
    public static current? GetMeteo()
    {
      try
      {
        HttpClient Client = new() { BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=Sainte-Marthe-sur-le-Lac&mode=xml&units=metric&appid=52534a6f666e45fb30ace3343cea4a47") };
        Task<HttpResponseMessage> response = Client.GetAsync(Client.BaseAddress);

        if (response.Result.IsSuccessStatusCode)
        {
          string xml = response.Result.Content.ReadAsStringAsync().Result;

          using TextReader reader = new StringReader(xml);
          XmlSerializer serializer = new(typeof(current));

          return (current?)serializer.Deserialize(reader);
        }
        else
          return null;
      }
      catch
      {
        return null;
      }
    }
  }
}